namespace SendGridApi.Interfaces
{
    public interface IEmailService
    {
        Task<bool> EnviarEmailAsync(string destinatario, string nombre);
    }
}
