namespace SharpEngine
{
    /// <summary>
    /// Couleur
    /// </summary>
    public class Color
    {
        private int internalR;
        private int internalG;
        private int internalB;
        private int internalA;

        public int r
        {
            get => internalR;
            set => internalR = Math.Clamp(value, 0, 255);
        }
        public int g
        {
            get => internalG;
            set => internalG = Math.Clamp(value, 0, 255);
        }
        public int b
        {
            get => internalB;
            set => internalB = Math.Clamp(value, 0, 255);
        }
        public int a
        {
            get => internalA;
            set => internalA = Math.Clamp(value, 0, 255);
        }

        public Color(int r, int g, int b) 
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 255;
        }

        public Color(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Microsoft.Xna.Framework.Color ToMG()
        {
            return new Microsoft.Xna.Framework.Color(r, g, b, a);
        }

        public override bool Equals(object obj)
        {
            if (obj is Color color)
                return this == color;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Color(r={r}, g={g}, b={b}, a={a})";
        }

        public static bool operator !=(Color color, Color color2)
            => !(color == color2);

        public static bool operator ==(Color color, Color color2)
        {
            if (color is null)
                return color2 is null;
            else if (color2 is null)
                return false;
            else
                return color.r == color2.r && color.g == color2.g && color.b == color2.b && color.a == color2.a;
        }

        public static readonly Color MEDIUM_AQUAMARINE = new Color(102, 205, 170, 255);
        public static readonly Color MEDIUM_BLUE = new Color(0, 0, 205, 255);
        public static readonly Color MEDIUM_ORQUID = new Color(186, 85, 211, 255);
        public static readonly Color MEDIUM_PURPLE = new Color(147, 112, 219, 255);
        public static readonly Color MEDIUM_SEA_GREEN = new Color(60, 179, 113, 255);
        public static readonly Color MEDIUM_SLATE_BLUE = new Color(123, 104, 238, 255);
        public static readonly Color MEDIUM_SPRING_GREEN = new Color(0, 250, 154, 255);
        public static readonly Color MEDIUM_TURQUOISE = new Color(72, 209, 204, 255);
        public static readonly Color MOCCASIN = new Color(255, 228, 181, 255);
        public static readonly Color MIDNIGHT_BLUE = new Color(25, 25, 112, 255);
        public static readonly Color MINT_CREAM = new Color(245, 255, 250, 255);
        public static readonly Color MISTY_ROSE = new Color(255, 228, 225, 255);
        public static readonly Color MAROON = new Color(128, 0, 0, 255);
        public static readonly Color THEME = new Color(231, 60, 0, 255);
        public static readonly Color NAVAJO_WHITE = new Color(255, 222, 173, 255);
        public static readonly Color NAVY = new Color(0, 0, 128, 255);
        public static readonly Color OLD_LACE = new Color(253, 245, 230, 255);
        public static readonly Color MEDIUM_VIOLET_RED = new Color(199, 21, 133, 255);
        public static readonly Color MAGENTA = new Color(255, 0, 255, 255);
        public static readonly Color LIGHT_STEEL_BLUE = new Color(176, 196, 222, 255);
        public static readonly Color LIME_GREEN = new Color(50, 205, 50, 255);
        public static readonly Color LAVENDER_BLUSH = new Color(255, 240, 245, 255);
        public static readonly Color LAWN_GREEN = new Color(124, 252, 0, 255);
        public static readonly Color LEMON_CHIFFON = new Color(255, 250, 205, 255);
        public static readonly Color LIGHT_BLUE = new Color(173, 216, 230, 255);
        public static readonly Color LIGHT_CORAL = new Color(240, 128, 128, 255);
        public static readonly Color LIGHT_CYAN = new Color(224, 255, 255, 255);
        public static readonly Color LIGHT_GOLDENROD_YELLOW = new Color(250, 250, 210, 255);
        public static readonly Color LIGHT_GRAY = new Color(211, 211, 211, 255);
        public static readonly Color LIGHT_GREEN = new Color(144, 238, 144, 255);
        public static readonly Color LIGHT_PINK = new Color(255, 182, 193, 255);
        public static readonly Color LIGHT_SALMON = new Color(255, 160, 122, 255);
        public static readonly Color LIGHT_SEA_GREEN = new Color(32, 178, 170, 255);
        public static readonly Color LIGHT_SKY_BLUE = new Color(135, 206, 250, 255);
        public static readonly Color LIGHT_SLATE_GRAY = new Color(119, 136, 153, 255);
        public static readonly Color OLIVE = new Color(128, 128, 0, 255);
        public static readonly Color LIGHT_YELLOW = new Color(255, 255, 224, 255);
        public static readonly Color LIME = new Color(0, 255, 0, 255);
        public static readonly Color LINEN = new Color(250, 240, 230, 255);
        public static readonly Color OLIVE_DRAB = new Color(107, 142, 35, 255);
        public static readonly Color PALE_GREEN = new Color(152, 251, 152, 255);
        public static readonly Color ORANGE_RED = new Color(255, 69, 0, 255);
        public static readonly Color SILVER = new Color(192, 192, 192, 255);
        public static readonly Color SKY_BLUE = new Color(135, 206, 235, 255);
        public static readonly Color SLATE_BLUE = new Color(106, 90, 205, 255);
        public static readonly Color SLATE_GRAY = new Color(112, 128, 144, 255);
        public static readonly Color SNOW = new Color(255, 250, 250, 255);
        public static readonly Color SPRING_GREEN = new Color(0, 255, 127, 255);
        public static readonly Color STEEL_BLUE = new Color(70, 130, 180, 255);
        public static readonly Color TAN = new Color(210, 180, 140, 255);
        public static readonly Color TEAL = new Color(0, 128, 128, 255);
        public static readonly Color THISTLE = new Color(216, 191, 216, 255);
        public static readonly Color TOMATO = new Color(255, 99, 71, 255);
        public static readonly Color TURQUOISE = new Color(64, 224, 208, 255);
        public static readonly Color VIOLET = new Color(238, 130, 238, 255);
        public static readonly Color WHEAT = new Color(245, 222, 179, 255);
        public static readonly Color WHITE = new Color(255, 255, 255, 255);
        public static readonly Color WHITE_SMOKE = new Color(245, 245, 245, 255);
        public static readonly Color YELLOW = new Color(255, 255, 0, 255);
        public static readonly Color SIENNA = new Color(160, 82, 45, 255);
        public static readonly Color ORANGE = new Color(255, 165, 0, 255);
        public static readonly Color SEA_SHELL = new Color(255, 245, 238, 255);
        public static readonly Color SANDY_BROWN = new Color(244, 164, 96, 255);
        public static readonly Color ORCHID = new Color(218, 112, 214, 255);
        public static readonly Color PALE_GOLDENROD = new Color(238, 232, 170, 255);
        public static readonly Color LAVENDER = new Color(230, 230, 250, 255);
        public static readonly Color PALE_TURQUOISE = new Color(175, 238, 238, 255);
        public static readonly Color PALE_VIOLET_RED = new Color(219, 112, 147, 255);
        public static readonly Color PAPAYA_WHIP = new Color(255, 239, 213, 255);
        public static readonly Color PEACH_PUFF = new Color(255, 218, 185, 255);
        public static readonly Color PERU = new Color(205, 133, 63, 255);
        public static readonly Color PINK = new Color(255, 192, 203, 255);
        public static readonly Color PLUM = new Color(221, 160, 221, 255);
        public static readonly Color POWDER_BLUE = new Color(176, 224, 230, 255);
        public static readonly Color PURPLE = new Color(128, 0, 128, 255);
        public static readonly Color RED = new Color(255, 0, 0, 255);
        public static readonly Color ROSY_BROWN = new Color(188, 143, 143, 255);
        public static readonly Color ROYAL_BLUE = new Color(65, 105, 225, 255);
        public static readonly Color SADDLE_BROWN = new Color(139, 69, 19, 255);
        public static readonly Color SALMON = new Color(250, 128, 114, 255);
        public static readonly Color SEA_GREEN = new Color(46, 139, 87, 255);
        public static readonly Color KHAKI = new Color(240, 230, 140, 255);
        public static readonly Color HONEYDEW = new Color(240, 255, 240, 255);
        public static readonly Color INDIGO = new Color(75, 0, 130, 255);
        public static readonly Color DARK_GRAY = new Color(169, 169, 169, 255);
        public static readonly Color DARK_GOLDENROD = new Color(184, 134, 11, 255);
        public static readonly Color DARK_CYAN = new Color(0, 139, 139, 255);
        public static readonly Color DARK_BLUE = new Color(0, 0, 139, 255);
        public static readonly Color CYAN = new Color(0, 255, 255, 255);
        public static readonly Color CRIMSON = new Color(220, 20, 60, 255);
        public static readonly Color CORNSILK = new Color(255, 248, 220, 255);
        public static readonly Color CORNFLOWER_BLUE = new Color(100, 149, 237, 255);
        public static readonly Color CORAL = new Color(255, 127, 80, 255);
        public static readonly Color IVORY = new Color(255, 255, 240, 255);
        public static readonly Color CHARTREUSE = new Color(127, 255, 0, 255);
        public static readonly Color CADET_BLUE = new Color(95, 158, 160, 255);
        public static readonly Color BURLY_WOOD = new Color(222, 184, 135, 255);
        public static readonly Color BROWN = new Color(165, 42, 42, 255);
        public static readonly Color BLUE_VIOLET = new Color(138, 43, 226, 255);
        public static readonly Color BLUE = new Color(0, 0, 255, 255);
        public static readonly Color BLANCHED_ALMOND = new Color(255, 235, 205, 255);
        public static readonly Color BLACK = new Color(0, 0, 0, 255);
        public static readonly Color BISQUE = new Color(255, 228, 196, 255);
        public static readonly Color BEIGE = new Color(245, 245, 220, 255);
        public static readonly Color AZURE = new Color(240, 255, 255, 255);
        public static readonly Color AQUAMARINE = new Color(127, 255, 212, 255);
        public static readonly Color AQUA = new Color(0, 255, 255, 255);
        public static readonly Color ANTIQUE_WHITE = new Color(250, 235, 215, 255);
        public static readonly Color ALICE_BLUE = new Color(240, 248, 255, 255);
        public static readonly Color TRANSPARENT = new Color(0, 0, 0, 0);
        public static readonly Color DARK_GREEN = new Color(0, 100, 0, 255);
        public static readonly Color DARK_KHAKI = new Color(189, 183, 107, 255);
        public static readonly Color CHOCOLATE = new Color(210, 105, 30, 255);
        public static readonly Color DARK_OLIVE_GREEN = new Color(85, 107, 47, 255);
        public static readonly Color INDIAN_RED = new Color(205, 92, 92, 255);
        public static readonly Color DARK_MAGENTA = new Color(139, 0, 139, 255);
        public static readonly Color YELLOW_GREEN = new Color(154, 205, 50, 255);
        public static readonly Color GREEN_YELLOW = new Color(173, 255, 47, 255);
        public static readonly Color GREEN = new Color(0, 128, 0, 255);
        public static readonly Color GRAY = new Color(128, 128, 128, 255);
        public static readonly Color GOLDENROD = new Color(218, 165, 32, 255);
        public static readonly Color GOLD = new Color(255, 215, 0, 255);
        public static readonly Color GHOST_WHITE = new Color(248, 248, 255, 255);
        public static readonly Color GAINSBORO = new Color(220, 220, 220, 255);
        public static readonly Color FUCHSIA = new Color(255, 0, 255, 255);
        public static readonly Color FOREST_GREEN = new Color(34, 139, 34, 255);
        public static readonly Color FLORAL_WHITE = new Color(255, 250, 240, 255);
        public static readonly Color HOT_PINK = new Color(255, 105, 180, 255);
        public static readonly Color DODGER_BLUE = new Color(30, 144, 255, 255);
        public static readonly Color DIM_GRAY = new Color(105, 105, 105, 255);
        public static readonly Color DEEP_SKY_BLUE = new Color(0, 191, 255, 255);
        public static readonly Color DEEP_PINK = new Color(255, 20, 147, 255);
        public static readonly Color DARK_VIOLET = new Color(148, 0, 211, 255);
        public static readonly Color DARK_TURQUOISE = new Color(0, 206, 209, 255);
        public static readonly Color DARK_SLATE_GRAY = new Color(47, 79, 79, 255);
        public static readonly Color DARK_SLATE_BLUE = new Color(72, 61, 139, 255);
        public static readonly Color DARK_SEA_GREEN = new Color(143, 188, 139, 255);
        public static readonly Color DARK_SALMON = new Color(233, 150, 122, 255);
        public static readonly Color DARK_RED = new Color(139, 0, 0, 255);
        public static readonly Color DARK_ORCHID = new Color(153, 50, 204, 255);
        public static readonly Color DARK_ORANGE = new Color(255, 140, 0, 255);
        public static readonly Color FIREBRICK = new Color(178, 34, 34, 255);
    }
}
