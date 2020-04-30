// namespace RZ
namespace UnityEngine
{
    /// <summary>
    /// Extensions for 'UnityEngine.Color'
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Create Color from RGB values 0..255
        /// </summary>
        public static Color RZ_From255(this Color color, byte r, byte g, byte b, byte a = 255)
        {
            color.r = (float)r / byte.MaxValue;
            color.g = (float)g / byte.MaxValue;
            color.b = (float)b / byte.MaxValue;
            color.a = (float)a / byte.MaxValue;
            return color;
        }
    }
}
