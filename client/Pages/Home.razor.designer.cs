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
    public partial class HomeComponent : ComponentBase
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
        protected RadzenDataGrid<Crm.Models.Crmdata.Contrat> datagrid0;
        protected RadzenDataGrid<Crm.Models.Crmdata.Tache> datagrid1;

        Crm.Pages.Stats _monthlyStats;
        protected Crm.Pages.Stats monthlyStats
        {
            get
            {
                return _monthlyStats;
            }
            set
            {
                if (!object.Equals(_monthlyStats, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "monthlyStats", NewValue = value, OldValue = _monthlyStats };
                    _monthlyStats = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Crm.Pages.RevenueByCompany> _revenueByCompany;
        protected IEnumerable<Crm.Pages.RevenueByCompany> revenueByCompany
        {
            get
            {
                return _revenueByCompany;
            }
            set
            {
                if (!object.Equals(_revenueByCompany, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "revenueByCompany", NewValue = value, OldValue = _revenueByCompany };
                    _revenueByCompany = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Crm.Pages.RevenueByMonth> _revenueByMonth;
        protected IEnumerable<Crm.Pages.RevenueByMonth> revenueByMonth
        {
            get
            {
                return _revenueByMonth;
            }
            set
            {
                if (!object.Equals(_revenueByMonth, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "revenueByMonth", NewValue = value, OldValue = _revenueByMonth };
                    _revenueByMonth = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Crm.Pages.RevenueByEmployee> _revenueByEmployee;
        protected IEnumerable<Crm.Pages.RevenueByEmployee> revenueByEmployee
        {
            get
            {
                return _revenueByEmployee;
            }
            set
            {
                if (!object.Equals(_revenueByEmployee, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "revenueByEmployee", NewValue = value, OldValue = _revenueByEmployee };
                    _revenueByEmployee = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Crm.Models.Crmdata.Contrat> _getOpportunitiesResult;
        protected IEnumerable<Crm.Models.Crmdata.Contrat> getOpportunitiesResult
        {
            get
            {
                return _getOpportunitiesResult;
            }
            set
            {
                if (!object.Equals(_getOpportunitiesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunitiesResult", NewValue = value, OldValue = _getOpportunitiesResult };
                    _getOpportunitiesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Crm.Models.Crmdata.Tache> _getTasksResult;
        protected IEnumerable<Crm.Models.Crmdata.Tache> getTasksResult
        {
            get
            {
                return _getTasksResult;
            }
            set
            {
                if (!object.Equals(_getTasksResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getTasksResult", NewValue = value, OldValue = _getTasksResult };
                    _getTasksResult = value;
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
            var monthlyStatsResult = await MonthlyStats();
            monthlyStats = monthlyStatsResult;

            var revenueByCompanyResult = await RevenueByCompany();
            revenueByCompany = revenueByCompanyResult;

            var revenueByMonthResult = await RevenueByMonth();
            revenueByMonth = revenueByMonthResult;

            var revenueByEmployeeResult = await RevenueByEmployee();
            revenueByEmployee = revenueByEmployeeResult;

            var crmdataGetContratsResult = await Crmdata.GetContrats(orderby:$@"CloseDate desc", expand:$"Contact,StatutContrat");
            getOpportunitiesResult = crmdataGetContratsResult.Value;

            var crmdataGetTachesResult = await Crmdata.GetTaches(orderby:$@"DueDate desc", expand:$"Contrat($expand=User,Contact)");
            getTasksResult = crmdataGetTachesResult.Value;
        }
    }
}
