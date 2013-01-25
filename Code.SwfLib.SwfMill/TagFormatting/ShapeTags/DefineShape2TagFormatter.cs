﻿using System.Xml.Linq;
using Code.SwfLib.SwfMill.Shapes;
using Code.SwfLib.Tags.ShapeTags;

namespace Code.SwfLib.SwfMill.TagFormatting.ShapeTags {
    public class DefineShape2TagFormatter : DefineShapeBaseFormatter<DefineShape2Tag> {

        protected override void WriteStyles(DefineShape2Tag tag, XElement xStyles) {
            xStyles.Add(XStyleList.ToXml(tag.FillStyles, tag.LineStyles));
        }

        protected override void ReadShapes(DefineShape2Tag tag, XElement xEdges) {
            foreach (var xShape in xEdges.Elements()) {
                tag.ShapeRecords.Add(XShapeRecord.RGBFromXml(xShape));
            }
        }

        protected override void WriteShapes(DefineShape2Tag tag, XElement xEdges) {
            foreach (var shapeRecord in tag.ShapeRecords) {
                xEdges.Add(XShapeRecord.ToXml(shapeRecord));
            }
        }

        protected override void AcceptShapeTagElement(DefineShape2Tag tag, XElement element) {
        }

        public override string TagName {
            get { return "DefineShape2"; }
        }
    }
}
