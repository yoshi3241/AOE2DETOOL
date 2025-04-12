using System.Drawing;

namespace AOE2DETOOL.Utilities
{
    public static class BrushHelper
    {
        public static Brush ColorToBrush(Color color)
        {
            switch (color.Name)
            {
                case "Blue":
                    return Brushes.Blue;
                case "Red":
                    return Brushes.Red;
                case "Black":
                    return Brushes.Black;
                case "White":
                    return Brushes.White;
                case "Green":
                    return Brushes.Green;
                case "Yellow":
                    return Brushes.Yellow;
                case "Gray":
                    return Brushes.Gray;
                case "Orange":
                    return Brushes.Orange;
                case "Purple":
                    return Brushes.Purple;
                default:
                    return new SolidBrush(color); // 定義済み以外は動的生成
            }
        }
    }
}
