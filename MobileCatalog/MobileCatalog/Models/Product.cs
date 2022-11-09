using SQLite;
using System;

namespace MobileCatalog.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public decimal Quantity { get; set; }
        public string Model { get; set; }
        public string Sort { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Wight { get; set; }
        public DateTime DateChanges { get; set; }

    }
}
