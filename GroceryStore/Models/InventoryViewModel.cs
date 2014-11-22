using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace GroceryStore.Models
{
    public class InventoryViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "SKU")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        public string SKU { get; set; }

        [Required]
        [Display(Name = "Brand")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0)]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Date Recieved")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime DateRecieved { get; set; }

        [Required]
        [Display(Name = "Quantity Recieved")]
        public int QuantityRecieved { get; set; }

        [Required]
        [Display(Name = "Quantity On Hand")]
        public int QuantityOnHand { get; set; }

        [Display(Name = "Last Edited")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "datetime2")]
        public DateTime LastEdited { get; set; }

        [Display(Name = "Edited By")]
        public ApplicationUser User { get; set; }

    }
}