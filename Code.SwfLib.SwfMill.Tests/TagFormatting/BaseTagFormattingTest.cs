﻿using System.IO;
using System.Xml.Linq;
using Code.SwfLib.SwfMill.TagFormatting;
using Code.SwfLib.Tags;
using NUnit.Framework;

namespace Code.SwfLib.SwfMill.Tests.TagFormatting {
    public abstract class BaseTagFormattingTest<T, F>
        where T : SwfTagBase, new()
        where F : TagFormatterBase<T>, new() {

        private readonly F _formatter = new F();

        protected void ConvertToXmlAndCompare(T tag, string resourceXml) {
            var doc = _formatter.FormatElement(tag);
            new XmlComparision(XmlDifferenceHandler).Compare(doc, XDocument.Load(new StreamReader(OpenEmbeddedResource(resourceXml))).Root);
        }

        private Stream OpenEmbeddedResource(string name) {
            return GetType().Assembly.GetManifestResourceStream("Code.SwfLib.SwfMill.Tests.Resources.TagFormatting." + name);
        }

        private static void XmlDifferenceHandler(string message) {
            Assert.Fail(message);
        }

        protected T ParseTagFromResource(string resourceXml) {
            var resource = OpenEmbeddedResource(resourceXml);
            return ParseTag(new StreamReader(resource));
        }

        protected T ParseTag(string tagXml) {
            return ParseTag(new StringReader(tagXml));
        }

        protected T ParseTag(TextReader reader) {
            XDocument doc = XDocument.Load(reader);
            var tagElem = doc.Root;
            return ParseTag(tagElem);
        }

        protected T ParseTag(XElement tagElem) {
            var formatter = new F();
            var tag = new T();
            formatter.InitTag(tag, tagElem);
            foreach (var attrib in tagElem.Attributes()) {
                formatter.AcceptAttribute(tag, attrib);
            }
            foreach (var elem in tagElem.Elements()) {
                formatter.AcceptElement(tag, elem);
            }
            return tag;
        }

        protected void DoubleConversionFromResourceTest(string resourceXml) {
            var resource = OpenEmbeddedResource(resourceXml);
            var reader = new StreamReader(resource);
            XDocument originalDoc = XDocument.Load(reader);
            var xOriginalTag = originalDoc.Root;

            var tag = ParseTag(xOriginalTag);
            var formatter = new F();
            var xResultTag = formatter.FormatElement(tag);
            new XmlComparision(Assert.Fail).Compare(xOriginalTag, xResultTag);
        }

    }
}
