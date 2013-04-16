﻿using System.Xml.Linq;
using Code.SwfLib.Shapes.FillStyles;
using Code.SwfLib.SwfMill.Data;
using Code.SwfLib.SwfMill.Utils;

namespace Code.SwfLib.SwfMill.Shapes {
    public static class XSolidFillStyle {

        public const string SOLID = "Solid";

        public static XElement ToXml(SolidFillStyleRGB fillStyle) {
            var res = new XElement(SOLID);
            res.Add(new XElement("color", XColorRGB.ToXml(fillStyle.Color)));
            return res;
        }

        public static XElement ToXml(SolidFillStyleRGBA fillStyle) {
            var res = new XElement(SOLID);
            res.Add(new XElement("color", XColorRGBA.ToXml(fillStyle.Color)));
            return res;
        }

        public static SolidFillStyleRGB FromXmlRGB(XElement xFillStyle) {
            var xColor = xFillStyle.RequiredElement("color").Element("Color");
            return new SolidFillStyleRGB {
                Color = XColorRGB.FromXml(xColor)
            };
        }

        public static SolidFillStyleRGBA FromXmlRGBA(XElement xFillStyle) {
            var xColor = xFillStyle.RequiredElement("color").Element("Color");
            return new SolidFillStyleRGBA {
                Color = XColorRGBA.FromXml(xColor)
            };
        }
    }
}