﻿using System;
using System.Xml.Linq;
using Code.SwfLib.SwfMill.Data;
using Code.SwfLib.SwfMill.Shapes;
using Code.SwfLib.Tags.ShapeTags;

namespace Code.SwfLib.SwfMill.TagFormatting.ShapeTags {
    public class DefineShape4TagFormatter : DefineShapeBaseFormatter<DefineShape4Tag> {

        private const string EDGE_BOUNDS_ELEM = "strokeBounds";

        protected override XElement FormatStyles(DefineShape4Tag tag) {
            return XStyleList.ToXml(tag.FillStyles, tag.LineStyles);
        }

        protected override XElement FormatShape(DefineShape4Tag tag) {
            return XShape.ToXml(tag.ShapeRecords);
        }

        protected override void ReadShapes(DefineShape4Tag tag, XElement xShape) {
            XShape.FromXml(xShape, tag.ShapeRecords);
        }

        protected override void ReadStyles(DefineShape4Tag tag, XElement xStyleList) {
            XStyleList.FromXml(xStyleList, tag.FillStyles, tag.LineStyles);
        }

        protected override void FormatAdditionalAttributes(DefineShape4Tag tag, XElement xTag) {
            xTag.Add(new XAttribute("nonScalingStrokes", CommonFormatter.Format(tag.UsesNonScalingStrokes)));
            xTag.Add(new XAttribute("scalingStrokes", CommonFormatter.Format(tag.UsesScalingStrokes)));
            if (tag.UsesFillWindingRule) {
                xTag.Add(new XAttribute("fillWindingRule", CommonFormatter.Format(tag.UsesFillWindingRule)));
            }
            if (tag.ReservedFlags != 0) {
                xTag.Add(new XAttribute("reserved", tag.ReservedFlags));
            }
        }

        protected override void AcceptShapeAttribute(DefineShape4Tag tag, XAttribute attrib) {
            switch (attrib.Name.LocalName) {
                case "nonScalingStrokes":
                    tag.UsesNonScalingStrokes = ParseBoolFromDigit(attrib);
                    break;
                case "scalingStrokes":
                    tag.UsesScalingStrokes = ParseBoolFromDigit(attrib);
                    break;
                case "fillWindingRule":
                    tag.UsesFillWindingRule = ParseBoolFromDigit(attrib);
                    break;
                case "reserved":
                    tag.ReservedFlags = byte.Parse(attrib.Value);
                    break;
                default:
                    throw new FormatException("Invalid attribute " + attrib.Name.LocalName);
            }
        }

        protected override void FormatAdditionalBounds(DefineShape4Tag tag, XElement elem) {
            elem.Add(new XElement(EDGE_BOUNDS_ELEM, XRect.ToXml(tag.EdgeBounds)));
        }

        protected override void AcceptShapeTagElement(DefineShape4Tag tag, XElement element) {
            switch (element.Name.LocalName) {
                case EDGE_BOUNDS_ELEM:
                    tag.EdgeBounds = XRect.FromXml(element.Element("Rectangle"));
                    break;
                default:
                    AcceptShapeTagElement(tag, element);
                    break;
            }
        }

        public override string TagName {
            get { return "DefineShape5"; }
        }
    }
}
