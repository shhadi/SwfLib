﻿using System;
using System.Xml.Linq;
using Code.SwfLib.Tags.ButtonTags;

namespace Code.SwfLib.SwfMill.TagFormatting.ButtonTags {
    public class DefineButtonCxformTagFormatter : DefineButtonBaseTagFormatter<DefineButtonCxformTag> {
        protected override XElement FormatTagElement(DefineButtonCxformTag tag, XElement xTag) {
            return xTag;
        }

        protected override void AcceptTagAttribute(DefineButtonCxformTag tag, XAttribute attrib) {
            throw new NotImplementedException();
        }

        protected override void AcceptTagElement(DefineButtonCxformTag tag, XElement element) {
            throw new NotImplementedException();
        }

        public override string TagName {
            get { return "DefineButtonCxform"; }
        }
    }
}
