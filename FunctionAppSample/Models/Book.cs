using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppSample.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsRent { get; set; }
    }
}
