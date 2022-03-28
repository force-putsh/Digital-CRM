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

  [Route("odata/crmdata/Contacts")]
  public partial class ContactsController : ODataController
  {
    private Data.CrmdataContext context;

    public ContactsController(Data.CrmdataContext context)
    {
      this.context = context;
    }
    // GET /odata/Crmdata/Contacts
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crmdata.Contact> GetContacts()
    {
      var items = this.context.Contacts.AsQueryable<Models.Crmdata.Contact>();
      this.OnContactsRead(ref items);

      return items;
    }

    partial void OnContactsRead(ref IQueryable<Models.Crmdata.Contact> items);

    partial void OnContactGet(ref SingleResult<Models.Crmdata.Contact> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Contact> GetContact(int key)
    {
        var items = this.context.Contacts.Where(i=>i.Id == key);
        var result = SingleResult.Create(items);

        OnContactGet(ref result);

        return result;
    }
    partial void OnContactDeleted(Models.Crmdata.Contact item);
    partial void OnAfterContactDeleted(Models.Crmdata.Contact item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteContact(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Contacts
                .Where(i => i.Id == key)
                .Include(i => i.Contrats)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnContactDeleted(item);
            this.context.Contacts.Remove(item);
            this.context.SaveChanges();
            this.OnAfterContactDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnContactUpdated(Models.Crmdata.Contact item);
    partial void OnAfterContactUpdated(Models.Crmdata.Contact item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutContact(int key, [FromBody]Models.Crmdata.Contact newItem)
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

            this.OnContactUpdated(newItem);
            this.context.Contacts.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Contacts.Where(i => i.Id == key);
            this.OnAfterContactUpdated(newItem);
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
    public IActionResult PatchContact(int key, [FromBody]Delta<Models.Crmdata.Contact> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Contacts.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnContactUpdated(item);
            this.context.Contacts.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Contacts.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnContactCreated(Models.Crmdata.Contact item);
    partial void OnAfterContactCreated(Models.Crmdata.Contact item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crmdata.Contact item)
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

            this.OnContactCreated(item);
            this.context.Contacts.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crmdata/Contacts/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
