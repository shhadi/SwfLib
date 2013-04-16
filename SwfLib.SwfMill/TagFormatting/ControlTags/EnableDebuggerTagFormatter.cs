﻿using System.Xml.Linq;
using Code.SwfLib.Tags.ControlTags;

namespace Code.SwfLib.SwfMill.TagFormatting.ControlTags {
    public class EnableDebuggerTagFormatter : TagFormatterBase<EnableDebuggerTag> {
        
        protected override void FormatTagElement(EnableDebuggerTag tag, XElement xTag) {
        }

        public override string TagName {
            get { return "EnableDebugger"; }
        }
    }
}