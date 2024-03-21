namespace StudentRegistration.Services.Interfaces
{
    public interface IAuthentication
    {
        public bool AuthUser(string username, string password);
    }
}
