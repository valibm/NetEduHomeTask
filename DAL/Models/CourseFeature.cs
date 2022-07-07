using DAL.Base;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CourseFeature : BaseEntity, IEntity
    {
        public DateTime StartingDate { get; set; }
        public double Duration { get; set; }
        public double ClassDuration { get; set; }
        public int StudentCount { get; set; }
        public string Assesments { get; set; }

        public List<CourseFeatureLanguage> CourseFeatureLanguages { get; set; }

        public int SkillLevelId { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}
