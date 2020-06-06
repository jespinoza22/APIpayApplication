using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIpayApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIpayApplication.DbContext
{
    public class PayContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PayContext(DbContextOptions<PayContext> options) : base(options) 
        { 
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
