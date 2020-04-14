using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotatingChores.Models;

namespace RotatingChores.Helpers
{
    public class SortableChore
    {
        public Chore Chore { get; set; }

        public bool ListFirst { get; set; }

        public SortableChore(Chore chore, bool listFirst)
        {
            Chore = chore;
            ListFirst = listFirst;
        }
    }
}
