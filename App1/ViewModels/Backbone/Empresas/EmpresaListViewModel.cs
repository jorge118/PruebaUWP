using App1.Models.Responses.Backbone.Empresas;
using App1.Services.DataStores.Backbone;
using App1.ViewModels.Base;
using App1.Views.Backbone.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels.Backbone.Empresas
{
    public sealed class EmpresaListViewModel : BaseListViewModel<EmpresaDataStore, EmpresaResponse, EmpresaDetailPage>
    {

        public EmpresaListViewModel()
        {
            Title = "Empresas";
        }

    }
}
