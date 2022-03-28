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

  [Route("odata/crmdata/Taches")]
  public partial class TachesController : ODataController
  {
    private Data.CrmdataContext context;

    public TachesController(Data.CrmdataContext context)
    {
      this.context = context;
    }
    // GET /odata/Crmdata/Taches
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crmdata.Tache> GetTaches()
    {
      var items = this.context.Taches.AsQueryable<Models.Crmdata.Tache>();
      this.OnTachesRead(ref items);

      return items;
    }

    partial void OnTachesRead(ref IQueryable<Models.Crmdata.Tache> items);

    partial void OnTacheGet(ref SingleResult<Models.Crmdata.Tache> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Tache> GetTache(int key)
    {
        var items = this.context.Taches.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnTacheGet(ref result);

        return result;
    }
    partial void OnTacheDeleted(Models.Crmdata.Tache item);
    partial void OnAfterTacheDeleted(Models.Crmdata.Tache item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteTache(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Taches
                .Where(i => i.Id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnTacheDeleted(item);
            this.context.Taches.Remove(item);
            this.context.SaveChanges();
            this.OnAfterTacheDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTacheUpdated(Models.Crmdata.Tache item);
    partial void OnAfterTacheUpdated(Models.Crmdata.Tache item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutTache(int key, [FromBody]Models.Crmdata.Tache newItem)
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

            this.OnTacheUpdated(newItem);
            this.context.Taches.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Taches.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contrat,TypesTache,StatutTache");
            this.OnAfterTacheUpdated(newItem);
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
    public IActionResult PatchTache(int key, [FromBody]Delta<Models.Crmdata.Tache> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Taches.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnTacheUpdated(item);
            this.context.Taches.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Taches.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contrat,TypesTache,StatutTache");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTacheCreated(Models.Crmdata.Tache item);
    partial void OnAfterTacheCreated(Models.Crmdata.Tache item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crmdata.Tache item)
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

            this.OnTacheCreated(item);
            this.context.Taches.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Taches.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Contrat,TypesTache,StatutTache");

            this.OnAfterTacheCreated(item);

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
