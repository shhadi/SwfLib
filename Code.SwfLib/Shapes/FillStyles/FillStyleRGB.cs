﻿using Code.SwfLib.Data;
using Code.SwfLib.Gradients;
using Code.SwfLib.Shapes.FillStyles;

namespace Code.SwfLib.Tags.ShapeTags {
    public struct FillStyleRGB {

        public FillStyleType FillStyleType;

        public SwfRGB Color;

        public SwfMatrix GradientMatrix;

        public GradientRGB Gradient;

        public FocalGradient FocalGradient;

        public ushort BitmapID;

        public SwfMatrix BitmapMatrix;

    }
}