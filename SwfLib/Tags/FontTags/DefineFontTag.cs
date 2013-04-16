﻿using System.Collections.Generic;

namespace Code.SwfLib.Tags.FontTags {
    public class DefineFontTag : FontBaseTag {

        public readonly IList<ushort> OffsetTable = new List<ushort>();

        public override SwfTagType TagType {
            get { return SwfTagType.DefineFont; }
        }

        public override TResult AcceptVistor<TArg, TResult>(ISwfTagVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }
    }
}