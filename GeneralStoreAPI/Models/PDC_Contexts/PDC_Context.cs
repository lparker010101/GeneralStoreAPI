using GeneralStoreAPI.Models.Data_POCOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class PDC_Context : DbContext // So the migration will work.
    {
        // In parameters is the connection string.  Either a database string or a connection string.
        public PDC_Context() : base("DefaultConnection")  
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}