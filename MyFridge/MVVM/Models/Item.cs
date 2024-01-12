
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFridge.MVVM.Models
{
    public class Item


    {
        public string ean { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string upc { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string dimension { get; set; }
        public string weight { get; set; }
        public string category { get; set; }
        public string currency { get; set; }
        public decimal lowest_recorded_price { get; set; }
        public decimal highest_recorded_price { get; set; }
        public List<string> images { get; set; }
        public List<Offer> offers { get; set; }
        public string asin { get; set; }
    }
}
