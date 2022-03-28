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
    public partial class TypesTachesComponent : ComponentBase
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
        protected RadzenDataGrid<Crm.Models.Crmdata.TypesTache> grid0;

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

        IEnumerable<Crm.Models.Crmdata.TypesTache> _getTypesTachesResult;
        protected IEnumerable<Crm.Models.Crmdata.TypesTache> getTypesTachesResult
        {
            get
            {
                return _getTypesTachesResult;
            }
            set
            {
                if (!object.Equals(_getTypesTachesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTypesTachesResult", NewValue = value, OldValue = _getTypesTachesResult };
                    _getTypesTachesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getTypesTachesCount;
        protected int getTypesTachesCount
        {
            get
            {
                return _getTypesTachesCount;
            }
            set
            {
                if (!object.Equals(_getTypesTachesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTypesTachesCount", NewValue = value, OldValue = _getTypesTachesCount };
                    _getTypesTachesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddTypesTache>("Add Types Tache", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmdataGetTypesTachesResult = await Crmdata.GetTypesTaches(filter:$@"(contains(Name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getTypesTachesResult = crmdataGetTypesTachesResult.Value.AsODataEnumerable();

                getTypesTachesCount = crmdataGetTypesTachesResult.Count;
            }
            catch (System.Exception crmdataGetTypesTachesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load TypesTaches" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<Crm.Models.Crmdata.TypesTache> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTypesTache>("Edit Types Tache", new Dictionary<string, object>() { {"Id", args.Data.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var crmdataDeleteTypesTacheResult = await Crmdata.DeleteTypesTache(id:data.Id);
                    if (crmdataDeleteTypesTacheResult != null && crmdataDeleteTypesTacheResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (crmdataDeleteTypesTacheResult != null && crmdataDeleteTypesTacheResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete TypesTache" });
                    }
                }
            }
            catch (System.Exception crmdataDeleteTypesTacheException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete TypesTache" });
            }
        }
    }
}
