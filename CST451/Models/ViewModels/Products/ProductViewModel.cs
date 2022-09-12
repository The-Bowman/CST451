using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CST451.Models.ViewModels.Products
{
    public class ProductViewModel
    {
        public int? ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength (500)]
        public string Description { get; set; }
        public int? Compatibility { get; set; }
        [Range(0, 9999.99)]
        public double? Price { get; set; }
        public string ImagePath { get; set; }
        public bool? Success { get; set; }
    }
}
