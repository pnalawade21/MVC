﻿using MyMVCDashboard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Configurations
{
    public class OrderConfiguration:EntityConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(O => O.OrderDate).IsRequired();
        }
    }
}