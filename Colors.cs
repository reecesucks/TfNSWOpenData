
using System.ComponentModel;

namespace TfNSWOpenData
{

    public enum LineColors
    {
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        T9,
        M1,
        F
    }

    public static class LineColorsExtensions
    {
        private static readonly Dictionary<LineColors, string> _lineColorMap = new()
    {
        { LineColors.T1, "#F99D1C" },
        { LineColors.T2, "#0098CD" },
        { LineColors.T3, "#F37021" },
        { LineColors.T4, "#005AA3" },
        { LineColors.T5, "#C4258F" },
        { LineColors.T6, "#7C3E21" },
        { LineColors.T7, "#6F818E" },
        { LineColors.T8, "#00954C" },
        { LineColors.T9, "#D11F2F" },
        { LineColors.M1, "#168388" },
        { LineColors.F, "#5AB031" }
    };

        public static string GetColor(this LineColors line)
        {
            return _lineColorMap.TryGetValue(line, out var color) ? color : "#000000";
        }
    }
}
