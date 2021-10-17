using MediatRCORSTrial.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MediatRCORSTrial.Data.Entities
{
    public class Category : EntityBase<int>
    {
        //public Category()
        //{
        //    AssignedUsers = new List<User>();
        //    Products = new List<Product>();
        //}

        public string Name { get; set; }

        public int Rank { get; set; }

        //[NotMapped]
        //public virtual ICollection<User> AssignedUsers { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
    }
}
