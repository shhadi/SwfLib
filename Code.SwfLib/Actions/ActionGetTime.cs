﻿using Code.SwfLib.Actions;

namespace Code.SwfLib.Data.Actions {
    public class ActionGetTime : ActionBase {

        public override ActionCode ActionCode {
            get { return ActionCode.GetTime; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IActionVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}
