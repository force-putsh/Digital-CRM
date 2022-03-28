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
    public partial class StatutTachesComponent : ComponentBase
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
        protected RadzenDataGrid<Crm.Models.Crmdata.StatutTache> grid0;

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

        IEnumerable<Crm.Models.Crmdata.StatutTache> _getStatutTachesResult;
        protected IEnumerable<Crm.Models.Crmdata.StatutTache> getStatutTachesResult
        {
            get
            {
                return _getStatutTachesResult;
            }
            set
            {
                if (!object.Equals(_getStatutTachesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStatutTachesResult", NewValue = value, OldValue = _getStatutTachesResult };
                    _getStatutTachesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getStatutTachesCount;
        protected int getStatutTachesCount
        {
            get
            {
                return _getStatutTachesCount;
            }
            set
            {
                if (!object.Equals(_getStatutTachesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getStatutTachesCount", NewValue = value, OldValue = _getStatutTachesCount };
                    _getStatutTachesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddStatutTache>("Add Statut Tache", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmdataGetStatutTachesResult = await Crmdata.GetStatutTaches(filter:$@"(contains(Name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getStatutTachesResult = crmdataGetStatutTachesResult.Value.AsODataEnumerable();

                getStatutTachesCount = crmdataGetStatutTachesResult.Count;
            }
            catch (System.Exception crmdataGetStatutTachesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load StatutTaches" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Crm.Models.Crmdata.StatutTache> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditStatutTache>("Edit Statut Tache", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmdataDeleteStatutTacheResult = await Crmdata.DeleteStatutTache(id:data.Id);
                    if (crmdataDeleteStatutTacheResult != null && crmdataDeleteStatutTacheResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmdataDeleteStatutTacheResult != null && crmdataDeleteStatutTacheResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete StatutTache" });
                    }
                }
            }
            catch (System.Exception crmdataDeleteStatutTacheException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete StatutTache" });
            }
        }
    }
}
