using Crm.Models;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data
{
    /* This class is called when the model is being built */
    public partial class CrmdataContext
    {
        /// <summary>
        /// This function is called when the model is being built
        /// </summary>
        /// <param name="ModelBuilder">The builder parameter is the ModelBuilder object that is used to
        /// build the model.</param>
        /// <summary>
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        }
    }
}
