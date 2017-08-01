using System;
using System.Collections.Generic;
using System.Xml;
namespace Heatmap
{
    /// <summary>
    /// deal with gpx file
    /// </summary>
    public class XMLParser
    {
        List<KeyValuePair<double, double>> points;
        string filePath;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">path to catalog with gpx files</param>
        public XMLParser (string path)
        {
            points = new List<KeyValuePair<double, double>>();
            filePath = path;
        }
        /// <summary>
        /// get all points from single gpx file
        /// </summary>
        /// <returns>list of points</returns>
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
