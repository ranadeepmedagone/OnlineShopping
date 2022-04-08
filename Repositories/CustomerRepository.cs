using Dapper;
using OnlineShopping.Models;
using OnlineShopping.Utilities;
using OnlineShopping.Models;

namespace OnlineShopping.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllCustomers(CustomerParameter customerParameter);
    Task<Customer> Create(Customer item);
    Task<bool> Update(Customer item);
    Task<bool> Delete(long Id);
    Task<List<Customer>> GetList();
    Task<Customer> GetById(long Id);

}

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<Customer>> GetAllCustomers(CustomerParameter customerParameter)
    {

        // Query
        var query = $@"SELECT * FROM ""{TableNames.customer}""";

        List<Customer> res;
        using (var con = NewConnection)
        res = (await con.QueryAsync<Customer>(query, new {Limit =  customerParameter.PageSize, Offset = Parameter.PageNumber  }))

         return res;
    }




    public async Task<Customer> Create(Customer item)
    {
        var query = $@"INSERT INTO ""{TableNames.customer}""
        (customer_name, email, mobile, password)
        VALUES(@CustomerName, @Email, @Mobile, @Password) RETURNING *";
        using (var con = NewConnection)
        {

            var res = await con.QuerySingleOrDefaultAsync<Customer>(query, item);
            return res;

        }
    }

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.customer}""
        WHERE id = @Id";
        using (var con = NewConnection)
        {

            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;

        }
    }

    public async Task<Customer> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.customer}""
        WHERE id = @Id";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Customer>(query,
            new
            {
                Id
            });
    }

    public async Task<List<Customer>> GetList()
    {


        var query = $@"SELECT * FROM ""{TableNames.customer}""";
        List<Customer> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Customer>(query)).AsList();
        return res;

    }

    public async Task<bool> Update(Customer item)
    {
        var query = $@"UPDATE ""{TableNames.customer}"" SET customer_name = @CustomerName, email = @Email, mobile = @Mobile, password =@Password
        WHERE id = @Id";

        using (var con = NewConnection)
        {

            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}