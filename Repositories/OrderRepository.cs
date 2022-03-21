using Dapper;
using OnlineShopping.Models;
using OnlineShopping.Utilities;

namespace OnlineShopping.Repositories;

public interface IOrderRepository
{
    Task<Order> Create (Order item);
    Task<bool> Update (Order item);
    // Task<bool> Delete (long Id);
    // Task<List<Order>> GetList();
    Task<Order> GetById (long Id);

    Task<List<Order>> GetOrderByCustomerId(long CustomerId);

}

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Order> Create(Order item)
    {
        var query = $@"INSERT INTO ""{TableNames.order}""
        (created_at, status, total_value, payment_status, delivery_date,customer_id)
        VALUES(@CreatedAt, @Status, @TotalValue, @PaymentStatus, @DeliveryDate, @CustomerId) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<Order>(query,item);
            return res;
            
        }
    }

    // public async Task<bool> Delete(long Id)
    // {
    //     var query = $@"DELETE FROM ""{TableNames.order}""
    //     WHERE id = @Id";
    //     using(var con = NewConnection){

    //         var res = await con.ExecuteAsync(query, new{Id});
    //         return res > 0;

    //     }
    // }

    public async Task<Order> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.order}""
        WHERE id = @Id";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Order>(query,
        new{
            Id
        });
    }

    public async Task<List<Order>> GetOrderByCustomerId(long CustomerId)
    {
        var query = $@"SELECT * FROM ""{TableNames.order}"" WHERE customer_id = @CustomerId";

        using(var con = NewConnection)
        {
          return  (await con.QueryAsync<Order>(query, new{CustomerId})).AsList();
       }
    }
 
    // public async Task<List<Order>> GetOrdersByProductId(long Id)
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.order_product}"" op
    //     LEFT JOIN {TableNames.order} o ON o.id = op.order_id
    //     WHERE product_id = @Id";

    //     using(var con = NewConnection)
    //     {
    //        return  (await con.QueryAsync<Order>(query, new{Id})).AsList();
    //     }
    // }

    // public async Task<List<Order>> GetList()
    // {


    //     var query = $@"SELECT * FROM ""{TableNames.order}""";
    //     List<Order>res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Order>(query)).AsList();
    //     return res;

    // }

    public async Task<bool> Update(Order item)
    {
        var query = $@"UPDATE ""{TableNames.order}"" SET status = @Status, total_value = @TotalValue, payment_status = @PaymentStatus, delivery_date = @DeliveryDate
        WHERE id = @Id";

        using(var con = NewConnection){

            var rowCount = await con.ExecuteAsync(query,item);
            return rowCount == 1;

        }
    }
}