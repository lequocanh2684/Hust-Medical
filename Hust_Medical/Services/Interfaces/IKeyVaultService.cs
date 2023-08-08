namespace Hust_Medical.Services.Interfaces
{
    public interface IKeyVaultService
    {
        string GetConnectingString();
        Auth0KeyVault GetAuth0KeyVault();
    }
}
