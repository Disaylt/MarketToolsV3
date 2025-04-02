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
    public class UpdateDefinitionContainer<TDocument>
    {
        private readonly List<UpdateDefinition<TDocument>> _updateDefinitions = [];

        public UpdateDefinitionContainer<TDocument> AddIfTrue<TField>(bool condition, Expression<Func<TDocument, TField>> field, TField value)
        {
            if (condition)
            {
                UpdateDefinition<TDocument> updateDefinition = Builders<TDocument>.Update.Set(field, value);
                _updateDefinitions.Add(updateDefinition);
            }

            return this;
        }

        public IReadOnlyCollection<UpdateDefinition<TDocument>> Collection => _updateDefinitions.AsReadOnly();
    }
}
