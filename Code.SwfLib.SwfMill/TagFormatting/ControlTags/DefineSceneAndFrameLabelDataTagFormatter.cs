﻿using System;
using System.Xml.Linq;
using Code.SwfLib.Tags.ControlTags;

namespace Code.SwfLib.SwfMill.TagFormatting.ControlTags {
    public class DefineSceneAndFrameLabelDataTagFormatter : TagFormatterBase<DefineSceneAndFrameLabelDataTag> {
        protected override XElement FormatTagElement(DefineSceneAndFrameLabelDataTag tag, XElement xTag) {
            return xTag;
        }

        protected override void AcceptTagAttribute(DefineSceneAndFrameLabelDataTag tag, XAttribute attrib) {
            throw new NotImplementedException();
        }

        protected override void AcceptTagElement(DefineSceneAndFrameLabelDataTag tag, XElement element) {
            throw new NotImplementedException();
        }

        public override string TagName {
            get { return "DefineSceneAndFrameLabelData"; }
        }
    }
}
