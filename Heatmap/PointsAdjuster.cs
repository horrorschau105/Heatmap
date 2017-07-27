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
    public class PointsAdjuster
    {
        List<KeyValuePair<double, double>> points;
        public PointsAdjuster(List<KeyValuePair<double ,double> > incomingPoints)
        {
            points = incomingPoints;
        }
        public List<KeyValuePair<int, int>> adjust()
        {
            List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();

            return result;
        }

    }
}
