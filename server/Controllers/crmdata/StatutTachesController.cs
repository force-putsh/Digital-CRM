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

  [Route("odata/crmdata/StatutTaches")]
  public partial class StatutTachesController : ODataController
  {
    private Data.CrmdataContext context;

    public StatutTachesController(Data.CrmdataContext context)
    {
      this.context = context;
    }
    // GET /odata/Crmdata/StatutTaches
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crmdata.StatutTache> GetStatutTaches()
    {
      var items = this.context.StatutTaches.AsQueryable<Models.Crmdata.StatutTache>();
      this.OnStatutTachesRead(ref items);

      return items;
    }

    partial void OnStatutTachesRead(ref IQueryable<Models.Crmdata.StatutTache> items);

    partial void OnStatutTacheGet(ref SingleResult<Models.Crmdata.StatutTache> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<StatutTache> GetStatutTache(int key)
    {
        var items = this.context.StatutTaches.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnStatutTacheGet(ref result);

        return result;
    }
    partial void OnStatutTacheDeleted(Models.Crmdata.StatutTache item);
    partial void OnAfterStatutTacheDeleted(Models.Crmdata.StatutTache item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteStatutTache(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.StatutTaches
                .Where(i => i.Id == key)
                .Include(i => i.Taches)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnStatutTacheDeleted(item);
            this.context.StatutTaches.Remove(item);
            this.context.SaveChanges();
            this.OnAfterStatutTacheDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStatutTacheUpdated(Models.Crmdata.StatutTache item);
    partial void OnAfterStatutTacheUpdated(Models.Crmdata.StatutTache item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutStatutTache(int key, [FromBody]Models.Crmdata.StatutTache newItem)
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

            this.OnStatutTacheUpdated(newItem);
            this.context.StatutTaches.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.StatutTaches.Where(i => i.Id == key);
            this.OnAfterStatutTacheUpdated(newItem);
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
    public IActionResult PatchStatutTache(int key, [FromBody]Delta<Models.Crmdata.StatutTache> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.StatutTaches.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnStatutTacheUpdated(item);
            this.context.StatutTaches.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.StatutTaches.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStatutTacheCreated(Models.Crmdata.StatutTache item);
    partial void OnAfterStatutTacheCreated(Models.Crmdata.StatutTache item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crmdata.StatutTache item)
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

            this.OnStatutTacheCreated(item);
            this.context.StatutTaches.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crmdata/StatutTaches/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
