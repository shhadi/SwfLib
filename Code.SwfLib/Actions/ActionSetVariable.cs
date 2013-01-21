﻿using Code.SwfLib.Actions;

namespace Code.SwfLib.Data.Actions {
    public class ActionSetVariable : ActionBase {

        public override ActionCode ActionCode {
            get { return ActionCode.SetVariable; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IActionVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
