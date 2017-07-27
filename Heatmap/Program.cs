using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Drawing;

namespace Heatmap
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<KeyValuePair<int, int>, int> all_points =  new Dictionary<KeyValuePair<int,int>, int>();
            List<KeyValuePair<int, int>> points_from_file;
            XmlDocument dc = new XmlDocument();
            double lat, lon;
            int countof = Directory.GetFiles("../../Files", "*.gpx").Count();
            int index = 1;
            if (countof == 0)
            {
                Console.WriteLine("No files found!");
                return;
            }
            try
            {
                foreach (string path in Directory.GetFiles("../../Files", "*.gpx"))
                {
                    Console.WriteLine("Reading file {0} of {1}", index++, countof);
                    points_from_file = new List<KeyValuePair<int, int>>();
                    dc.Load(path);
                    foreach(XmlNode x in dc.GetElementsByTagName("trkpt"))
                    {
                        
                        lat = Convert.ToDouble(x.Attributes[0].Value.Replace('.', ','));
                        lon = Convert.ToDouble(x.Attributes[1].Value.Replace('.', ','));
                        points_from_file.Add(new KeyValuePair<int, int>
                            (Convert.ToInt32(CONST.ZOOM * lat), Convert.ToInt32(CONST.ZOOM * lon)));

                    }
                    all_points.AddAllNearBy(points_from_file[0]);
                    for (int i=1;i<points_from_file.Count;++i)
                    {
                        all_points.AddAllNearBy(points_from_file[i]);
                        all_points.AddPointsBetween(points_from_file[i - 1], points_from_file[i], CONST.DEEP_OF_SEARCHING);
                        
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Count of points (millions): {0}", all_points.Sum(x => x.Value)/(1000000));
            int min_lon, max_lon, min_lat, max_lat, max_val;
            min_lon = all_points.Min(i => i.Key.Value);
            max_lon = all_points.Max(i => i.Key.Value);
            min_lat = all_points.Min(i => i.Key.Key);
            max_lat = all_points.Max(i => i.Key.Key);
            max_val = all_points.Max(i => i.Value);
            int width = max_lon - min_lon + 2 * CONST.MARGIN;
            int height = max_lat - min_lat + 2 * CONST.MARGIN;
            Colour[,] bitmap = new Colour[height,width];
            for(int i=0;i<height;++i)
            {
                for(int j=0;j<width;++j)
                {
                    bitmap[i, j] = new Colour();
                }
            }
            Console.WriteLine("Generating heatmap...");
            foreach(KeyValuePair<int,int> coord in all_points.Keys)
            {
                bitmap[coord.Key - min_lat + CONST.MARGIN, coord.Value - min_lon + CONST.MARGIN] = Others.get_color(all_points[coord], max_val);
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
            output.Save("../../heatmap.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Console.WriteLine("Finished!");
            Console.Read();
        }
    }
}
