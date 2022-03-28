using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Crm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crm.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        // Sample method which returns the sum of its arguments
        // For more examples check https://www.radzen.com/documentation/invoke-custom-method/
        // public IActionResult Sum(int x, int y)
        // {
        //    var sum = x + y;
        //
        //    return Json(new { sum });
        // }
        private readonly CrmdataContext context;

        public ServerMethodsController(CrmdataContext context)
        {
            this.context = context;
        }

        public IActionResult MonthlyStats()
        {
            double contratGagne = context.Contrats
                                 .Include(contrat => contrat.StatutContrat)
                                 .Where(contrat => contrat.StatutContrat.Name == "Won")
                                 .Count();

            var totalContrats = context.Contrats.Count();

            var ratio = contratGagne / totalContrats;

            var stats = context.Contrats
                                 .Include(contrat => contrat.StatutContrat)
                                 .Where(contrat => contrat.StatutContrat.Name == "Won")
                                 .ToList()
                                 .GroupBy(contrat => new DateTime(contrat.CloseDate.Year, contrat.CloseDate.Month, 1))
                                 .Select(group => new
                                 {
                                     Month = group.Key,
                                     Revenue = group.Sum(contrat => contrat.Amount),
                                     Contrats = group.Count(),
                                     AverageDealSize = group.Average(contrat => contrat.Amount),
                                     Ratio = ratio
                                 })
                                 .OrderBy(deals => deals.Month)
                                 .LastOrDefault();

            return Ok(JsonSerializer.Serialize(stats, new JsonSerializerOptions { PropertyNamingPolicy = null }));
        }
        public IActionResult RevenueByCompany()
        {
            var result = context.Contrats
                                 .Include(opportunity => opportunity.Contact)
                                 .ToList()
                                 .GroupBy(opportunity => opportunity.Contact.Company)
                                 .Select(group => new {
                                     Company = group.Key,
                                     Revenue = group.Sum(opportunity => opportunity.Amount)
                                 });

            return Ok(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByEmployee()
        {
            var result = context.Contrats
                                 .Include(opportunity => opportunity.User)
                                 .ToList()
                                 .GroupBy(opportunity => $"{opportunity.User.Nom} {opportunity.User.Prenom}")
                                 .Select(group => new {
                                     Employee = group.Key,
                                     Revenue = group.Sum(opportunity => opportunity.Amount)
                                 });


            return Ok(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByMonth()
        {
            var result = context.Contrats
                                 .Include(opportunity => opportunity.StatutContrat)
                                 .Where(opportunity => opportunity.StatutContrat.Name == "Won")
                                 .ToList()
                                 .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                                 .Select(group => new {
                                     Revenue = group.Sum(opportunity => opportunity.Amount),
                                     Month = group.Key
                                 })
                                 .OrderBy(deals => deals.Month);

            return Ok(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }
    }

}
