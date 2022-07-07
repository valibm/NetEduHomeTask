using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CourseFeatureLanguage : IEntity
    {
        public int Id { get; set; }
        public int CourseFeatureId { get; set; }
        public CourseFeature CourseFeature { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
