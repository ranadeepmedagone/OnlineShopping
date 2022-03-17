using Dapper;
using OnlineShopping.Models;
using OnlineShopping.Utilities;

namespace OnlineShopping.Repositories;

public interface ITagRepository
{
    Task<Tag> Create (Tag item);
    // Task<bool> Update (Tag item);
    Task<bool> Delete (long Id);
    Task<List<Tag>> GetList();
    Task<Tag> GetById (long Id);
    Task<List<Tag>> GetTagsByProductId(long ProductId);
}

public class TagRepository : BaseRepository, ITagRepository
{
    public TagRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Tag> Create(Tag item)
    {
        var query = $@"INSERT INTO ""{TableNames.tag}""
        (name)
        VALUES(@Name) RETURNING *";
        using(var con = NewConnection){

            var res = await con.QuerySingleOrDefaultAsync<Tag>(query,item);
            return res;
            
        }
    }

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.tag}""
        WHERE id = @Id";
        using(var con = NewConnection){

            var res = await con.ExecuteAsync(query, new{Id});
            return res > 0;

        }
    }

    public async Task<Tag> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.tag}""
        WHERE id = @Id";
        using(var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Tag>(query,
        new{
            Id
        });
    }

    public async Task<List<Tag>> GetList()
    {
        
    
        var query = $@"SELECT * FROM ""{TableNames.tag}""";
        List<Tag>res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Tag>(query)).AsList();
        return res;
    
    }

    public async Task<List<Tag>> GetTagsByProductId(long ProductId)
    {
        var query = $@"SELECT * FROM ""{TableNames.product_tag}"" pt
         LEFT JOIN {TableNames.tag} t ON t.id = pt.tag_id
         WHERE pt.product_id = @Id";

        using(var con = NewConnection)
        {
           return  (await con.QueryAsync<Tag>(query, new{ProductId})).AsList();
        }
    }

    // public async Task<bool> Update(Tag item)
    // {
    //     var query = $@"UPDATE ""{TableNames.Tag}"" SET status = @Status, total_value = @TotalValue, payment_status = @PaymentStatus, delivery_date = @DeliveryDate
    //     WHERE id = @Id";

    //     using(var con = NewConnection){

    //         var rowCount = await con.ExecuteAsync(query,item);
    //         return rowCount == 1;

    //     }
    // }
} 