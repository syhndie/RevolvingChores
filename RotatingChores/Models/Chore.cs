using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RotatingChores.Helpers;

namespace RotatingChores.Models
{
    public class Chore
    {
        public int ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? DaysToRepeat { get; set; }

        public DateTime? DateLastCompleted { get; set; }

        public Priority? Priority { get; set; }

    }
}
