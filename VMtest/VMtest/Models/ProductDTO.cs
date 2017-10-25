using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMtest.Models
{
    public class ProductDTO
    {
        [Key]
        public int productID { get; set; }
        public string productName { get; set; }
        public int categoryID { get; set; }

        public CategoryDTO categories { get; set; }
    }
}