using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Application.Context
{
    public class DataContext : IdentityDbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}

