﻿namespace Code.SwfLib.Actions {
    public class ActionRemoveSprite : ActionBase {

        public override ActionCode ActionCode {
            get { return ActionCode.RemoveSprite; }
        }

        public override TResult AcceptVisitor<TArg, TResult>(IActionVisitor<TArg, TResult> visitor, TArg arg) {
            return visitor.Visit(this, arg);
        }

    }
}