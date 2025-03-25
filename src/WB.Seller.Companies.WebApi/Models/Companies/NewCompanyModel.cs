namespace WB.Seller.Companies.WebApi.Models.Companies
{
    public class NewCompanyModel
    {
        public required string Name { get; set; }
        public string? Token { get; set; }
        public string? Description { get; set; }
    }
}
