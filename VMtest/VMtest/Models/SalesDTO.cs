using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VMtest.Models
{
    public class SalesDTO
    {
        [Key]
        public int saleId { get; set; }
        public int productId { get; set; }

        public ProductDTO product { get; set; }

        public DateTime SaleDate { get; set; }

    }
}