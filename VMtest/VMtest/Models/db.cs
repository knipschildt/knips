using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VMtest.Models;

namespace VMtest.Models
{
    public class db : DbContext

    {
        DbSet<CategoryDTO> categories { get; set; }
        DbSet<ProductDTO> products { get; set; }

        DbSet<SalesDTO> sales { get; set; }

        public System.Data.Entity.DbSet<VMtest.Models.CategoryDTO> CategoryDTOes { get; set; }

        public System.Data.Entity.DbSet<VMtest.Models.ProductDTO> ProductDTOes { get; set; }

        public System.Data.Entity.DbSet<VMtest.Models.SalesDTO> SalesDTOes { get; set; }
    }
}