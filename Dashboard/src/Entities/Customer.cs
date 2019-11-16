using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Entities
{
    public class Customer:IEntity
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public int ID  {  get;  set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}