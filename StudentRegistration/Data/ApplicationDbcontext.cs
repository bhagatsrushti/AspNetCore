using Microsoft.EntityFrameworkCore;
using StudentRegistration.Models;

namespace StudentRegistration.Data
{
    public class ApplicationDbcontext : DbContext
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options) 
        {
                
        }
        public DbSet<usersModel> users {  get; set; }
        public DbSet<StudentModel> Student {  get; set; }


    }
}
