using GC.MFI.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GC.MFI.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {      

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }               
    }
}
