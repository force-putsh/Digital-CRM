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

  [Route("odata/crmdata/StatutContrats")]
  public partial class StatutContratsController : ODataController
  {
    private Data.CrmdataContext context;

    public StatutContratsController(Data.CrmdataContext context)
    {
      this.context = context;
    }
    // GET /odata/Crmdata/StatutContrats
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crmdata.StatutContrat> GetStatutContrats()
    {
      var items = this.context.StatutContrats.AsQueryable<Models.Crmdata.StatutContrat>();
      this.OnStatutContratsRead(ref items);

      return items;
    }

    partial void OnStatutContratsRead(ref IQueryable<Models.Crmdata.StatutContrat> items);

    partial void OnStatutContratGet(ref SingleResult<Models.Crmdata.StatutContrat> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<StatutContrat> GetStatutContrat(int key)
    {
        var items = this.context.StatutContrats.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnStatutContratGet(ref result);

        return result;
    }
    partial void OnStatutContratDeleted(Models.Crmdata.StatutContrat item);
    partial void OnAfterStatutContratDeleted(Models.Crmdata.StatutContrat item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteStatutContrat(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.StatutContrats
                .Where(i => i.Id == key)
                .Include(i => i.Contrats)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnStatutContratDeleted(item);
            this.context.StatutContrats.Remove(item);
            this.context.SaveChanges();
            this.OnAfterStatutContratDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStatutContratUpdated(Models.Crmdata.StatutContrat item);
    partial void OnAfterStatutContratUpdated(Models.Crmdata.StatutContrat item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutStatutContrat(int key, [FromBody]Models.Crmdata.StatutContrat newItem)
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

            this.OnStatutContratUpdated(newItem);
            this.context.StatutContrats.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.StatutContrats.Where(i => i.Id == key);
            this.OnAfterStatutContratUpdated(newItem);
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
    public IActionResult PatchStatutContrat(int key, [FromBody]Delta<Models.Crmdata.StatutContrat> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.StatutContrats.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnStatutContratUpdated(item);
            this.context.StatutContrats.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.StatutContrats.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStatutContratCreated(Models.Crmdata.StatutContrat item);
    partial void OnAfterStatutContratCreated(Models.Crmdata.StatutContrat item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crmdata.StatutContrat item)
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

            this.OnStatutContratCreated(item);
            this.context.StatutContrats.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crmdata/StatutContrats/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
