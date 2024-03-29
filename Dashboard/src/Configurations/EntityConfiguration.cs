﻿using MyMVCDashboard.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MyMVCDashboard.Configurations
{
    public class EntityConfiguration<T>:EntityTypeConfiguration<T> where T:class, IEntity
    {
        public EntityConfiguration()
        {
            HasKey(e => e.ID);
        }
    }
}