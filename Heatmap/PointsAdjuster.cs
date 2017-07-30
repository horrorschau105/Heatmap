using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heatmap
{
    /// <summary>
    /// Manipulations with points come here
    /// </summary>
    public static class PointsAdjuster
    {
        public static List<KeyValuePair<int, int>> Adjust(List<KeyValuePair<double, double>> incomingPoints)
        {
            List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();
            foreach(var pair in incomingPoints)
                result.Add(new KeyValuePair<int, int>(Convert.ToInt32(Constants.ZOOM * pair.Key), Convert.ToInt32(Constants.ZOOM * pair.Value)));
            return result;
        }
        public static Dictionary<KeyValuePair<int, int>, int> BoldLine(Dictionary<KeyValuePair<int, int>, int> incomingPoints)
        {
            Dictionary<KeyValuePair<int, int>, int> additionalPoints = new Dictionary<KeyValuePair<int, int>, int>();
            KeyValuePair<int,int> extendedPoint;
            foreach (var point in incomingPoints)
            {
                for (int idx = -Constants.WIDTH_OF_ROUTE; idx < Constants.WIDTH_OF_ROUTE; ++idx)
                {
                    for (int jdx = -Constants.WIDTH_OF_ROUTE; jdx < Constants.WIDTH_OF_ROUTE; ++jdx)
                    {
                        extendedPoint = new KeyValuePair<int, int>(point.Key.Key + idx, point.Key.Value + jdx);
                        if (additionalPoints.ContainsKey(extendedPoint))
                            additionalPoints[extendedPoint] += point.Value;
                        else additionalPoints.Add(extendedPoint, point.Value);
                    }
                }
            }
            return additionalPoints;
        }
        public static Dictionary<KeyValuePair<int, int>, int> Adjust(Dictionary<KeyValuePair<int, int>, int> incomingPoints)
        {
            Dictionary<KeyValuePair<int, int>, int> modifiedPoints = new Dictionary<KeyValuePair<int, int>, int>();
            int minLongitude,  minLattitude;
            minLongitude = incomingPoints.Min(i => i.Key.Value);
            minLattitude = incomingPoints.Min(i => i.Key.Key);
            foreach(var point in incomingPoints)
                modifiedPoints.Add(new KeyValuePair<int, int>
                      (point.Key.Key - minLattitude, point.Key.Value - minLongitude), point.Value);
            
            return modifiedPoints;


        }
    }
}
