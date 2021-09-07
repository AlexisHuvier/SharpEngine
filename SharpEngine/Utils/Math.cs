namespace SharpEngine
{
    class Math
    {
        public static int Clamp(int value, int mini = int.MinValue, int maxi = int.MaxValue)
        {
            if (value < mini)
                return mini;
            else if (value > maxi)
                return maxi;
            else
                return value;
        }
        public static float Clamp(float value, float mini = float.MinValue, float maxi = float.MaxValue)
        {
            if (value < mini)
                return mini;
            else if (value > maxi)
                return maxi;
            else
                return value;
        }
    }
}
