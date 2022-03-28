
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Crm.Models.Crmdata;

namespace Crm
{
    public partial class CrmdataService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public CrmdataService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/crmdata/");
        }

        public async System.Threading.Tasks.Task ExportContactsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContactsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetContacts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Crm.Models.Crmdata.Contact>> GetContacts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contacts");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContacts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Crm.Models.Crmdata.Contact>>(response);
        }
        partial void OnCreateContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.Contact> CreateContact(Crm.Models.Crmdata.Contact contact = default(Crm.Models.Crmdata.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnCreateContact(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.Contact>(response);
        }

        public async System.Threading.Tasks.Task ExportContratsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/contrats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/contrats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportContratsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/contrats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/contrats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetContrats(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Crm.Models.Crmdata.Contrat>> GetContrats(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Contrats");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContrats(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Crm.Models.Crmdata.Contrat>>(response);
        }
        partial void OnCreateContrat(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.Contrat> CreateContrat(Crm.Models.Crmdata.Contrat contrat = default(Crm.Models.Crmdata.Contrat))
        {
            var uri = new Uri(baseUri, $"Contrats");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contrat), Encoding.UTF8, "application/json");

            OnCreateContrat(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.Contrat>(response);
        }

        public async System.Threading.Tasks.Task ExportStatutContratsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/statutcontrats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/statutcontrats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStatutContratsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/statutcontrats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/statutcontrats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetStatutContrats(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Crm.Models.Crmdata.StatutContrat>> GetStatutContrats(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"StatutContrats");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStatutContrats(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Crm.Models.Crmdata.StatutContrat>>(response);
        }
        partial void OnCreateStatutContrat(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.StatutContrat> CreateStatutContrat(Crm.Models.Crmdata.StatutContrat statutContrat = default(Crm.Models.Crmdata.StatutContrat))
        {
            var uri = new Uri(baseUri, $"StatutContrats");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(statutContrat), Encoding.UTF8, "application/json");

            OnCreateStatutContrat(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.StatutContrat>(response);
        }

        public async System.Threading.Tasks.Task ExportStatutTachesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/statuttaches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/statuttaches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStatutTachesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/statuttaches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/statuttaches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetStatutTaches(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Crm.Models.Crmdata.StatutTache>> GetStatutTaches(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"StatutTaches");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStatutTaches(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Crm.Models.Crmdata.StatutTache>>(response);
        }
        partial void OnCreateStatutTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.StatutTache> CreateStatutTache(Crm.Models.Crmdata.StatutTache statutTache = default(Crm.Models.Crmdata.StatutTache))
        {
            var uri = new Uri(baseUri, $"StatutTaches");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(statutTache), Encoding.UTF8, "application/json");

            OnCreateStatutTache(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.StatutTache>(response);
        }

        public async System.Threading.Tasks.Task ExportTachesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/taches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/taches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTachesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/taches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/taches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetTaches(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Crm.Models.Crmdata.Tache>> GetTaches(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Taches");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTaches(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Crm.Models.Crmdata.Tache>>(response);
        }
        partial void OnCreateTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.Tache> CreateTache(Crm.Models.Crmdata.Tache tache = default(Crm.Models.Crmdata.Tache))
        {
            var uri = new Uri(baseUri, $"Taches");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tache), Encoding.UTF8, "application/json");

            OnCreateTache(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.Tache>(response);
        }

        public async System.Threading.Tasks.Task ExportTypesTachesToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/typestaches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/typestaches/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportTypesTachesToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crmdata/typestaches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crmdata/typestaches/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetTypesTaches(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<Crm.Models.Crmdata.TypesTache>> GetTypesTaches(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"TypesTaches");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTypesTaches(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<Crm.Models.Crmdata.TypesTache>>(response);
        }
        partial void OnCreateTypesTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.TypesTache> CreateTypesTache(Crm.Models.Crmdata.TypesTache typesTache = default(Crm.Models.Crmdata.TypesTache))
        {
            var uri = new Uri(baseUri, $"TypesTaches");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(typesTache), Encoding.UTF8, "application/json");

            OnCreateTypesTache(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.TypesTache>(response);
        }
        partial void OnDeleteContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteContact(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetContactById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.Contact> GetContactById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContactById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.Contact>(response);
        }
        partial void OnUpdateContact(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateContact(int? id = default(int?), Crm.Models.Crmdata.Contact contact = default(Crm.Models.Crmdata.Contact))
        {
            var uri = new Uri(baseUri, $"Contacts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

            OnUpdateContact(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteContrat(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteContrat(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contrats({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteContrat(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetContratById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.Contrat> GetContratById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Contrats({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetContratById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.Contrat>(response);
        }
        partial void OnUpdateContrat(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateContrat(int? id = default(int?), Crm.Models.Crmdata.Contrat contrat = default(Crm.Models.Crmdata.Contrat))
        {
            var uri = new Uri(baseUri, $"Contrats({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(contrat), Encoding.UTF8, "application/json");

            OnUpdateContrat(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteStatutContrat(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteStatutContrat(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"StatutContrats({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteStatutContrat(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetStatutContratById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.StatutContrat> GetStatutContratById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"StatutContrats({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStatutContratById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.StatutContrat>(response);
        }
        partial void OnUpdateStatutContrat(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateStatutContrat(int? id = default(int?), Crm.Models.Crmdata.StatutContrat statutContrat = default(Crm.Models.Crmdata.StatutContrat))
        {
            var uri = new Uri(baseUri, $"StatutContrats({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(statutContrat), Encoding.UTF8, "application/json");

            OnUpdateStatutContrat(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteStatutTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteStatutTache(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"StatutTaches({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteStatutTache(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetStatutTacheById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.StatutTache> GetStatutTacheById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"StatutTaches({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStatutTacheById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.StatutTache>(response);
        }
        partial void OnUpdateStatutTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateStatutTache(int? id = default(int?), Crm.Models.Crmdata.StatutTache statutTache = default(Crm.Models.Crmdata.StatutTache))
        {
            var uri = new Uri(baseUri, $"StatutTaches({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(statutTache), Encoding.UTF8, "application/json");

            OnUpdateStatutTache(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTache(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Taches({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTache(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTacheById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.Tache> GetTacheById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Taches({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTacheById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.Tache>(response);
        }
        partial void OnUpdateTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTache(int? id = default(int?), Crm.Models.Crmdata.Tache tache = default(Crm.Models.Crmdata.Tache))
        {
            var uri = new Uri(baseUri, $"Taches({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(tache), Encoding.UTF8, "application/json");

            OnUpdateTache(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteTypesTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteTypesTache(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TypesTaches({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteTypesTache(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetTypesTacheById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Crm.Models.Crmdata.TypesTache> GetTypesTacheById(string expand = default(string), int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"TypesTaches({id})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetTypesTacheById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Crm.Models.Crmdata.TypesTache>(response);
        }
        partial void OnUpdateTypesTache(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateTypesTache(int? id = default(int?), Crm.Models.Crmdata.TypesTache typesTache = default(Crm.Models.Crmdata.TypesTache))
        {
            var uri = new Uri(baseUri, $"TypesTaches({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(typesTache), Encoding.UTF8, "application/json");

            OnUpdateTypesTache(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
