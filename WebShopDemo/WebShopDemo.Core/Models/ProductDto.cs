using System.ComponentModel.DataAnnotations;

namespace WebShopDemo.Core.Models
{
    /// <summary>
    /// Product model
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Prodcut name
        /// </summary>
       
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Product price
        /// </summary>
        [Range(typeof(decimal), "0.1", "1000")]
        public decimal Price { get; set; }

        /// <summary>
        /// Available quantity
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
