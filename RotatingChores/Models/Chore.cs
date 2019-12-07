using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RotatingChores.Models
{
    public class Chore
    {
        public int ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateLastCompleted { get; set; }

        public int FrequencyValue { get; set; }

        

        
    }
}
//    frequencyUnit - enum (days, weeks, months, quarters, years)
//    duedate - datetime, with a getter that uses datelast, frequencyValue, and frequencyUnit, readonly
//    highPriority - bool, default is false, required
//    description? notes? maybe this would only show on the details and/or edit page?, optional
