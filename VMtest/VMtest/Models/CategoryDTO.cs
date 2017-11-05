using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMtest.Models
{
    public class CategoryDTO
    {
        [Key]
        public int categoryID { get; set; }
        public string catName { get; set; }

        public IEnumerable<ProductDTO> products { get; set; }
    }
}