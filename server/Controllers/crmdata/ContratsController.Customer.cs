using Crm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Crm.Controllers.Crmdata
{
    [Authorize]
    public partial class ContratsController
    {
        /// <summary>
        /// It filters the opportunities by the current user's id.
        /// </summary>
        /// <param name="items">The queryable object that you want to filter.</param>
        partial void OnContratsRead(ref IQueryable<Models.Crmdata.Contrat> items)
        {
            var userManager = (UserManager<ApplicationUser>)HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user != null)
            {
                var roles = userManager.GetRolesAsync(user).Result;

                if (roles.Contains("Sales Manager"))
                {
                    // Filter the opportunities by the current user's id
                    items = items.Where(item => item.UserId == user.Id);
                }
            }
        }

        /// <summary>
        /// It sets the UserId property of the opportunity to the current user's id.
        /// </summary>
        /// <param name="item">The opportunity that is being created.</param>
        partial void OnContratCreated(Models.Crmdata.Contrat item)
        {
            var userManager = (UserManager<ApplicationUser>)HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

            var userId = userManager.GetUserId(HttpContext.User);

            // Set the UserId property of the opportunity to the current user's id
            item.UserId = userId;
        }
    }
}
