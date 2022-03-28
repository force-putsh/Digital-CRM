using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Crm.Models.Crmdata;
using Microsoft.AspNetCore.Identity;
using Crm.Models;
using Crm.Client.Pages;

namespace Crm.Pages
{
    public partial class ContratsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected CrmdataService Crmdata { get; set; }
        protected RadzenDataGrid<Crm.Models.Crmdata.Contrat> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Crm.Models.Crmdata.Contrat> _getContratsResult;
        protected IEnumerable<Crm.Models.Crmdata.Contrat> getContratsResult
        {
            get
            {
                return _getContratsResult;
            }
            set
            {
                if (!object.Equals(_getContratsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContratsResult", NewValue = value, OldValue = _getContratsResult };
                    _getContratsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getContratsCount;
        protected int getContratsCount
        {
            get
            {
                return _getContratsCount;
            }
            set
            {
                if (!object.Equals(_getContratsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContratsCount", NewValue = value, OldValue = _getContratsCount };
                    _getContratsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddContrat>("Add Contrat", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Crmdata.ExportContratsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Contact,StatutContrat", Select = "Id,Amount,Name,UserId,Contact.Email as ContactEmail,StatutContrat.Name as StatutContratName,CloseDate" }, $"Contrats");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Crmdata.ExportContratsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Contact,StatutContrat", Select = "Id,Amount,Name,UserId,Contact.Email as ContactEmail,StatutContrat.Name as StatutContratName,CloseDate" }, $"Contrats");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmdataGetContratsResult = await Crmdata.GetContrats(filter:$@"(contains(Name,""{search}"") or contains(UserId,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Contact,StatutContrat,User", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getContratsResult = crmdataGetContratsResult.Value.AsODataEnumerable();

                getContratsCount = crmdataGetContratsResult.Count;
            }
            catch (System.Exception crmdataGetContratsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Contrats" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Crm.Models.Crmdata.Contrat> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditContrat>("Edit Contrat", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmdataDeleteContratResult = await Crmdata.DeleteContrat(id:data.Id);
                    if (crmdataDeleteContratResult != null && crmdataDeleteContratResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmdataDeleteContratResult != null && crmdataDeleteContratResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Contrat" });
                    }
                }
            }
            catch (System.Exception crmdataDeleteContratException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Contrat" });
            }
        }
    }
}
