using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;

namespace Heatmap
{
    class Heatmapmaker
    {
        Dictionary<KeyValuePair<int, int>, int> allPoints;
        XMLParser parser;
        string path;
        public Heatmapmaker(string filesPath)
        {
            path = filesPath;
            allPoints = new Dictionary<KeyValuePair<int, int>, int>();
        }
        public void ReadAllFiles()
        {
            var allFiles = Directory.GetFiles(path, "*.gpx");
            if (allFiles.Count() == 0)
                throw new Exception("No files found");
            foreach(var fileName in allFiles)
            {
                parser = new XMLParser(fileName);
                ...
            }
        }
    }
}
