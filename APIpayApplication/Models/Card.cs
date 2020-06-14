using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIpayApplication.Models
{
    public class Card
    {
        [Key]
        public string IdCard { get; set; }
        public string IdUser { get; set; }
        public string Description { get; set; }
        public int CardNumber { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateModify { get; set; }
        public string IdMoneda { get; set; }
    }
}