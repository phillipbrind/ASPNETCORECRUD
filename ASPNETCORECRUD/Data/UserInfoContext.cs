using ASPNETCORECRUD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCORECRUD.Data
{
    public class UserInfoContext : DbContext
    {
        public UserInfoContext(DbContextOptions<UserInfoContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
