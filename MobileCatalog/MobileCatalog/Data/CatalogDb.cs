using MobileCatalog.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileCatalog.Data
{
    public class CatalogDb
    {
        readonly SQLiteAsyncConnection db;
        int codeQuery;
        decimal priceQuery;
        public CatalogDb(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Product>().Wait();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return db.Table<Product>().ToListAsync();
        }

        public Task<Product> GetProductAsync(int id)
        {
            return db.Table<Product>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<List<Product>> SearchProductByCodeAsync(string query)
        {
            int.TryParse(query, out codeQuery);
            return db.Table<Product>().Where(i => i.Code == codeQuery).ToListAsync();
        }
        public Task<List<Product>> SearchProductByNameAsync(string query)
        {
            return db.Table<Product>().Where(i => i.Name.ToLower().StartsWith(query)).ToListAsync();
        }
        public Task<List<Product>> SearchProductByBarCodeAsync(string query)
        {
            return db.Table<Product>().Where(i => i.BarCode.ToLower().StartsWith(query)).ToListAsync();
        }
        public Task<List<Product>> SearchProductByPriceAsync(string query)
        {
            decimal.TryParse(query.Replace('.', ','), out priceQuery);
            return db.Table<Product>().Where(i => i.Price == priceQuery).ToListAsync();
        }
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.Id != 0)
            {
                return db.UpdateAsync(product);
            }
            else
            {
                return db.InsertAsync(product);
            }
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return db.DeleteAsync(product);
        }
    }
}
