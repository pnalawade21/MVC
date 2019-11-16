using MyMVCDashboard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Configurations
{
    public class OrderDetailConfiguration : EntityConfiguration<OrderDetails>
    {
        public OrderDetailConfiguration()
        {
            Property(od => od.Quantity).IsRequired();
        }
    }
}