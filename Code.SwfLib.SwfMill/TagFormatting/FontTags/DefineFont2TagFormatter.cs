﻿using System.Xml.Linq;
using Code.SwfLib.Tags.FontTags;

namespace Code.SwfLib.SwfMill.TagFormatting.FontTags {
    public class DefineFont2TagFormatter : DefineFontBaseFormatter<DefineFont2Tag> {
        protected override XElement FormatTagElement(DefineFont2Tag tag, XElement xTag) {
            return xTag;
        }

        protected override void AcceptTagAttribute(DefineFont2Tag tag, XAttribute attrib) {
            throw new System.NotImplementedException();
        }

        protected override void AcceptTagElement(DefineFont2Tag tag, XElement element) {
            throw new System.NotImplementedException();
        }

        public override string TagName {
            get { return "DefineFont2"; }
        }

    }
}
