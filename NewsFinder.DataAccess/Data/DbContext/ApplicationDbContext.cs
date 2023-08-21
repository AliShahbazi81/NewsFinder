using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsFinder.DataAccess.Data.Users;

namespace NewsFinder.DataAccess.Data.DbContext;

// Consider the ApplicationDbContext as the main context for the application.
public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> User { get; set; }
}