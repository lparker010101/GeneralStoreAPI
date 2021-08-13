using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.Data_POCOs
{
    public class Customer
    {
        [Key]
        public int? ID { get; set; }

        [Required]
        public string FirstName { get; set; } //Getter gets the value.  Setter assigns the value.

        [Required]
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }  // It is readonly. 
                                                                              // We won't be able to assign it, because we didn't give it a setter.
                                                                              // Example: customer.FullName = "Phil Smith" won't work because we can't assign it, unless we give it a setter.
    }
}