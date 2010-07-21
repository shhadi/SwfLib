﻿namespace Code.SwfLib.Data.Actions {
    public class ActionSetVariable : ActionBase {

        public override ActionCode ActionCode {
            get { return ActionCode.SetVariable; }
        }

        public override object AcceptVisitor(IActionVisitor visitor) {
            return visitor.Visit(this);
        }

    }
}