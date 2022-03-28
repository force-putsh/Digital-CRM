using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace Crm.Controllers.Crmdata
{
  using Models;
  using Data;
  using Models.Crmdata;

  [Route("odata/crmdata/Contrats")]
  public partial class ContratsController : ODataController
  {
    private Data.CrmdataContext context;

    public ContratsController(Data.CrmdataContext context)
    {
      this.context = context;
    }
    // GET /odata/Crmdata/Contrats
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crmdata.Contrat> GetContrats()
    {
      var items = this.context.Contrats.AsQueryable<Models.Crmdata.Contrat>();
      this.OnContratsRead(ref items);

      return items;
    }

    partial void OnContratsRead(ref IQueryable<Models.Crmdata.Contrat> items);

    partial void OnContratGet(ref SingleResult<Models.Crmdata.Contrat> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Contrat> GetContrat(int key)
    {
        var items = this.context.Contrats.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnContratGet(ref result);

        return result;
    }
    partial void OnContratDeleted(Models.Crmdata.Contrat item);
    partial void OnAfterContratDeleted(Models.Crmdata.Contrat item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteContrat(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Contrats
                .Where(i => i.Id == key)
                .Include(i => i.Taches)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnContratDeleted(item);
            this.context.Contrats.Remove(item);
            this.context.SaveChanges();
            this.OnAfterContratDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnContratUpdated(Models.Crmdata.Contrat item);
    partial void OnAfterContratUpdated(Models.Crmdata.Contrat item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutContrat(int key, [FromBody]Models.Crmdata.Contrat newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.Id != key))
            {
                return BadRequest();
            }

            this.OnContratUpdated(newItem);
            this.context.Contrats.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Contrats.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contact,StatutContrat");
            this.OnAfterContratUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchContrat(int key, [FromBody]Delta<Models.Crmdata.Contrat> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Contrats.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnContratUpdated(item);
            this.context.Contrats.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Contrats.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contact,StatutContrat");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnContratCreated(Models.Crmdata.Contrat item);
    partial void OnAfterContratCreated(Models.Crmdata.Contrat item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crmdata.Contrat item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnContratCreated(item);
            this.context.Contrats.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Contrats.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Contact,StatutContrat");

            this.OnAfterContratCreated(item);

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
