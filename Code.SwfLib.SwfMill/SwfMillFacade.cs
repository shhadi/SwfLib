﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Code.SwfLib.Data;
using Code.SwfLib.Tags;

namespace Code.SwfLib.SwfMill {
    public class SwfMillFacade {

        private TagFormatterFactory _formatterFactory;

        public XDocument ConvertToXml(SwfFile file) {
            _formatterFactory = new TagFormatterFactory(file.FileInfo.Version);
            XDocument doc = new XDocument(GetRoot(file));
            doc.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            return doc;
        }

        public SwfFile ReadFromXml(XDocument doc) {
            var root = doc.Root;
            SwfFile file = new SwfFile();
            SwfFileInfo fileInfo;
            if (root == null || root.Name.LocalName != "swf") {
                throw new FormatException("Expected swf as root");
            }
            fileInfo.Version = byte.Parse(root.Attribute(XName.Get("version")).Value);
            fileInfo.Format = root.Attribute(XName.Get("compressed")).Value == "1" ? "CWS" : "FWS";
            fileInfo.FileLength = 0;
            file.FileInfo = fileInfo;

            var hdr = root.Element(XName.Get("Header"));
            file.Header.FrameRate = double.Parse(hdr.Attribute(XName.Get("framerate")).Value);
            file.Header.FrameCount = ushort.Parse(hdr.Attribute(XName.Get("frames")).Value);
            file.Header.FrameSize = ParseRect(hdr.Element(XName.Get("size")).Element(XName.Get("Rectangle")));

            var formatterFactory = new TagFormatterFactory(fileInfo.Version);
            var tags = hdr.Element(XName.Get("tags"));
            foreach (var tagElem in tags.Elements()) {
                var tag = SwfTagNameMapping.CreateTagByXmlName(tagElem.Name.LocalName);
                var formatter = formatterFactory.GetFormatter(tag);
                foreach (var attrib in tagElem.Attributes()) {
                    formatter.AcceptAttribute(tag, attrib);
                }
                foreach (var elem in tagElem.Elements()) {
                    formatter.AcceptElement(tag, elem);
                }
                file.Tags.Add(tag);
            }
            return file;
        }


        private static SwfRect ParseRect(XElement elem) {
            if (elem.Name.LocalName != "Rectangle") throw new FormatException("Invalid rectangle");
            SwfRect rect;
            rect.XMin = int.Parse(elem.Attribute(XName.Get("left")).Value);
            rect.YMin = int.Parse(elem.Attribute(XName.Get("top")).Value);
            rect.XMax = int.Parse(elem.Attribute(XName.Get("right")).Value);
            rect.YMax = int.Parse(elem.Attribute(XName.Get("bottom")).Value);
            return rect;
        }

        private XElement GetRoot(SwfFile file) {
            return new XElement(XName.Get("swf"),
                                new XAttribute(XName.Get("version"), file.FileInfo.Version),
                                new XAttribute(XName.Get("compressed"), IsCompressed(file) ? "1" : "0"),
                                GetSwfHeaderXml(file)
                );
        }

        private XElement GetTagsXml(IEnumerable<SwfTagBase> tags) {
            return new XElement(XName.Get("tags"), tags.Select(item => BuildTagXml(item)));
        }

        private XElement BuildTagXml(SwfTagBase tag) {
            var formatter = _formatterFactory.GetFormatter(tag);
            return formatter.FormatTag(tag);
        }


        private XElement GetSwfHeaderXml(SwfFile file) {
            //TODO: This is strange that swfmill wants tags to be inside header... 
            var header = file.Header;
            return new XElement(XName.Get("Header"),
                                new XAttribute(XName.Get("framerate"), header.FrameRate),
                                new XAttribute(XName.Get("frames"), header.FrameCount),
                                new XElement(XName.Get("size"), GetRectangleXml(header.FrameSize)),
                                GetTagsXml(file.Tags));
        }

        private static XElement GetRectangleXml(SwfRect rect) {
            return new XElement(XName.Get("Rectangle"),
                                new XAttribute(XName.Get("left"), rect.XMin),
                                new XAttribute(XName.Get("right"), rect.XMax),
                                new XAttribute(XName.Get("top"), rect.YMin),
                                new XAttribute(XName.Get("bottom"), rect.YMax));
        }

        private static bool IsCompressed(SwfFile file) {
            switch (file.FileInfo.Format) {
                case "CWS":
                    return true;
                case "FWS":
                    return false;
                default:
                    throw new InvalidOperationException();
            }
        }

    }
}