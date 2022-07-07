﻿using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SkillLevel : IEntity
    {
        public int Id { get; set; }
        public string Level { get; set; }
    }
}
