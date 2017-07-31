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
            Heatmapmaker hm = new Heatmapmaker("..\\..\\Files");
            hm.ReadAllFiles();
            hm.Generate();
        }
    }
}
