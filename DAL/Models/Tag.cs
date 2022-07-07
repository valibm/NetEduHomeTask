using DAL.Base;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Tag : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public List<CourseTag> CourseTags { get; set; }
    }
}
