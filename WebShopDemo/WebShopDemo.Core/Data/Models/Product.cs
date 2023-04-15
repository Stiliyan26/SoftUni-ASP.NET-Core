using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDemo.Core.Data.Models
{
    [Comment("Product to sell")]
    public class Product
    {
        [Key]
        [Comment("Primary key")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Product name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Comment("Product price")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Product in stock")]
        public int Quantity { get; set; }

        [Comment("Product is available")]
        public bool IsActive { get; set; } = true;
    }
}
