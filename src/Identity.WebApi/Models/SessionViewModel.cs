namespace Identity.WebApi.Models
{
    public class SessionViewModel
    {
        public required string Id { get; set; }
        public required string UserAgent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Updated { get; set; }
        public bool IsActive { get; set; }
    }
}
