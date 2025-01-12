namespace MarketToolsV3.ApiGateway.Constant
{
    public enum AuthState
    {
        None,
        AccessTokenContainsCorrectSessionId,
        AccessTokenValid,
        TokensRefreshed,
        SessionTokenValid
    }
}
