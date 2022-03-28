using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Crm.Models;

namespace Crm
{
    public partial class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment env;

        public AccountController(IWebHostEnvironment env, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.env = env;
        }

        private IActionResult RedirectWithError(string error, string redirectUrl)
        {
             if (!string.IsNullOrEmpty(redirectUrl))
             {
                 return Redirect($"~/Login?error={error}&redirectUrl={Uri.EscapeDataString(redirectUrl)}");
             }
             else
             {
                 return Redirect($"~/Login?error={error}");
             }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string redirectUrl)
        {
            if (env.EnvironmentName == "Development" && userName == "admin" && password == "admin")
            {
                var claims = new List<Claim>()
                {
                        new Claim(ClaimTypes.Name, "admin"),
                        new Claim(ClaimTypes.Email, "admin")
                };

                roleManager.Roles.ToList().ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));
                await signInManager.SignInWithClaimsAsync(new ApplicationUser { UserName = userName, Email = userName }, isPersistent: false, claims);

                return Redirect($"~/{redirectUrl}");
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var user = await userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    return RedirectWithError("Invalid user or password", redirectUrl);
                }

                if (!user.EmailConfirmed)
                {
                    return RedirectWithError("User email not confirmed", redirectUrl);
                }

                var result = await signInManager.PasswordSignInAsync(userName, password, false, false);

                if (result.Succeeded)
                {
                    return Redirect($"~/{redirectUrl}");
                }
            }

            return RedirectWithError("Invalid user or password", redirectUrl);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return Redirect("~/Login?error=Invalid user or password");
            }

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await SendConfirmationEmail(userManager, user, Url, Request.Scheme);
                return Redirect("~/Login");
            }

            var message = string.Join(", ", result.Errors.Select(error => error.Description));

            return Redirect($"~/Login?error={message}");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            if (oldPassword == null || newPassword == null)
            {
                return Redirect($"~/Profile?error=Invalid old or new password");
            }

            var id = this.HttpContext.User.FindFirst("sub").Value;

            var user = await userManager.FindByIdAsync(id);

            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);

                return Redirect("~/");
            }

            var message = string.Join(", ", result.Errors.Select(error => error.Description));

            return Redirect($"~/Profile?error={message}");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Redirect("~/");
        }

        public async Task SendConfirmationEmail(UserManager<ApplicationUser> userManager, ApplicationUser user, IUrlHelper url, string scheme)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: scheme);

            await SendEmail(user, code, callbackUrl, "Votre Compte a été crée avec Succès. Veillez Confirmer votre adress Email", "Votre Compte a été crée avec Succès. Veillez Confirmer votre adress Email");
        }

        partial void OnSendEmail(System.Net.Mail.MailMessage mailMessage);

        public async Task SendEmail(ApplicationUser user, string code, string callbackUrl, string subject, string text)
        {
            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;

            client.EnableSsl = true;

            client.Credentials = new System.Net.NetworkCredential("projettest85@gmail.com", "DVD VCD1");

            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress("projettest85@gmail.com");
            mailMessage.To.Add(user.Email);
            if(callbackUrl != null)
            {
                mailMessage.Body = string.Format(@"<a href=""{0}"">{1}</a>", callbackUrl, text);
            } else
            {
                mailMessage.Body = text;
            }

            mailMessage.Subject = subject;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;

            OnSendEmail(mailMessage);

            await client.SendMailAsync(mailMessage);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId);
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                return Redirect("~/Login");
            }

            return Redirect("~/Login?error=Invalid user Id or confirmation code");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Redirect("~/Login?error=Invalid user");
            }

            if (!user.EmailConfirmed)
            {
                return Redirect("~/Login?error=User email not confirmed");
            }

            await SendResetPasswordEmail(userManager, user, Url, Request.Scheme);

            return Redirect("~/Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ApplicationUser data)
        {
            var user = await userManager.FindByNameAsync(data.UserName);

            if (user == null)
            {
                return Redirect("~/Login?error=Invalid user");
            }

            if (!user.EmailConfirmed)
            {
                return Redirect("~/Login?error=User email not confirmed");
            }

            await SendResetPasswordEmail(userManager, user, Url, Request.Scheme);

            return Redirect("~/Login");
        }

        public async Task SendResetPasswordEmail(UserManager<ApplicationUser> userManager, ApplicationUser user, IUrlHelper url, string scheme)
        {
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = url.Action("ConfirmResetPassword", "Account", new { userId = user.Id, code = code }, protocol: scheme);

            await SendEmail(user, code, callbackUrl, "Confirm password reset", "Confirm password reset");
        }

        partial void OnConfirmResetPassword(string userId, string code, string newPassword);

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmResetPassword(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Redirect("~/Login?error=Invalid user");
            }

            var newPassword = GenerateRandomPassword();

            OnConfirmResetPassword(userId, code, newPassword);

            var result = await userManager.ResetPasswordAsync(user, code, newPassword);
            if (result.Succeeded)
            {
                await SendEmail(user, code, null, "New password", newPassword);
                return Redirect("~/Login");
            }

            return Redirect("~/Login?error=Invalid user Id or confirmation code");
        }

        public static string GenerateRandomPassword(PasswordOptions options = null)
        {
            if (options == null)
            {
                options = new PasswordOptions()
                {
                    RequiredLength = 8,
                    RequiredUniqueChars = 4,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true
                };
            }

            var randomChars = new[] {
                    "ABCDEFGHJKLMNOPQRSTUVWXYZ",
                    "abcdefghijkmnopqrstuvwxyz",
                    "0123456789",
                    "!@$?_-"
                };

            var rand = new Random(Environment.TickCount);
            var chars = new List<char>();

            if (options.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (options.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (options.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (options.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < options.RequiredLength
                || chars.Distinct().Count() < options.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
