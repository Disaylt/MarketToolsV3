using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Domain.Seed
{
    public interface ISpecificationHandler<TSpecification, TResult>
        where TResult : Entity
        where TSpecification : BaseSpecification<TResult>
    {
        Task<TResult> HandleAsync(TSpecification specification);
    }
}
