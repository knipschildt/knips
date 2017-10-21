﻿using MVCShop.Models.Data;

namespace MVCShop.Models.ViewModels.Pages
{
    public class SidebarVM
    {
        public SidebarVM()
        {

        }

        public SidebarVM(SidebarDTO row)
        {
            Id = row.Id;
            Body = row.Body;

        }

        public int Id { get; set; }
        public string Body { get; set; }

    }
}