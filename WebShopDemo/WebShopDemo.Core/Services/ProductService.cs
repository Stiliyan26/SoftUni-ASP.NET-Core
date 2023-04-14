using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using WebShopDemo.Core.Contracts;
using WebShopDemo.Core.Data.Common;
using WebShopDemo.Core.Data.Models;
using WebShopDemo.Core.Models;
using WebShopDemo.Data;

namespace WebShopDemo.Core.Services
{
    /// <summary>
    /// Manipulates product
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IConfiguration config;

        private readonly IRepository repo;

        private readonly ApplicationDbContext context;

        /// <summary>
        /// IoC 
        /// </summary>
        /// <param name="_config">Application configuration</param>
        public ProductService(
            IConfiguration _config,
            IRepository _repo)
        {
            config = _config;
            repo = _repo;
        }

        public async Task Add(ProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity  
            };

            /*await context.AddAsync(product);
            await context.SaveChangesAsync();*/

            await repo.AddAsync(product);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            /*string dataPath = config.GetSection("DataFiles:Products").Value; 
            string data = await File.ReadAllTextAsync(dataPath);

            return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(data);*/
            return await repo.AllReadonly<Product>()
                .Select(p => new ProductDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .ToListAsync();
        }
    }
}
