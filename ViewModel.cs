using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    internal class ViewModel
    {
        public List<Column> Data { get; set; }
        public ViewModel()
        {
            Data = new List<Column>();
        }
    }
}
