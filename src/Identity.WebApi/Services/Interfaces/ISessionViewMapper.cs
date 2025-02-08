using Identity.Application.Models;
using Identity.WebApi.Models;

namespace Identity.WebApi.Services.Interfaces
{
    public interface ISessionViewMapper
    {
        SessionViewModel MapFrom(SessionDto session);
    }
}
