using Dapper;
using System.Data;
using System.Text.Json;

namespace WB.Seller.Companies.Infrastructure.Utilities.Implementation;

public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    public override T Parse(object value)
    {
        if (value is string json)
        {
            return JsonSerializer.Deserialize<T>(json) ?? default!;
        }

        return default!;
    }

    public override void SetValue(IDbDataParameter parameter, T? value)
    {
        parameter.Value = JsonSerializer.Serialize(value);
    }
}