﻿using System.Data.Entity;

namespace MVCShop.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<PageDTO> Pages { get; set; }

    }
}