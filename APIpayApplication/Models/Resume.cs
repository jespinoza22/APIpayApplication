using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIpayApplication.Models
{
    public class Resume
    {
        public int month { get; set; }
        public int year { get; set; }
        public decimal income { get; set; }
        public decimal expense { get; set; }
        public decimal total { get; set; }
    }
}
