using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace User.API.Entities
{
	public class UserDbContext : IdentityDbContext<UserData>
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserData> Users { get;set; }
    }
}

