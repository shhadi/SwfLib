﻿using System.Xml.Linq;
using Code.SwfLib.Filters;
using Code.SwfLib.SwfMill.Data;

namespace Code.SwfLib.SwfMill.Filters {
    public static class XDropShadowFilter {

        public const string TAG_NAME = "DropShadow";

        public static XElement ToXml(DropShadowFilter filter) {
            return new XElement(TAG_NAME,
                new XAttribute("angle", CommonFormatter.Format(filter.Angle)),
                new XAttribute("blurX", CommonFormatter.Format(filter.BlurX)),
                new XAttribute("blurY", CommonFormatter.Format(filter.BlurY)),
                new XAttribute("distance", CommonFormatter.Format(filter.Distance)),
                new XAttribute("innerShadow", CommonFormatter.Format(filter.InnerShadow)),
                new XAttribute("knockout", CommonFormatter.Format(filter.Knockout)),
                new XAttribute("compositeSource", CommonFormatter.Format(filter.CompositeSource)),
                new XAttribute("passes", filter.Passes),
                new XAttribute("strength", CommonFormatter.Format(filter.Strength)),
                new XElement("color", XColorRGBA.ToXml(filter.Color))
            );
        }

        public static DropShadowFilter FromXml(XElement xFilter) {
            var xAngle = xFilter.Attribute("angle");
            var xBlurX = xFilter.Attribute("blurX");
            var xBlurY = xFilter.Attribute("blurY");
            var xDistance = xFilter.Attribute("distance");
            var xInnerShadow = xFilter.Attribute("innerShadow");
            var xKnockout = xFilter.Attribute("knockout");
            var xPasses = xFilter.Attribute("passes");
            var xStrength = xFilter.Attribute("strength");
            var xCompositeSource = xFilter.Attribute("compositeSource");

            var xColor = xFilter.Element("color").Element("Color");

            return new DropShadowFilter {
                Angle = CommonFormatter.ParseDouble(xAngle.Value),
                BlurX = CommonFormatter.ParseDouble(xBlurX.Value),
                BlurY = CommonFormatter.ParseDouble(xBlurY.Value),
                Distance = CommonFormatter.ParseDouble(xDistance.Value),
                InnerShadow = CommonFormatter.ParseBool(xInnerShadow.Value),
                Knockout = CommonFormatter.ParseBool(xKnockout.Value),
                Passes = uint.Parse(xPasses.Value),
                Strength = CommonFormatter.ParseDouble(xStrength.Value),
                CompositeSource = xCompositeSource == null || CommonFormatter.ParseBool(xCompositeSource.Value),
                Color = XColorRGBA.FromXml(xColor)
            };
        }
    }
}
