using SharpEngine.Utils.Math;
using System;

namespace SharpEngine.Utils;

/// <summary>
/// Couleur
/// </summary>
public struct Color
{
    private int _internalR;
    private int _internalG;
    private int _internalB;
    private int _internalA;

    public int R
    {
        get => _internalR;
        set => _internalR = MathUtils.Clamp(value, 0, 255);
    }
    public int G
    {
        get => _internalG;
        set => _internalG = MathUtils.Clamp(value, 0, 255);
    }
    public int B
    {
        get => _internalB;
        set => _internalB = MathUtils.Clamp(value, 0, 255);
    }
    public int A
    {
        get => _internalA;
        set => _internalA = MathUtils.Clamp(value, 0, 255);
    }

    public Color(int r, int g, int b) 
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }

    public Color(int r, int g, int b, int a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Microsoft.Xna.Framework.Color ToMg() => new(R, G, B, A);

    public override bool Equals(object obj)
    {
        if (obj is Color color)
            return this == color;
        return obj != null && obj.Equals(this);
    }
    public override int GetHashCode() => HashCode.Combine(_internalR, _internalG, _internalB, _internalA);

    public override string ToString() => $"Color(r={R}, g={G}, b={B}, a={A})";

    public static bool operator !=(Color color, Color color2) => !(color == color2);
    public static bool operator ==(Color color, Color color2) =>
        color.R == color2.R && color.G == color2.G && color.B == color2.B && color.A == color2.A;

    public static Color GetColorBetween(Color startColor, Color endColor, float currentTime, float maxTime)
    {
        var stepA = (endColor.A - startColor.A) * currentTime / maxTime;
        var stepR = (endColor.R - startColor.R) * currentTime / maxTime;
        var stepG = (endColor.G - startColor.G) * currentTime / maxTime;
        var stepB = (endColor.B - startColor.B) * currentTime / maxTime;

        return new Color(
            startColor.R + Convert.ToInt32(stepR), 
            startColor.G + Convert.ToInt32(stepG),
            startColor.B + Convert.ToInt32(stepB),
            startColor.A + Convert.ToInt32(stepA));
    }

    public static readonly Color MediumAquamarine = new(102, 205, 170, 255);
    public static readonly Color MediumBlue = new(0, 0, 205, 255);
    public static readonly Color MediumOrquid = new(186, 85, 211, 255);
    public static readonly Color MediumPurple = new(147, 112, 219, 255);
    public static readonly Color MediumSeaGreen = new(60, 179, 113, 255);
    public static readonly Color MediumSlateBlue = new(123, 104, 238, 255);
    public static readonly Color MediumSpringGreen = new(0, 250, 154, 255);
    public static readonly Color MediumTurquoise = new(72, 209, 204, 255);
    public static readonly Color Moccasin = new(255, 228, 181, 255);
    public static readonly Color MidnightBlue = new(25, 25, 112, 255);
    public static readonly Color MintCream = new(245, 255, 250, 255);
    public static readonly Color MistyRose = new(255, 228, 225, 255);
    public static readonly Color Maroon = new(128, 0, 0, 255);
    public static readonly Color Theme = new(231, 60, 0, 255);
    public static readonly Color NavajoWhite = new(255, 222, 173, 255);
    public static readonly Color Navy = new(0, 0, 128, 255);
    public static readonly Color OldLace = new(253, 245, 230, 255);
    public static readonly Color MediumVioletRed = new(199, 21, 133, 255);
    public static readonly Color Magenta = new(255, 0, 255, 255);
    public static readonly Color LightSteelBlue = new(176, 196, 222, 255);
    public static readonly Color LimeGreen = new(50, 205, 50, 255);
    public static readonly Color LavenderBlush = new(255, 240, 245, 255);
    public static readonly Color LawnGreen = new(124, 252, 0, 255);
    public static readonly Color LemonChiffon = new(255, 250, 205, 255);
    public static readonly Color LightBlue = new(173, 216, 230, 255);
    public static readonly Color LightCoral = new(240, 128, 128, 255);
    public static readonly Color LightCyan = new(224, 255, 255, 255);
    public static readonly Color LightGoldenrodYellow = new(250, 250, 210, 255);
    public static readonly Color LightGray = new(211, 211, 211, 255);
    public static readonly Color LightGreen = new(144, 238, 144, 255);
    public static readonly Color LightPink = new(255, 182, 193, 255);
    public static readonly Color LightSalmon = new(255, 160, 122, 255);
    public static readonly Color LightSeaGreen = new(32, 178, 170, 255);
    public static readonly Color LightSkyBlue = new(135, 206, 250, 255);
    public static readonly Color LightSlateGray = new(119, 136, 153, 255);
    public static readonly Color Olive = new(128, 128, 0, 255);
    public static readonly Color LightYellow = new(255, 255, 224, 255);
    public static readonly Color Lime = new(0, 255, 0, 255);
    public static readonly Color Linen = new(250, 240, 230, 255);
    public static readonly Color OliveDrab = new(107, 142, 35, 255);
    public static readonly Color PaleGreen = new(152, 251, 152, 255);
    public static readonly Color OrangeRed = new(255, 69, 0, 255);
    public static readonly Color Silver = new(192, 192, 192, 255);
    public static readonly Color SkyBlue = new(135, 206, 235, 255);
    public static readonly Color SlateBlue = new(106, 90, 205, 255);
    public static readonly Color SlateGray = new(112, 128, 144, 255);
    public static readonly Color Snow = new(255, 250, 250, 255);
    public static readonly Color SpringGreen = new(0, 255, 127, 255);
    public static readonly Color SteelBlue = new(70, 130, 180, 255);
    public static readonly Color Tan = new(210, 180, 140, 255);
    public static readonly Color Teal = new(0, 128, 128, 255);
    public static readonly Color Thistle = new(216, 191, 216, 255);
    public static readonly Color Tomato = new(255, 99, 71, 255);
    public static readonly Color Turquoise = new(64, 224, 208, 255);
    public static readonly Color Violet = new(238, 130, 238, 255);
    public static readonly Color Wheat = new(245, 222, 179, 255);
    public static readonly Color White = new(255, 255, 255, 255);
    public static readonly Color WhiteSmoke = new(245, 245, 245, 255);
    public static readonly Color Yellow = new(255, 255, 0, 255);
    public static readonly Color Sienna = new(160, 82, 45, 255);
    public static readonly Color Orange = new(255, 165, 0, 255);
    public static readonly Color SeaShell = new(255, 245, 238, 255);
    public static readonly Color SandyBrown = new(244, 164, 96, 255);
    public static readonly Color Orchid = new(218, 112, 214, 255);
    public static readonly Color PaleGoldenrod = new(238, 232, 170, 255);
    public static readonly Color Lavender = new(230, 230, 250, 255);
    public static readonly Color PaleTurquoise = new(175, 238, 238, 255);
    public static readonly Color PaleVioletRed = new(219, 112, 147, 255);
    public static readonly Color PapayaWhip = new(255, 239, 213, 255);
    public static readonly Color PeachPuff = new(255, 218, 185, 255);
    public static readonly Color Peru = new(205, 133, 63, 255);
    public static readonly Color Pink = new(255, 192, 203, 255);
    public static readonly Color Plum = new(221, 160, 221, 255);
    public static readonly Color PowderBlue = new(176, 224, 230, 255);
    public static readonly Color Purple = new(128, 0, 128, 255);
    public static readonly Color Red = new(255, 0, 0, 255);
    public static readonly Color RosyBrown = new(188, 143, 143, 255);
    public static readonly Color RoyalBlue = new(65, 105, 225, 255);
    public static readonly Color SaddleBrown = new(139, 69, 19, 255);
    public static readonly Color Salmon = new(250, 128, 114, 255);
    public static readonly Color SeaGreen = new(46, 139, 87, 255);
    public static readonly Color Khaki = new(240, 230, 140, 255);
    public static readonly Color Honeydew = new(240, 255, 240, 255);
    public static readonly Color Indigo = new(75, 0, 130, 255);
    public static readonly Color DarkGray = new(169, 169, 169, 255);
    public static readonly Color DarkGoldenrod = new(184, 134, 11, 255);
    public static readonly Color DarkCyan = new(0, 139, 139, 255);
    public static readonly Color DarkBlue = new(0, 0, 139, 255);
    public static readonly Color Cyan = new(0, 255, 255, 255);
    public static readonly Color Crimson = new(220, 20, 60, 255);
    public static readonly Color Cornsilk = new(255, 248, 220, 255);
    public static readonly Color CornflowerBlue = new(100, 149, 237, 255);
    public static readonly Color Coral = new(255, 127, 80, 255);
    public static readonly Color Ivory = new(255, 255, 240, 255);
    public static readonly Color Chartreuse = new(127, 255, 0, 255);
    public static readonly Color CadetBlue = new(95, 158, 160, 255);
    public static readonly Color BurlyWood = new(222, 184, 135, 255);
    public static readonly Color Brown = new(165, 42, 42, 255);
    public static readonly Color BlueViolet = new(138, 43, 226, 255);
    public static readonly Color Blue = new(0, 0, 255, 255);
    public static readonly Color BlanchedAlmond = new(255, 235, 205, 255);
    public static readonly Color Black = new(0, 0, 0, 255);
    public static readonly Color Bisque = new(255, 228, 196, 255);
    public static readonly Color Beige = new(245, 245, 220, 255);
    public static readonly Color Azure = new(240, 255, 255, 255);
    public static readonly Color Aquamarine = new(127, 255, 212, 255);
    public static readonly Color Aqua = new(0, 255, 255, 255);
    public static readonly Color AntiqueWhite = new(250, 235, 215, 255);
    public static readonly Color AliceBlue = new(240, 248, 255, 255);
    public static readonly Color Transparent = new(0, 0, 0, 0);
    public static readonly Color DarkGreen = new(0, 100, 0, 255);
    public static readonly Color DarkKhaki = new(189, 183, 107, 255);
    public static readonly Color Chocolate = new(210, 105, 30, 255);
    public static readonly Color DarkOliveGreen = new(85, 107, 47, 255);
    public static readonly Color IndianRed = new(205, 92, 92, 255);
    public static readonly Color DarkMagenta = new(139, 0, 139, 255);
    public static readonly Color YellowGreen = new(154, 205, 50, 255);
    public static readonly Color GreenYellow = new(173, 255, 47, 255);
    public static readonly Color Green = new(0, 128, 0, 255);
    public static readonly Color Gray = new(128, 128, 128, 255);
    public static readonly Color Goldenrod = new(218, 165, 32, 255);
    public static readonly Color Gold = new(255, 215, 0, 255);
    public static readonly Color GhostWhite = new(248, 248, 255, 255);
    public static readonly Color Gainsboro = new(220, 220, 220, 255);
    public static readonly Color Fuchsia = new(255, 0, 255, 255);
    public static readonly Color ForestGreen = new(34, 139, 34, 255);
    public static readonly Color FloralWhite = new(255, 250, 240, 255);
    public static readonly Color HotPink = new(255, 105, 180, 255);
    public static readonly Color DodgerBlue = new(30, 144, 255, 255);
    public static readonly Color DimGray = new(105, 105, 105, 255);
    public static readonly Color DeepSkyBlue = new(0, 191, 255, 255);
    public static readonly Color DeepPink = new(255, 20, 147, 255);
    public static readonly Color DarkViolet = new(148, 0, 211, 255);
    public static readonly Color DarkTurquoise = new(0, 206, 209, 255);
    public static readonly Color DarkSlateGray = new(47, 79, 79, 255);
    public static readonly Color DarkSlateBlue = new(72, 61, 139, 255);
    public static readonly Color DarkSeaGreen = new(143, 188, 139, 255);
    public static readonly Color DarkSalmon = new(233, 150, 122, 255);
    public static readonly Color DarkRed = new(139, 0, 0, 255);
    public static readonly Color DarkOrchid = new(153, 50, 204, 255);
    public static readonly Color DarkOrange = new(255, 140, 0, 255);
    public static readonly Color Firebrick = new(178, 34, 34, 255);
}
