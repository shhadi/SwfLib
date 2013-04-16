﻿namespace Code.SwfLib.Actions {
    public class ActionGetProperty : ActionBase {

        public override ActionCode ActionCode {
            get { return ActionCode.GetProperty; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IActionVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}