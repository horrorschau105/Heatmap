using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
namespace Heatmap
{
    class Heatmapmaker
    {
        Dictionary<KeyValuePair<int, int>, int> allPoints; // contains all points from gpx 
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
            Console.WriteLine("Bolding line...");
            allPoints = PointsAdjuster.Normalize(PointsAdjuster.BoldLine(allPoints));
            int width = allPoints.Max(i => i.Key.Value) + 2 * Constants.MARGIN;
            int height = allPoints.Max(i => i.Key.Key) + 2 * Constants.MARGIN;
            int heighestValue = allPoints.Max(i => i.Value);
            Color[,] bitmap = new Color[height, width];
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    bitmap[i, j] = Color.White; // everything is white
            Console.WriteLine("Generating heatmap...");
            foreach (KeyValuePair<int, int> coord in allPoints.Keys)
                bitmap[coord.Key + Constants.MARGIN, coord.Value + Constants.MARGIN] = 
                    ColorHandler.SetColor(allPoints[coord], heighestValue); // setting color
            Bitmap output = new Bitmap(height, width);
            for (int i = 0; i < height; ++i)
                for (int j = 0; j < width; ++j)
                    output.SetPixel(i, j, bitmap[i, j]); // putting on bitmap
            Console.WriteLine("Saving...");
            output.RotateFlip(RotateFlipType.Rotate270FlipNone);
            output.Save("..\\..\\heatmap.png", System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine("Finished!");
            Console.WriteLine("Count of points: {0}", allPoints.Count);
            Console.Read();
        }
    }
}
