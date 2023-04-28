namespace Patient_Health_Management_System.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> TokenGenerator();
    }
}