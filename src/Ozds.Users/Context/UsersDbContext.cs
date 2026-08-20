using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ozds.Users.Entities;

namespace Ozds.Users.Context;

public class UsersDbContext(DbContextOptions<UsersDbContext> options)
  : IdentityDbContext<OzdsUser>(options) { }
