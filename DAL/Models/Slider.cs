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
    public class Slider : BaseEntity, IEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
