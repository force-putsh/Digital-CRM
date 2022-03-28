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
    public partial class EditStatutTacheComponent : ComponentBase
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

        Crm.Models.Crmdata.StatutTache _statuttache;
        protected Crm.Models.Crmdata.StatutTache statuttache
        {
            get
            {
                return _statuttache;
            }
            set
            {
                if (!object.Equals(_statuttache, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "statuttache", NewValue = value, OldValue = _statuttache };
                    _statuttache = value;
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
            var crmdataGetStatutTacheByIdResult = await Crmdata.GetStatutTacheById(id:Id);
            statuttache = crmdataGetStatutTacheByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(Crm.Models.Crmdata.StatutTache args)
        {
            try
            {
                var crmdataUpdateStatutTacheResult = await Crmdata.UpdateStatutTache(id:Id, statutTache:statuttache);
                DialogService.Close(statuttache);
            }
            catch (System.Exception crmdataUpdateStatutTacheException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update StatutTache" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
