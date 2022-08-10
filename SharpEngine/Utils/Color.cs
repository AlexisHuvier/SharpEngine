using SharpEngine.Utils.Math;
using System;

namespace SharpEngine.Utils;

/// <summary>
/// Couleur
/// </summary>
public class Color
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

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => $"Color(r={R}, g={G}, b={B}, a={A})";

    public static bool operator !=(Color color, Color color2) => !(color == color2);

    public static bool operator ==(Color color, Color color2)
    {
        if (color is null)
            return color2 is null;
        if (color2 is null)
            return false;
        return color.R == color2.R && color.G == color2.G && color.B == color2.B && color.A == color2.A;
    }

    public static Color GetColorBetween(Color startColor, Color endColor, float currentTime, float maxTime)
    {
        float stepA = ((endColor.A - startColor.A) * currentTime / maxTime);
        float stepR = ((endColor.R - startColor.R) * currentTime / maxTime);
        float stepG = ((endColor.G - startColor.G) * currentTime / maxTime);
        float stepB = ((endColor.B - startColor.B) * currentTime / maxTime);

        return new Color(startColor.R + Convert.ToInt32(stepR), startColor.G + Convert.ToInt32(stepG), startColor.B + Convert.ToInt32(stepB),
            startColor.A + Convert.ToInt32(stepA));
    }

    public static readonly Color MediumAquamarine = new Color(102, 205, 170, 255);
    public static readonly Color MediumBlue = new Color(0, 0, 205, 255);
    public static readonly Color MediumOrquid = new Color(186, 85, 211, 255);
    public static readonly Color MediumPurple = new Color(147, 112, 219, 255);
    public static readonly Color MediumSeaGreen = new Color(60, 179, 113, 255);
    public static readonly Color MediumSlateBlue = new Color(123, 104, 238, 255);
    public static readonly Color MediumSpringGreen = new Color(0, 250, 154, 255);
    public static readonly Color MediumTurquoise = new Color(72, 209, 204, 255);
    public static readonly Color Moccasin = new Color(255, 228, 181, 255);
    public static readonly Color MidnightBlue = new Color(25, 25, 112, 255);
    public static readonly Color MintCream = new Color(245, 255, 250, 255);
    public static readonly Color MistyRose = new Color(255, 228, 225, 255);
    public static readonly Color Maroon = new Color(128, 0, 0, 255);
    public static readonly Color Theme = new Color(231, 60, 0, 255);
    public static readonly Color NavajoWhite = new Color(255, 222, 173, 255);
    public static readonly Color Navy = new Color(0, 0, 128, 255);
    public static readonly Color OldLace = new Color(253, 245, 230, 255);
    public static readonly Color MediumVioletRed = new Color(199, 21, 133, 255);
    public static readonly Color Magenta = new Color(255, 0, 255, 255);
    public static readonly Color LightSteelBlue = new Color(176, 196, 222, 255);
    public static readonly Color LimeGreen = new Color(50, 205, 50, 255);
    public static readonly Color LavenderBlush = new Color(255, 240, 245, 255);
    public static readonly Color LawnGreen = new Color(124, 252, 0, 255);
    public static readonly Color LemonChiffon = new Color(255, 250, 205, 255);
    public static readonly Color LightBlue = new Color(173, 216, 230, 255);
    public static readonly Color LightCoral = new Color(240, 128, 128, 255);
    public static readonly Color LightCyan = new Color(224, 255, 255, 255);
    public static readonly Color LightGoldenrodYellow = new Color(250, 250, 210, 255);
    public static readonly Color LightGray = new Color(211, 211, 211, 255);
    public static readonly Color LightGreen = new Color(144, 238, 144, 255);
    public static readonly Color LightPink = new Color(255, 182, 193, 255);
    public static readonly Color LightSalmon = new Color(255, 160, 122, 255);
    public static readonly Color LightSeaGreen = new Color(32, 178, 170, 255);
    public static readonly Color LightSkyBlue = new Color(135, 206, 250, 255);
    public static readonly Color LightSlateGray = new Color(119, 136, 153, 255);
    public static readonly Color Olive = new Color(128, 128, 0, 255);
    public static readonly Color LightYellow = new Color(255, 255, 224, 255);
    public static readonly Color Lime = new Color(0, 255, 0, 255);
    public static readonly Color Linen = new Color(250, 240, 230, 255);
    public static readonly Color OliveDrab = new Color(107, 142, 35, 255);
    public static readonly Color PaleGreen = new Color(152, 251, 152, 255);
    public static readonly Color OrangeRed = new Color(255, 69, 0, 255);
    public static readonly Color Silver = new Color(192, 192, 192, 255);
    public static readonly Color SkyBlue = new Color(135, 206, 235, 255);
    public static readonly Color SlateBlue = new Color(106, 90, 205, 255);
    public static readonly Color SlateGray = new Color(112, 128, 144, 255);
    public static readonly Color Snow = new Color(255, 250, 250, 255);
    public static readonly Color SpringGreen = new Color(0, 255, 127, 255);
    public static readonly Color SteelBlue = new Color(70, 130, 180, 255);
    public static readonly Color Tan = new Color(210, 180, 140, 255);
    public static readonly Color Teal = new Color(0, 128, 128, 255);
    public static readonly Color Thistle = new Color(216, 191, 216, 255);
    public static readonly Color Tomato = new Color(255, 99, 71, 255);
    public static readonly Color Turquoise = new Color(64, 224, 208, 255);
    public static readonly Color Violet = new Color(238, 130, 238, 255);
    public static readonly Color Wheat = new Color(245, 222, 179, 255);
    public static readonly Color White = new Color(255, 255, 255, 255);
    public static readonly Color WhiteSmoke = new Color(245, 245, 245, 255);
    public static readonly Color Yellow = new Color(255, 255, 0, 255);
    public static readonly Color Sienna = new Color(160, 82, 45, 255);
    public static readonly Color Orange = new Color(255, 165, 0, 255);
    public static readonly Color SeaShell = new Color(255, 245, 238, 255);
    public static readonly Color SandyBrown = new Color(244, 164, 96, 255);
    public static readonly Color Orchid = new Color(218, 112, 214, 255);
    public static readonly Color PaleGoldenrod = new Color(238, 232, 170, 255);
    public static readonly Color Lavender = new Color(230, 230, 250, 255);
    public static readonly Color PaleTurquoise = new Color(175, 238, 238, 255);
    public static readonly Color PaleVioletRed = new Color(219, 112, 147, 255);
    public static readonly Color PapayaWhip = new Color(255, 239, 213, 255);
    public static readonly Color PeachPuff = new Color(255, 218, 185, 255);
    public static readonly Color Peru = new Color(205, 133, 63, 255);
    public static readonly Color Pink = new Color(255, 192, 203, 255);
    public static readonly Color Plum = new Color(221, 160, 221, 255);
    public static readonly Color PowderBlue = new Color(176, 224, 230, 255);
    public static readonly Color Purple = new Color(128, 0, 128, 255);
    public static readonly Color Red = new Color(255, 0, 0, 255);
    public static readonly Color RosyBrown = new Color(188, 143, 143, 255);
    public static readonly Color RoyalBlue = new Color(65, 105, 225, 255);
    public static readonly Color SaddleBrown = new Color(139, 69, 19, 255);
    public static readonly Color Salmon = new Color(250, 128, 114, 255);
    public static readonly Color SeaGreen = new Color(46, 139, 87, 255);
    public static readonly Color Khaki = new Color(240, 230, 140, 255);
    public static readonly Color Honeydew = new Color(240, 255, 240, 255);
    public static readonly Color Indigo = new Color(75, 0, 130, 255);
    public static readonly Color DarkGray = new Color(169, 169, 169, 255);
    public static readonly Color DarkGoldenrod = new Color(184, 134, 11, 255);
    public static readonly Color DarkCyan = new Color(0, 139, 139, 255);
    public static readonly Color DarkBlue = new Color(0, 0, 139, 255);
    public static readonly Color Cyan = new Color(0, 255, 255, 255);
    public static readonly Color Crimson = new Color(220, 20, 60, 255);
    public static readonly Color Cornsilk = new Color(255, 248, 220, 255);
    public static readonly Color CornflowerBlue = new Color(100, 149, 237, 255);
    public static readonly Color Coral = new Color(255, 127, 80, 255);
    public static readonly Color Ivory = new Color(255, 255, 240, 255);
    public static readonly Color Chartreuse = new Color(127, 255, 0, 255);
    public static readonly Color CadetBlue = new Color(95, 158, 160, 255);
    public static readonly Color BurlyWood = new Color(222, 184, 135, 255);
    public static readonly Color Brown = new Color(165, 42, 42, 255);
    public static readonly Color BlueViolet = new Color(138, 43, 226, 255);
    public static readonly Color Blue = new Color(0, 0, 255, 255);
    public static readonly Color BlanchedAlmond = new Color(255, 235, 205, 255);
    public static readonly Color Black = new Color(0, 0, 0, 255);
    public static readonly Color Bisque = new Color(255, 228, 196, 255);
    public static readonly Color Beige = new Color(245, 245, 220, 255);
    public static readonly Color Azure = new Color(240, 255, 255, 255);
    public static readonly Color Aquamarine = new Color(127, 255, 212, 255);
    public static readonly Color Aqua = new Color(0, 255, 255, 255);
    public static readonly Color AntiqueWhite = new Color(250, 235, 215, 255);
    public static readonly Color AliceBlue = new Color(240, 248, 255, 255);
    public static readonly Color Transparent = new Color(0, 0, 0, 0);
    public static readonly Color DarkGreen = new Color(0, 100, 0, 255);
    public static readonly Color DarkKhaki = new Color(189, 183, 107, 255);
    public static readonly Color Chocolate = new Color(210, 105, 30, 255);
    public static readonly Color DarkOliveGreen = new Color(85, 107, 47, 255);
    public static readonly Color IndianRed = new Color(205, 92, 92, 255);
    public static readonly Color DarkMagenta = new Color(139, 0, 139, 255);
    public static readonly Color YellowGreen = new Color(154, 205, 50, 255);
    public static readonly Color GreenYellow = new Color(173, 255, 47, 255);
    public static readonly Color Green = new Color(0, 128, 0, 255);
    public static readonly Color Gray = new Color(128, 128, 128, 255);
    public static readonly Color Goldenrod = new Color(218, 165, 32, 255);
    public static readonly Color Gold = new Color(255, 215, 0, 255);
    public static readonly Color GhostWhite = new Color(248, 248, 255, 255);
    public static readonly Color Gainsboro = new Color(220, 220, 220, 255);
    public static readonly Color Fuchsia = new Color(255, 0, 255, 255);
    public static readonly Color ForestGreen = new Color(34, 139, 34, 255);
    public static readonly Color FloralWhite = new Color(255, 250, 240, 255);
    public static readonly Color HotPink = new Color(255, 105, 180, 255);
    public static readonly Color DodgerBlue = new Color(30, 144, 255, 255);
    public static readonly Color DimGray = new Color(105, 105, 105, 255);
    public static readonly Color DeepSkyBlue = new Color(0, 191, 255, 255);
    public static readonly Color DeepPink = new Color(255, 20, 147, 255);
    public static readonly Color DarkViolet = new Color(148, 0, 211, 255);
    public static readonly Color DarkTurquoise = new Color(0, 206, 209, 255);
    public static readonly Color DarkSlateGray = new Color(47, 79, 79, 255);
    public static readonly Color DarkSlateBlue = new Color(72, 61, 139, 255);
    public static readonly Color DarkSeaGreen = new Color(143, 188, 139, 255);
    public static readonly Color DarkSalmon = new Color(233, 150, 122, 255);
    public static readonly Color DarkRed = new Color(139, 0, 0, 255);
    public static readonly Color DarkOrchid = new Color(153, 50, 204, 255);
    public static readonly Color DarkOrange = new Color(255, 140, 0, 255);
    public static readonly Color Firebrick = new Color(178, 34, 34, 255);
}
