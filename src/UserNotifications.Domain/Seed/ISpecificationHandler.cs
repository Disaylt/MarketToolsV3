using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Domain.Seed
{
    public interface IRangeSpecificationHandler<TSpecification, TResult>
        where TResult : Entity
        where TSpecification : RangeBaseSpecification<TResult>
    {
        Task<IEnumerable<TResult>> HandleAsync(TSpecification specification);
    }

    public interface ISpecificationHandler<TSpecification, TResult>
        where TResult : Entity
        where TSpecification : BaseSpecification<TResult>
    {
        Task<TResult> HandleAsync(TSpecification specification);
    }

    public interface ISpecificationHandler<TSpecification>
        where TSpecification : BaseSpecification
    {
        Task HandleAsync(TSpecification specification);
    }
}
