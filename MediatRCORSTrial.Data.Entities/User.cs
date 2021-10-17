using MediatRCORSTrial.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MediatRCORSTrial.Data.Entities
{
    public class User : EntityBase<int>
    {
        //public User()
        //{
        //    AllowedCategories = new List<Category>();
        //}

        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        public string Mail { get; set; }

       
        //public virtual ICollection<Category> AllowedCategories { get; set; }
    }
}
