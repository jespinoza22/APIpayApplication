using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIpayApplication.Models
{
    public class Income
    {
        [Key]
        public string IdIncome { get; set; }
        public string  IdUser { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateApply { get; set; }
        public decimal Amount { get; set; }
        public string IdCard { get; set; }
        public string IdMoneda { get; set; }
    }
}
