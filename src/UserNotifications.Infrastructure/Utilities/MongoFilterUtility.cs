using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Infrastructure.Utilities
{
    internal static class MongoFilterUtility
    {
        public static FilterDefinition<T> CreateOrEmpty<T>(Expression<Func<T,bool>>? expression)
        {
            return expression == null
                ? Builders<T>.Filter.Empty
                : Builders<T>.Filter.Where(expression);
        }
    }
}
