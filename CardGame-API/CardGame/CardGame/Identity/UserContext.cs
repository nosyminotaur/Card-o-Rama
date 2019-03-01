using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace CardGame.Identity
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //TO MAP BETWEEN EXTERNALLOGINHELPER AND IT'S STRING VALUE
            var ExternalLoginHelperConverter = new ValueConverter<List<ExternalLogin>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<ExternalLogin>>(v));

            builder.Entity<User>()
                .Property(e => e.ExternalInfo)
                .HasConversion(ExternalLoginHelperConverter);

            base.OnModelCreating(builder);
        }
    }
}
