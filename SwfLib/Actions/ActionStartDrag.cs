﻿using SwfLib.Actions;

namespace Code.SwfLib.Actions {
    public class ActionStartDrag : ActionBase {

        public override ActionCode ActionCode {
            get { return ActionCode.StartDrag; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IActionVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
