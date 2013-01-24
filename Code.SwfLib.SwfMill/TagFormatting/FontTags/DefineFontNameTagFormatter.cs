﻿using System;
using System.Xml.Linq;
using Code.SwfLib.Tags.FontTags;

namespace Code.SwfLib.SwfMill.TagFormatting.FontTags {
    public class DefineFontNameTagFormatter : DefineFontBaseFormatter<DefineFontNameTag> {

        protected const string COPYRIGHT_ATTRIB = "copyright";

        protected override void AcceptTagAttribute(DefineFontNameTag tag, XAttribute attrib) {
            switch (attrib.Name.LocalName) {
                case NAME_ATTRIB:
                    tag.FontName = attrib.Value;
                    break;
                case COPYRIGHT_ATTRIB:
                    tag.FontCopyright = attrib.Value;
                    break;
                default:
                    throw new FormatException("Invalid attribute " + attrib.Name.LocalName);
            }
        }

        protected override void AcceptTagElement(DefineFontNameTag tag, XElement element) {
            switch (element.Name.LocalName) {
                default:
                    throw new FormatException("Invalid element " + element.Name.LocalName);
            }
        }

        protected override XElement FormatTagElement(DefineFontNameTag tag, XElement xTag) {
            xTag.Add(new XAttribute(XName.Get(NAME_ATTRIB), tag.FontName));
            xTag.Add(new XAttribute(XName.Get(COPYRIGHT_ATTRIB), tag.FontCopyright));
            return xTag;
        }

        public override string TagName {
            get { return "DefineFontInfo3"; }
        }

    }
}
