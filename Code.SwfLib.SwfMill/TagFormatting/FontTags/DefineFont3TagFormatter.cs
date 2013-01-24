﻿using System;
using System.Xml.Linq;
using Code.SwfLib.Tags.FontTags;

namespace Code.SwfLib.SwfMill.TagFormatting.FontTags {
    public class DefineFont3TagFormatter : DefineFontBaseFormatter<DefineFont3Tag> {

        private const string HAS_LAYOUT_ATTRIB = "hasLayout";
        private const string SHIFT_JIS_ATTRIB = "isShiftJIS";
        private const string UNICODE_ATTRIB = "isUnicode";
        private const string ANSI_ATTRIB = "isANSII";
        private const string WIDE_GLYPH_OFFSETS_ATTRIB = "wideGlyphOffsets";
        private const string WIDE_CODES_ATTRIB = "wideCodes";
        private const string ITALIC_ATTRIB = "italic";
        private const string BOLD_ATTRIB = "bold";

        private const string LANGUAGE_ATTRIB = "language";
        private const string GLYPHS_COUNT_ATTRIB = "glyphsCount";

        protected override void AcceptTagAttribute(DefineFont3Tag tag, XAttribute attrib) {
            switch (attrib.Name.LocalName) {
                case NAME_ATTRIB:
                    tag.FontName = attrib.Value;
                    break;
                case LANGUAGE_ATTRIB:
                    tag.Language = byte.Parse(attrib.Value);
                    break;
                case GLYPHS_COUNT_ATTRIB:
                    tag.Glyphs = new DefineFont3Glyph[int.Parse(attrib.Value)];
                    break;
                case HAS_LAYOUT_ATTRIB:
                    tag.HasLayout = ParseBoolFromDigit(attrib);
                    break;
                case SHIFT_JIS_ATTRIB:
                    tag.ShiftJIS = ParseBoolFromDigit(attrib);
                    break;
                case UNICODE_ATTRIB:
                    tag.SmallText = ParseBoolFromDigit(attrib);
                    break;
                case ANSI_ATTRIB:
                    tag.ANSI = ParseBoolFromDigit(attrib);
                    break;
                case WIDE_GLYPH_OFFSETS_ATTRIB:
                    tag.WideOffsets = ParseBoolFromDigit(attrib);
                    break;
                case WIDE_CODES_ATTRIB:
                    tag.WideCodes = ParseBoolFromDigit(attrib);
                    break;
                case ITALIC_ATTRIB:
                    tag.Italic = ParseBoolFromDigit(attrib);
                    break;
                case BOLD_ATTRIB:
                    tag.Bold = ParseBoolFromDigit(attrib);
                    break;
                default:
                    throw new FormatException("Invalid attribute " + attrib.Name.LocalName);
            }
        }

        protected override void AcceptTagElement(DefineFont3Tag tag, XElement element) {
            switch (element.Name.LocalName) {
                case DATA_TAG:
                    tag.RestData = ReadBase64(element);
                    break;
                default:
                    throw new FormatException("Invalid element " + element.Name.LocalName);
            }
        }

        protected override XElement FormatTagElement(DefineFont3Tag tag, XElement xTag) {
            xTag.Add(new XAttribute(LANGUAGE_ATTRIB, tag.Language));
            xTag.Add(new XAttribute(GLYPHS_COUNT_ATTRIB, tag.Glyphs.Length));
            xTag.Add(new XAttribute(HAS_LAYOUT_ATTRIB, FormatBoolToDigit(tag.HasLayout)));
            xTag.Add(new XAttribute(SHIFT_JIS_ATTRIB, FormatBoolToDigit(tag.ShiftJIS)));
            xTag.Add(new XAttribute(UNICODE_ATTRIB, FormatBoolToDigit(tag.SmallText)));
            xTag.Add(new XAttribute(ANSI_ATTRIB, FormatBoolToDigit(tag.ANSI)));
            xTag.Add(new XAttribute(WIDE_GLYPH_OFFSETS_ATTRIB, FormatBoolToDigit(tag.WideOffsets)));
            xTag.Add(new XAttribute(WIDE_CODES_ATTRIB, FormatBoolToDigit(tag.WideCodes)));
            xTag.Add(new XAttribute(ITALIC_ATTRIB, FormatBoolToDigit(tag.Italic)));
            xTag.Add(new XAttribute(BOLD_ATTRIB, FormatBoolToDigit(tag.Bold)));
            xTag.Add(new XAttribute(NAME_ATTRIB, tag.FontName));
            return xTag;
        }

        protected override string TagName {
            get { return "DefineFont3"; }
        }

    }
}