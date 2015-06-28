using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameImporter.Dto
{
    public class SimpleGame
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int Platform { get; set; }
    }
}
