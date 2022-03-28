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
    public partial class AddContratComponent : ComponentBase
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

        IEnumerable<Crm.Models.Crmdata.Contact> _getContactsResult;
        protected IEnumerable<Crm.Models.Crmdata.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if (!object.Equals(_getContactsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContactsResult", NewValue = value, OldValue = _getContactsResult };
                    _getContactsResult = value;
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

        Crm.Models.Crmdata.Contrat _contrat;
        protected Crm.Models.Crmdata.Contrat contrat
        {
            get
            {
                return _contrat;
            }
            set
            {
                if (!object.Equals(_contrat, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "contrat", NewValue = value, OldValue = _contrat };
                    _contrat = value;
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
            var crmdataGetContactsResult = await Crmdata.GetContacts();
            getContactsResult = crmdataGetContactsResult.Value.AsODataEnumerable();

            var crmdataGetStatutContratsResult = await Crmdata.GetStatutContrats();
            getStatutContratsResult = crmdataGetStatutContratsResult.Value.AsODataEnumerable();

            contrat = new Crm.Models.Crmdata.Contrat(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Crm.Models.Crmdata.Contrat args)
        {
            try
            {
                var crmdataCreateContratResult = await Crmdata.CreateContrat(contrat:contrat);
                DialogService.Close(contrat);
            }
            catch (System.Exception crmdataCreateContratException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Contrat!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
