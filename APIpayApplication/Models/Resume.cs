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
        public decimal incomeSoles { get; set; }
        public decimal incomeDolares { get; set; }
        public decimal expenseSoles { get; set; }
        public decimal expenseDolares { get; set; }
        public decimal totalSoles { get; set; }
        public decimal totalDolares { get; set; }
    }

    public class ResumeTotal 
    {
        public decimal totalSoles { get; set; }
        public decimal totalDolares { get; set; }
    }
}
