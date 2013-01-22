﻿using System;
using Code.SwfLib.Data.Actions;

namespace Code.SwfLib.Actions {
    public class ActionsReader : IActionVisitor<ushort, ActionBase> {
        private readonly SwfStreamReader _reader;
        private readonly ActionsFactory _factory;
        public ActionsReader(SwfStreamReader reader) {
            _reader = reader;
            _factory = new ActionsFactory();
        }

        public ActionBase ReadAction() {
            var code = (ActionCode)_reader.ReadByte();
            ushort length = (byte)code >= 0x80 ? _reader.ReadUInt16() : (ushort)0;
            var action = _factory.Create(code);
            return action.AcceptVisitor(this, length);
        }

        #region SWF 3

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGotoFrame action, ushort length) {
            action.Frame = _reader.ReadUInt16(); 
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGetURL action, ushort length)
        {
            action.UrlString = _reader.ReadString();
            action.TargetString = _reader.ReadString() ; 
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionNextFrame action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionPreviousFrame action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionPlay action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStop action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionToggleQuality action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStopSounds action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionWaitForFrame action, ushort length) {
            action.Frame = _reader.ReadUInt16();
            action.SkipCount = _reader.ReadByte(); 
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionSetTarget action, ushort length) {
            action.TargetName = _reader.ReadString(); 
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGoToLabel action, ushort length) {
            action.Label = _reader.ReadString(); 
            return action;
        }

        #endregion

        #region SWF 4

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionAdd action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionDivide action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionMultiply action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionSubtract action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionEquals action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionLess action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionAnd action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionNot action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionOr action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStringAdd action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStringEquals action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStringExtract action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStringLength action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionMBStringExtract action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionMBStringLength action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStringLess action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionPop action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionPush action, ushort length) {
            var position = _reader.BaseStream.Position;
            while (_reader.BaseStream.Position - position < length) {
                var item = new ActionPushItem();
                var type = (ActionPushItemType)_reader.ReadByte();
                item.Type = type;
                switch (type) {
                    case ActionPushItemType.String:
                        item.String = _reader.ReadString();
                        break;
                    case ActionPushItemType.Float:
                        item.Float = _reader.ReadSingle();
                        break;
                    case ActionPushItemType.Undefined:
                        break;
                    case ActionPushItemType.Register:
                        item.Register = _reader.ReadByte();
                        break;
                    case ActionPushItemType.Boolean:
                        item.Boolean = _reader.ReadByte();
                        break;
                    case ActionPushItemType.Double:
                        item.Double = _reader.ReadDouble();
                        break;
                    case ActionPushItemType.Integer:
                        item.Integer = _reader.ReadInt32();
                        break;
                    case ActionPushItemType.Constant8:
                        item.Constant8 = _reader.ReadByte();
                        break;
                    case ActionPushItemType.Constant16:
                        item.Constant16 = _reader.ReadUInt16();
                        break;
                    default:
                        throw new NotSupportedException("Unknown PushData type " + type);
                }
                action.Items.Add(item);
            }
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionAsciiToChar action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionCharToAscii action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionToInteger action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionMBAsciiToChar action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionMBCharToAscii action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionCall action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionIf action, ushort length) {
            action.BranchOffset = _reader.ReadSInt16(); 
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionJump action, ushort length) {
            action.BranchOffset = _reader.ReadSInt16();
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGetVariable action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionSetVariable action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGetURL2 action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGetProperty action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGotoFrame2 action, ushort length) {
            action.Flags = _reader.ReadByte();
            if (action.SceneBiasFlag) {
                action.SceneBias = _reader.ReadUInt16();
            }
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionRemoveSprite action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionSetProperty action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionSetTarget2 action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStartDrag action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionWaitForFrame2 action, ushort length) {
            action.SkipCount = _reader.ReadByte();
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionCloneSprite action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionEndDrag action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGetTime action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionRandomNumber action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionTrace action, ushort length) {
            return action;
        }

        #endregion

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionInstanceOf action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionEnumerate2 action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStrictEquals action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionGreater action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionStringGreater action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionDefineFunction2 action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionExtends action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionCastOp action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionImplementsOp action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionTry action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionThrow action, ushort length) {
            return new ActionThrow();
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionConstantPool action, ushort length) {
            ushort count = _reader.ReadUInt16();
            var pool = new string[count];
            for (var i = 0; i < count; i++) {
                pool[i] = _reader.ReadString();
            }
            action.ConstantPool = pool;
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionDefineFunction action, ushort length) {
            string name = _reader.ReadString();
            ushort count = _reader.ReadUInt16();
            var parameters = new string[count];
            for (var i = 0; i < count; i++) {
                parameters[i] = _reader.ReadString();
            }
            ushort bodySize = _reader.ReadUInt16();
            action.FunctionName = name;
            action.Params = parameters;
            action.Body = _reader.ReadBytes(bodySize);
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionReturn action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionSetMember action, ushort length) {
            return action;
        }

        ActionBase IActionVisitor<ushort, ActionBase>.Visit(ActionUnknown action, ushort length) {
            return action;
        }
    }
}
