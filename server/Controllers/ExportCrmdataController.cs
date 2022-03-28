using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crm.Data;

namespace Crm
{
    public partial class ExportCrmdataController : ExportController
    {
        private readonly CrmdataContext context;
        public ExportCrmdataController(CrmdataContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/Crmdata/contacts/csv")]
        [HttpGet("/export/Crmdata/contacts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportContactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Contacts, Request.Query), fileName);
        }

        [HttpGet("/export/Crmdata/contacts/excel")]
        [HttpGet("/export/Crmdata/contacts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportContactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Contacts, Request.Query), fileName);
        }
        [HttpGet("/export/Crmdata/contrats/csv")]
        [HttpGet("/export/Crmdata/contrats/csv(fileName='{fileName}')")]
        public FileStreamResult ExportContratsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Contrats, Request.Query), fileName);
        }

        [HttpGet("/export/Crmdata/contrats/excel")]
        [HttpGet("/export/Crmdata/contrats/excel(fileName='{fileName}')")]
        public FileStreamResult ExportContratsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Contrats, Request.Query), fileName);
        }
        [HttpGet("/export/Crmdata/statutcontrats/csv")]
        [HttpGet("/export/Crmdata/statutcontrats/csv(fileName='{fileName}')")]
        public FileStreamResult ExportStatutContratsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.StatutContrats, Request.Query), fileName);
        }

        [HttpGet("/export/Crmdata/statutcontrats/excel")]
        [HttpGet("/export/Crmdata/statutcontrats/excel(fileName='{fileName}')")]
        public FileStreamResult ExportStatutContratsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.StatutContrats, Request.Query), fileName);
        }
        [HttpGet("/export/Crmdata/statuttaches/csv")]
        [HttpGet("/export/Crmdata/statuttaches/csv(fileName='{fileName}')")]
        public FileStreamResult ExportStatutTachesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.StatutTaches, Request.Query), fileName);
        }

        [HttpGet("/export/Crmdata/statuttaches/excel")]
        [HttpGet("/export/Crmdata/statuttaches/excel(fileName='{fileName}')")]
        public FileStreamResult ExportStatutTachesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.StatutTaches, Request.Query), fileName);
        }
        [HttpGet("/export/Crmdata/taches/csv")]
        [HttpGet("/export/Crmdata/taches/csv(fileName='{fileName}')")]
        public FileStreamResult ExportTachesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Taches, Request.Query), fileName);
        }

        [HttpGet("/export/Crmdata/taches/excel")]
        [HttpGet("/export/Crmdata/taches/excel(fileName='{fileName}')")]
        public FileStreamResult ExportTachesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Taches, Request.Query), fileName);
        }
        [HttpGet("/export/Crmdata/typestaches/csv")]
        [HttpGet("/export/Crmdata/typestaches/csv(fileName='{fileName}')")]
        public FileStreamResult ExportTypesTachesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.TypesTaches, Request.Query), fileName);
        }

        [HttpGet("/export/Crmdata/typestaches/excel")]
        [HttpGet("/export/Crmdata/typestaches/excel(fileName='{fileName}')")]
        public FileStreamResult ExportTypesTachesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.TypesTaches, Request.Query), fileName);
        }
    }
}
