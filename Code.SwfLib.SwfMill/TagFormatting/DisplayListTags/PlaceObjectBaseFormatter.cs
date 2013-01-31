﻿using System.Xml.Linq;
using Code.SwfLib.SwfMill.Data;
using Code.SwfLib.Tags.DisplayListTags;

namespace Code.SwfLib.SwfMill.TagFormatting.DisplayListTags {
    public abstract class PlaceObjectBaseFormatter<T> : TagFormatterBase<T> where T : PlaceObjectBaseTag {

        private const string DEPTH_ATTRIB = "depth";
        private const string TRANSFORM_ELEM = "transform";

        protected sealed override void FormatTagElement(T tag, XElement xTag) {
            xTag.Add(new XAttribute(DEPTH_ATTRIB, tag.Depth));

            if (HasMatrix(tag)) {
                xTag.Add(new XElement(TRANSFORM_ELEM, XMatrix.ToXml(tag.Matrix)));
            }

            FormatPlaceElement(tag, xTag);
        }

        protected sealed override bool AcceptTagAttribute(T tag, XAttribute attrib) {
            switch (attrib.Name.LocalName) {
                case DEPTH_ATTRIB:
                    tag.Depth = ushort.Parse(attrib.Value);
                    break;
                default:
                    var handled = AcceptPlaceAttribute(tag, attrib);
                    return handled;
            }
            return true;
        }

        protected sealed override void AcceptTagElement(T tag, XElement element) {
            switch (element.Name.LocalName) {
                case TRANSFORM_ELEM:
                    tag.Matrix = XMatrix.FromXml(element.Element(TRANSFORM_TYPE_ELEM));
                    HasMatrix(tag, true);
                    break;
                default:
                    AcceptPlaceTagElement(tag, element);
                    break;
            }
        }

        protected abstract void FormatPlaceElement(T tag, XElement elem);

        protected abstract bool HasCharacter(T tag);
        protected abstract void HasCharacter(T tag, bool val);

        protected abstract bool HasMatrix(T tag);
        protected abstract void HasMatrix(T tag, bool val);

        protected virtual bool AcceptPlaceAttribute(T tag, XAttribute attrib) {
            return false;
        }
        protected abstract void AcceptPlaceTagElement(T tag, XElement element);

        protected override ushort? GetObjectID(T tag) {
            if (!HasCharacter(tag)) return null;
            return tag.CharacterID;
        }

        protected override void SetObjectID(T tag, ushort value) {
            HasCharacter(tag, true);
            tag.CharacterID = value;
        }
    }
}
