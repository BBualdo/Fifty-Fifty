using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Household> Households { get; set; }
    public DbSet<>
}
