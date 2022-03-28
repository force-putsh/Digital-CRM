using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Crm.Data;
using Crm.Models;
using Crm.Authentication;

using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

namespace Crm
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        partial void OnConfigureServices(IServiceCollection services);

        partial void OnConfiguringServices(IServiceCollection services);

        public void ConfigureServices(IServiceCollection services)
        {
            OnConfiguringServices(services);

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAny",
                    x =>
                    {
                        x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(isOriginAllowed: _ => true)
                        .AllowCredentials();
                    });
            });
            var oDataBuilder = new ODataConventionModelBuilder();

            oDataBuilder.EntitySet<Crm.Models.Crmdata.Contact>("Contacts");
            oDataBuilder.EntitySet<Crm.Models.Crmdata.Contrat>("Contrats");
            oDataBuilder.EntitySet<Crm.Models.Crmdata.StatutContrat>("StatutContrats");
            oDataBuilder.EntitySet<Crm.Models.Crmdata.StatutTache>("StatutTaches");
            oDataBuilder.EntitySet<Crm.Models.Crmdata.Tache>("Taches");
            oDataBuilder.EntitySet<Crm.Models.Crmdata.TypesTache>("TypesTaches");

            this.OnConfigureOData(oDataBuilder);

            oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
            var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
            usersType.AddCollectionProperty(typeof(ApplicationUser).GetProperty("RoleNames"));
            oDataBuilder.EntitySet<IdentityRole>("ApplicationRoles");

            var model = oDataBuilder.GetEdmModel();
            services.AddControllers().AddOData(opt => { 
              opt.AddRouteComponents("odata/crmdata", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
              opt.AddRouteComponents("auth", model).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
            });

            

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("crmdataConnection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddRoleStore<RoleStore<IdentityRole, ApplicationIdentityDbContext, string>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationIdentityDbContext>();
            services.AddTransient<Duende.IdentityServer.Services.IProfileService, ProfileService>();
            services.AddAuthentication()
                .AddIdentityServerJwt();


            services.AddDbContext<Crm.Data.CrmdataContext>(options =>
            {
              options.UseSqlServer(Configuration.GetConnectionString("crmdataConnection"));
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            OnConfigureServices(services);
        }

        partial void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);
        partial void OnConfigureOData(ODataConventionModelBuilder builder);
        partial void OnConfiguring(IApplicationBuilder app, IWebHostEnvironment env);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationIdentityDbContext identityDbContext)
        {
            OnConfiguring(app, env);
            if (env.IsDevelopment())
            {
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.Use((ctx, next) =>
                {
                    ctx.Request.Scheme = "https";
                    return next();
                });
            }
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            IServiceProvider provider = app.ApplicationServices.GetRequiredService<IServiceProvider>();
            app.UseCors("AllowAny");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            identityDbContext.Database.Migrate();

            OnConfigure(app, env);
        }
    }


     public class ProfileService : Duende.IdentityServer.Services.IProfileService
    {
        private readonly IWebHostEnvironment env;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.env = env;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(Duende.IdentityServer.Models.ProfileDataRequestContext context)
        {
            var user = await userManager.GetUserAsync(context.Subject);

            var roles = user != null ? await userManager.GetRolesAsync(user) :
                env.IsDevelopment() && context.Subject.Identity.Name == "admin" ?
                    roleManager.Roles.Select(r => r.Name) : Enumerable.Empty<string>();

            context.IssuedClaims.AddRange(roles.Select(r => new System.Security.Claims.Claim(IdentityModel.JwtClaimTypes.Role, r)));
        }

        public Task IsActiveAsync(Duende.IdentityServer.Models.IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
