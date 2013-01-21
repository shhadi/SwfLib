﻿using Code.SwfLib.Actions;

namespace Code.SwfLib.Data.Actions {
    public class ActionSetTarget : ActionBase {

        public string TargetName;

        public override ActionCode ActionCode {
            get { return ActionCode.SetTarget; }
        }

        public override object AcceptVisitor(IActionVisitor visitor) {
            return visitor.Visit(this);
        }
    }
}