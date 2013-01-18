﻿namespace Code.SwfLib.Tags {
    public abstract class SwfTagBase {

        public abstract SwfTagType TagType { get; }

        /// <summary>
        /// Gets or sets rest data that is not parsed into properties.
        /// </summary>
        public byte[] RestData;

        public abstract object AcceptVistor(ISwfTagVisitor visitor);

        public abstract TResult AcceptVistor<TArg, TResult>(ISwfTagVisitor<TArg, TResult> visitor, TArg arg);

    }
}
