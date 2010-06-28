﻿using System;
using Code.SwfLib.Data.FillStyles;

namespace Code.SwfLib {
    public static class FillStyleStreamExt {

        public static void ReadToFillStyles1(this SwfStreamReader reader, FillStyles1List fillStyles) {
            byte count = reader.ReadByte();
            for (var i = 0; i < count; i++) {
                fillStyles.Add(reader.ReadFillStyle1());
            }
        }

        public static void WriteFillStyles1(this SwfStreamWriter writer, FillStyles1List styles) {
            byte cnt = (byte) styles.Count;
            //TODO: Check boundaries
            writer.WriteByte(cnt);
            for (var i = 0; i < cnt; i++) {
                var style = styles[i];
                writer.WriteFillStyle1(style);
            }
        }

        public static SolidRGBFillStyle ReadSolidRGBFillStyle(this SwfStreamReader reader) {
            var style = new SolidRGBFillStyle();
            style.Color = reader.ReadRGB();
            return style;
        }

        public static void WriteSolidRGBFillStyle(this SwfStreamWriter writer, SolidRGBFillStyle style) {
            writer.WriteByte(0x00);
            writer.WriteRGB(style.Color);
        }

        public static void WriteLinearGradientRGBFillStyle(this SwfStreamWriter writer, LinearGradientRGBFillStyle style) {
            writer.WriteByte(0x10);
            writer.WriteMatrix(style.GradientMatrix);
            throw new NotImplementedException();
        }

        public static LinearGradientRGBFillStyle ReadLinearGradientRGBFillStyle(this SwfStreamReader reader) {
            var style = new LinearGradientRGBFillStyle();
            style.GradientMatrix = reader.ReadMatrix();
            style.Gradient = reader.ReadGradientRGB();
            return style;
        }

        public static void WriteNonSmoothedClippedBitmapFillStyle(this SwfStreamWriter writer, NonSmoothedClippedBitmapFillStyle style) {
            writer.WriteByte(0x42);
            throw new NotImplementedException();
        }

        public static void WriteClippedBitmapFillStyle(this SwfStreamWriter writer, ClippedBitmapFillStyle style) {
            writer.WriteByte(0x41);
            writer.WriteUInt16(style.BitmapID);
            writer.WriteMatrix(style.BitmapMatrix);
        }

        public static ClippedBitmapFillStyle ReadClippedBitmapFillStyle(this SwfStreamReader reader) {
            var style = new ClippedBitmapFillStyle();
            style.BitmapID = reader.ReadUInt16();
            style.BitmapMatrix = reader.ReadMatrix();
            return style;
        }

        public static RepeatingBitmapFillStyle ReadRepeatingBitmapFillStyle(this SwfStreamReader reader) {
            var style = new RepeatingBitmapFillStyle();
            style.BitmapID = reader.ReadUInt16();
            style.BitmapMatrix = reader.ReadMatrix();
            return style;
        }


        public static NonSmoothedClippedBitmapFillStyle ReadNonSmoothedClippedBitmapFillStyle(this SwfStreamReader reader) {
            var style = new NonSmoothedClippedBitmapFillStyle();
            style.BitmapID = reader.ReadUInt16();
            style.BitmapMatrix = reader.ReadMatrix();
            return style;
        }

        public static FillStyle ReadFillStyle1(this SwfStreamReader reader) {
            var type = reader.ReadByte();
            switch (type) {
                //TODO: Use correct enum
                case 0x00:
                    return reader.ReadSolidRGBFillStyle();
                case 0x10:
                    return reader.ReadLinearGradientRGBFillStyle();
                case 0x41:
                    return reader.ReadClippedBitmapFillStyle();
                case 0x42:
                    return reader.ReadRepeatingBitmapFillStyle();
                case 0x43:
                    return reader.ReadNonSmoothedClippedBitmapFillStyle();
                default:
                    throw new NotSupportedException("Invalid fill style type " + type);
            }
        }

        public static void WriteFillStyle1(this SwfStreamWriter writer, FillStyle style) {
            switch (style.Type) {
                case FillStyleType.SolidRGB:
                    writer.WriteSolidRGBFillStyle((SolidRGBFillStyle)style);
                    break;
                case FillStyleType.ClippedBitmap:
                    writer.WriteClippedBitmapFillStyle((ClippedBitmapFillStyle)style);
                    break;
                case FillStyleType.LinearGradient:
                    writer.WriteLinearGradientRGBFillStyle((LinearGradientRGBFillStyle)style);
                    break;
                case FillStyleType.NonSmoothedClippedBitmap:
                    writer.WriteNonSmoothedClippedBitmapFillStyle((NonSmoothedClippedBitmapFillStyle)style);
                    break;
                default:
                    throw new InvalidOperationException("Unknown fill style type " + style.Type);
            }
        }

    }
}
