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
    public partial class EditTacheComponent : ComponentBase
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

        [Parameter]
        public dynamic Id { get; set; }

        Crm.Models.Crmdata.Tache _tache;
        protected Crm.Models.Crmdata.Tache tache
        {
            get
            {
                return _tache;
            }
            set
            {
                if (!object.Equals(_tache, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "tache", NewValue = value, OldValue = _tache };
                    _tache = value;
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
            var crmdataGetTacheByIdResult = await Crmdata.GetTacheById(id:Id);
            tache = crmdataGetTacheByIdResult;

            var crmdataGetContratsResult = await Crmdata.GetContrats();
            getContratsResult = crmdataGetContratsResult.Value.AsODataEnumerable();

            var crmdataGetTypesTachesResult = await Crmdata.GetTypesTaches();
            getTypesTachesResult = crmdataGetTypesTachesResult.Value.AsODataEnumerable();

            var crmdataGetStatutTachesResult = await Crmdata.GetStatutTaches();
            getStatutTachesResult = crmdataGetStatutTachesResult.Value.AsODataEnumerable();
        }

        protected async System.Threading.Tasks.Task Form0Submit(Crm.Models.Crmdata.Tache args)
        {
            try
            {
                var crmdataUpdateTacheResult = await Crmdata.UpdateTache(id:Id, tache:tache);
                DialogService.Close(tache);
            }
            catch (System.Exception crmdataUpdateTacheException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Tache" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
