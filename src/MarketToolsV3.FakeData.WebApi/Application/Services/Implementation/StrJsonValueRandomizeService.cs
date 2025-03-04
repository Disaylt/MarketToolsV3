using System.Text;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class StrJsonValueRandomizeService : IJsonValueRandomizeService<string>
    {
        private const string Words = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        public string Create(int min, int max)
        {
            StringBuilder sb = new StringBuilder();
            int length = Random.Shared.Next(min, max);

            for (var i = 0; i < length; i++)
            {
                int randomIndex = Random.Shared.Next(Words.Length);
                sb.Append(Words[randomIndex]);
            }

            return sb.ToString();
        }
    }
}
