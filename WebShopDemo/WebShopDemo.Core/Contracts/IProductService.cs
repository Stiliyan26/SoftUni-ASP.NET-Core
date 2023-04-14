using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDemo.Core.Models;

namespace WebShopDemo.Core.Contracts
{
    /// <summary>
    /// Manipulates product
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of products</returns>
        Task<IEnumerable<ProductDto>> GetAll();

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto">Product model</param>
        /// <returns></returns>
        Task Add(ProductDto productDto);
    }
}
