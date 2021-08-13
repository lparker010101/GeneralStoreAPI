using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.Data_POCOs
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey(nameof(Customer))]
        public int? CustomerID { get; set; } // We use CustomerID to navigate to the Customer object.
        public virtual Customer Customer { get; set; } // This is the navigation property where it says nameof.


        [ForeignKey(nameof(Product))]
        public string ProductSKU { get; set; }  //ProductSKU is the foreign key.  Whatever SKU it is associate with will navigate to the specific Product from the line below.
        public virtual Product Product { get; set; }  // Entity will know it is a foreign key, but it will not know to grab the data and bring back to me, unless in my services layer I tell it to.
                                                      // Lazy loading - virtual (Virtual tells it that we want every associated property about the product.)

        public int ItemCount { get; set; }
        public DateTime DateOfTransaction { get; set; } 
    }
}


// Note: Can't add migrations in manage package console.