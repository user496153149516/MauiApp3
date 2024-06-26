using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    internal class Cost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sum { get; set; }
        public DateOnly Data { get; set; }
        public Category Category { get; set; }
    }
}
