using MediatRCORSTrial.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRCORSTrial.Data.Entities
{
    public class Product : EntityBase<int>
    {
       // public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

       // public virtual Category Category { get; set; }
    }
}
