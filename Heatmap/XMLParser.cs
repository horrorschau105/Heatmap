using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Drawing;

namespace Heatmap
{
    /// <summary>
    /// deal with gpx file
    /// </summary>
    public class XMLParser
    {
        List<KeyValuePair<double, double>> points;
        string filePath;
        public XMLParser (string path)
        {
            points = new List<KeyValuePair<double, double>>();
            filePath = path;
        }
        public List<KeyValuePair<double, double>> GetAllPoints()
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);
            foreach (XmlNode node in document.GetElementsByTagName("trkpt"))
            {
                double lattitude = Convert.ToDouble(node.Attributes[0].Value.Replace(".", ","));
                double longitude = Convert.ToDouble(node.Attributes[1].Value.Replace(".", ","));
                points.Add(new KeyValuePair<double, double>(lattitude, longitude));
            }
            return points;
        }

    }
}
