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

  [Route("odata/crmdata/TypesTaches")]
  public partial class TypesTachesController : ODataController
  {
    private Data.CrmdataContext context;

    public TypesTachesController(Data.CrmdataContext context)
    {
      this.context = context;
    }
    // GET /odata/Crmdata/TypesTaches
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crmdata.TypesTache> GetTypesTaches()
    {
      var items = this.context.TypesTaches.AsQueryable<Models.Crmdata.TypesTache>();
      this.OnTypesTachesRead(ref items);

      return items;
    }

    partial void OnTypesTachesRead(ref IQueryable<Models.Crmdata.TypesTache> items);

    partial void OnTypesTacheGet(ref SingleResult<Models.Crmdata.TypesTache> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<TypesTache> GetTypesTache(int key)
    {
        var items = this.context.TypesTaches.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnTypesTacheGet(ref result);

        return result;
    }
    partial void OnTypesTacheDeleted(Models.Crmdata.TypesTache item);
    partial void OnAfterTypesTacheDeleted(Models.Crmdata.TypesTache item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteTypesTache(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.TypesTaches
                .Where(i => i.Id == key)
                .Include(i => i.Taches)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnTypesTacheDeleted(item);
            this.context.TypesTaches.Remove(item);
            this.context.SaveChanges();
            this.OnAfterTypesTacheDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTypesTacheUpdated(Models.Crmdata.TypesTache item);
    partial void OnAfterTypesTacheUpdated(Models.Crmdata.TypesTache item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutTypesTache(int key, [FromBody]Models.Crmdata.TypesTache newItem)
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

            this.OnTypesTacheUpdated(newItem);
            this.context.TypesTaches.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.TypesTaches.Where(i => i.Id == key);
            this.OnAfterTypesTacheUpdated(newItem);
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
    public IActionResult PatchTypesTache(int key, [FromBody]Delta<Models.Crmdata.TypesTache> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.TypesTaches.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnTypesTacheUpdated(item);
            this.context.TypesTaches.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.TypesTaches.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTypesTacheCreated(Models.Crmdata.TypesTache item);
    partial void OnAfterTypesTacheCreated(Models.Crmdata.TypesTache item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crmdata.TypesTache item)
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

            this.OnTypesTacheCreated(item);
            this.context.TypesTaches.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crmdata/TypesTaches/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
