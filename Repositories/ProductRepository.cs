using Dapper;
using OnlineShopping.Models;
using OnlineShopping.Utilities;

namespace OnlineShopping.Repositories;

public interface IProductRepository
{
    Task<Product> Create (Product item);
    Task<bool> Update (Product item);
    Task<bool> Delete (long Id);
    Task<List<Product>> GetList();
    Task<List<Product>> GetProductsByOrderId(long Id);
    Task<Product> GetById (long Id);

}

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Product> Create(Product item)
    {
        var query = $@"INSERT INTO ""{TableNames.product}""
        (name, price, size, created_at, in_stock)
        VALUES(@Name, @Price, @Size, @CreatedAt, @InStock) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<Product>(query,item);
            return res;
            
        }
    }

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.product}""
        WHERE id = @Id";
        using(var con = NewConnection){

            var res = await con.ExecuteAsync(query, new{Id});
            return res > 0;

        }
    }

    public async Task<Product> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.product}""
        WHERE id = @Id";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Product>(query,
        new{
            Id
        });
    }

    public async Task<List<Product>> GetList()
    {
        
    
        var query = $@"SELECT * FROM ""{TableNames.product}""";
        List<Product>res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Product>(query)).AsList();
        return res;
    
    }

    public async Task<List<Product>> GetProductsByOrderId(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.order_product}"" op
        LEFT JOIN {TableNames.product} p ON p.id = op.product_id
        WHERE op.order_id = @Id";

        using(var con = NewConnection)
        {
           return  (await con.QueryAsync<Product>(query, new{Id})).AsList();
        }
        
    }

    public async Task<bool> Update(Product item)
    {
        var query = $@"UPDATE ""{TableNames.product}"" SET name = @Name, price = @Price, size = @Size, created_at = @CreatedAt, in_stock = @InStock
        WHERE id = @Id";

        using(var con = NewConnection){

            var rowCount = await con.ExecuteAsync(query,item);
            return rowCount == 1;

        }
    }
}