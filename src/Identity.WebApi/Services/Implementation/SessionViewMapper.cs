using Identity.Application.Models;
using Identity.WebApi.Models;
using Identity.WebApi.Services.Interfaces;

namespace Identity.WebApi.Services.Implementation
{
    public class SessionViewMapper : ISessionViewMapper
    {
        public SessionViewModel MapFrom(SessionDto session)
        {
            return new SessionViewModel
            {
                CreateDate = session.CreateDate,
                Id = session.Id,
                IsActive = session.IsActive,
                UserAgent = session.UserAgent,
                Updated = session.Updated
            };
        }
    }
}
