﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Models
{
    public class ProductOrCustomerViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string TypeOrCountry { get; set; }
        public string Type { get; set; }
    }
}