namespace Identity.Application.Utilities.Abstract;

public interface ICodeUtility
{
    string Generate(int length = 6);
}