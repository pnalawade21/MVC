using MyMVCDashboard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Configurations
{
    public class CustomerConfiguration:EntityConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            Property(c => c.Name).IsRequired().HasMaxLength(100);
            Property(c => c.Email).IsRequired().HasMaxLength(60);
            Property(c => c.Phone).IsRequired().HasMaxLength(100);
            Property(c => c.Country).IsRequired().HasMaxLength(100);
            Property(c => c.Image).IsOptional();
        }
    }
}