namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IKeyVaultService
    {
        string GetConnectingString();
        Auth0KeyVault GetAuth0KeyVault();
    }
}
