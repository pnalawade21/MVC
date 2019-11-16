﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Models
{
    public class TopCustomerViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public string CustomerCountry { get; set; }
        public int CountOrder { get; set; }
    }
}