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
    public partial class StatutContratsComponent : ComponentBase
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
        protected RadzenDataGrid<Crm.Models.Crmdata.StatutContrat> grid0;

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

        IEnumerable<Crm.Models.Crmdata.StatutContrat> _getStatutContratsResult;
        protected IEnumerable<Crm.Models.Crmdata.StatutContrat> getStatutContratsResult
        {
            get
            {
                return _getStatutContratsResult;
            }
            set
            {
                if (!object.Equals(_getStatutContratsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStatutContratsResult", NewValue = value, OldValue = _getStatutContratsResult };
                    _getStatutContratsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getStatutContratsCount;
        protected int getStatutContratsCount
        {
            get
            {
                return _getStatutContratsCount;
            }
            set
            {
                if (!object.Equals(_getStatutContratsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStatutContratsCount", NewValue = value, OldValue = _getStatutContratsCount };
                    _getStatutContratsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddStatutContrat>("Add Statut Contrat", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmdataGetStatutContratsResult = await Crmdata.GetStatutContrats(filter:$@"(contains(Name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getStatutContratsResult = crmdataGetStatutContratsResult.Value.AsODataEnumerable();

                getStatutContratsCount = crmdataGetStatutContratsResult.Count;
            }
            catch (System.Exception crmdataGetStatutContratsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load StatutContrats" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Crm.Models.Crmdata.StatutContrat> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditStatutContrat>("Edit Statut Contrat", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmdataDeleteStatutContratResult = await Crmdata.DeleteStatutContrat(id:data.Id);
                    if (crmdataDeleteStatutContratResult != null && crmdataDeleteStatutContratResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmdataDeleteStatutContratResult != null && crmdataDeleteStatutContratResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete StatutContrat" });
                    }
                }
            }
            catch (System.Exception crmdataDeleteStatutContratException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete StatutContrat" });
            }
        }
    }
}
