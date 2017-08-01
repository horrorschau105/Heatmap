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
