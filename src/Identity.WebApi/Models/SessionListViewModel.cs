namespace Identity.WebApi.Models
{
    public class SessionListViewModel
    {
        public IReadOnlyCollection<SessionViewModel> Sessions { get; set; } = [];
        public required string CurrentSessionId { get; set; }
    }
}
