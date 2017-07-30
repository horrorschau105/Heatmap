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
                Console.WriteLine("Reading file: {0}", fileName);
                var pointsToAdd = PointsAdjuster.Adjust(parser.GetAllPoints());
                foreach (var pair in pointsToAdd)
                    if (allPoints.ContainsKey(pair))
                        allPoints[pair] += 1;
                    else allPoints.Add(pair, 1);
            }
        }
        public void Generate()
        {
            int min_lon, max_lon, min_lat, max_lat, max_val;
            min_lon = allPoints.Min(i => i.Key.Value);
            max_lon = allPoints.Max(i => i.Key.Value);
            min_lat = allPoints.Min(i => i.Key.Key);
            max_lat = allPoints.Max(i => i.Key.Key);
            max_val = allPoints.Max(i => i.Value);
            int width = max_lon - min_lon + 2 * Constants.MARGIN;
            int height = max_lat - min_lat + 2 * Constants.MARGIN;
            Colour[,] bitmap = new Colour[height, width];
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    bitmap[i, j] = new Colour();
                }
            }
            Console.WriteLine("Generating heatmap...");
            foreach (KeyValuePair<int, int> coord in allPoints.Keys)
            {
                bitmap[coord.Key - min_lat + Constants.MARGIN, coord.Value - min_lon + Constants.MARGIN] = Others.get_color(allPoints[coord], max_val);
            }

            Bitmap output = new Bitmap(height, width);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    output.SetPixel(i, j, bitmap[i, j].transform());

                }
            }
            Console.WriteLine("Saving...");
            output.RotateFlip(RotateFlipType.Rotate270FlipNone);
            output.Save("../../heatmap.jpg", System.Drawing.Imaging.ImageFormat.Tiff);
            Console.WriteLine("Finished!");
            Console.Read();
        }
    }
}
