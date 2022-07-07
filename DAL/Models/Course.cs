using DAL.Base;
using DAL.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Course : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }

        public List<CourseCategory> CourseCategories { get; set; }
        public List<CourseTag> CourseTags { get; set; }

        public int CourseFeatureId { get; set; }
        public CourseFeature CourseFeature { get; set; }
    }
}
