
using App1.Models.Responses.Finanzas.Turnos;
using App1.Services.DataStores.Finanzas;
using App1.ViewModels.Base;
using App1.Views.Finanzas.Turnos;

namespace App1.ViewModels.Finanzas.Turnos
{
    public sealed class TurnoListViewModel : BaseListViewModel<TurnoDataStore, TurnoResponse, TurnoDetailPage>
    {
        public TurnoListViewModel() 
        {
            Title = "Turnos";
        }
    }
}
