using System.Collections.Generic;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    /// <summary>
    /// Default Model for the ImageCropper DataType as a Property
    /// </summary>
    public class ImageCropper
    {
        public string Src { get; set; }
        public FocalPoint FocalPoint { get; set; }
        public IEnumerable<Crop> Crops { get; set; }

    }

    public class FocalPoint
    {
        public double Left { get; set; }
        public double Top { get; set; }
    }

    public class Crop
    {
        public string Alias { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        public decimal X1 { get; set; }
        public decimal Y1 { get; set; }
        public decimal X2 { get; set; }
        public decimal Y2 { get; set; }
    }
}
