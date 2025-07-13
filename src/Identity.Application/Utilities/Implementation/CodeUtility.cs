using System.Text;
using Identity.Application.Utilities.Abstract;

namespace Identity.Application.Utilities.Implementation;

public class CodeUtility : ICodeUtility
{
    public string Generate(int length = 6)
    {
        StringBuilder codeBuilder = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int num = Random.Shared.Next(10);
            codeBuilder.Append(num);
        }

        return codeBuilder.ToString();
    }
}