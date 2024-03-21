using StudentRegistration.Data;
using StudentRegistration.Services.Interfaces;

namespace StudentRegistration.Services.Implementations
{
    public class AuthenticationService : IAuthentication
    {
        private readonly ApplicationDbcontext _dbcontext;
        public AuthenticationService(ApplicationDbcontext applicationDbcontext)
        {
            _dbcontext = applicationDbcontext;

		}
        public bool AuthUser(string username, string password)
        {
            var user = _dbcontext.users.FirstOrDefault(u => u.username == username);
            if (user != null)
            {
                if (user.password == password)
                {
					return true;
				}
                else
                {
                    return false;
                }
            }
            else 
            { 
                return false; 
            }
            
        }
    }
}
