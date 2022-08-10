using App1.Controls;
using App1.Helpers;
using App1.Models.Common;
using App1.Models.Locals.Backbone.Empresas;
using App1.Models.Requests.Backbone.Empresas;
using App1.Models.Requests.Backbone.FormasPago;
using App1.Models.Responses.Backbone.Cortes;
using App1.Models.Responses.Backbone.Empresas;
using App1.Services;
using App1.Services.DataStores.Backbone;
using App1.Services.Infrastructure;
using App1.ViewModels.Base;
using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace App1.ViewModels.Backbone.Empresas
{
    public sealed class EmpresaDetailViewModel : BaseDetailViewModel<EmpresaResponse, EmpresaModel, EmpresaRequest, Guid, EmpresaDataStore>
    {
        public EmpresaDetailViewModel()
        {

        }

        //public EmpresaDetailViewModel(Func<EmpresaResponse, Task> onModelCreated, Func<EmpresaResponse, Task> onModelUpdated, Action onViewRequestTypeChanged) : base(onModelCreated, onModelUpdated, onViewRequestTypeChanged)
        //{

        //}

        //public EmpresaDetailViewModel(EmpresaResponse model, Func<EmpresaResponse, Task> onModelCreated, Func<EmpresaResponse, Task> onModelUpdated, Action onViewRequestTypeChanged, ViewRequestType viewRequestType) : base(model, onModelCreated, onModelUpdated, onViewRequestTypeChanged, viewRequestType)
        //{
        //}

        public bool IsDataGridEnabledActions
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public BitmapImage ImagenLogo
        {
            get => GetProperty<BitmapImage>();
            set => SetProperty(value);
        }

        public ObservableCollection<EmpresaCorteModel> Cortes { get; set; } = new ObservableCollection<EmpresaCorteModel>();
        public ObservableCollection<EmpresaFormaPagoModel> FormasPagos { get; set; } = new ObservableCollection<EmpresaFormaPagoModel>();

        public override ICommand SaveCommand => new RelayCommand(async () => await ExecutingBusy(OnSave, Messages.ConfirmSaveMessage, Messages.SuccessSaveMessage));

        public ICommand CancelarCommand => new RelayCommand(async () => await ExecutingBusy(CancelarEmpresa, Messages.CancelarMessageConfirm, Messages.CancelarMessageSuccess));

        public ICommand AddCorteCommand => new RelayCommand(async () => await ExecutingBusy(OpenAddCortePage));

        public ICommand DeleteItemCommand => new RelayCommand<EmpresaCorteModel>(async (item) => await ExecutingBusy(item, EliminarCorte, Messages.EliminarCorteMessageConfirm, Messages.EliminarCorteMessageSuccess));

        public ICommand AddFormaPagoCommand => new RelayCommand(async () => await ExecutingBusy(OpenAddFormaPago));

        public ICommand DeleteFormaPagoCommand => new RelayCommand<EmpresaFormaPagoModel>(async (item) => await ExecutingBusy(item, EliminarFormaPago, Messages.EliminarFormaPagoMessageConfirm, Messages.EliminarFormaPagoMessageSuccess));

        public ICommand LoadLogoCommand => new RelayCommand(async () => await OpenImage());

        protected override EmpresaRequest CreateRequestForDataStore() => App.Container.Resolve<IMapper>().Map<EmpresaRequest>(LocalModel);

        protected override async  Task SetModel(EmpresaResponse model)
        {
            Title = model.RazonSocial;

            App.Container.Resolve<IMapper>().Map(model, LocalModel);

            ImagenLogo = await ImageHelper.ImageFromBytes(LocalModel.Logo);

            await LoadData();

            IsDataGridEnabledActions = true;

            //ViewRequestType = ViewRequestType.Read;
        }

        protected override void ValidateModel()
        {
            throw new NotImplementedException();
        }

        private async Task LoadData()
        {
            IEnumerable<EmpresaCorteResponse> cortes = null;
            IEnumerable<EmpresaFormaPagoResponse> formaPagos = null;

            await ExecutingBusy(async () => await Task.Run(async () =>
            {
                cortes = (await DataStore.GetCortes(LocalModel.Id)).Results;
                formaPagos = (await DataStore.GetFormasPago(LocalModel.Id)).Results;
            }));

            if (cortes != null && cortes.Any())
            {
                Cortes?.Clear();

                cortes.Select(x => App.Container.Resolve<IMapper>().Map<EmpresaCorteModel>(x)).ToList().ForEach(x => Cortes.Add(x));

            }

            if (formaPagos != null && formaPagos.Any())
            {
                FormasPagos?.Clear();

                formaPagos.Select(x => App.Container.Resolve<IMapper>().Map<EmpresaFormaPagoModel>(x)).ToList().ForEach(e => FormasPagos.Add(e));
            }
        }

        private async Task CancelarEmpresa()
        {
            await DataStore.CancelarEmpresa(LocalModel.Id);
            Model.Estatus = EstatusRegistro.Cancelado;
            LocalModel.Estatus = EstatusRegistro.Cancelado;

            OnViewRequestTypeChanged();
        }

        private async Task EliminarCorte(EmpresaCorteModel item)
        {
            await DataStore.EliminarCorte(LocalModel.Id, item.Id);

            Cortes.Remove(item);
        }

        private async Task OpenAddCortePage()
        {
            //if (ViewRequestType == ViewRequestType.Read && LocalModel.Estatus == EstatusRegistro.Activo)
            //    await Navigation.PushModalAsync(new CorteSearchListPage(OnCortesAdded));

        }

        private async Task OnCortesAdded(List<CorteResponse> cortes)
        {
            if (cortes.Count <= 0)
                return;

            foreach (var corte in cortes.Where(e => !Cortes.Select(d => d.CorteId).Contains(e.Id)))
            {
                var response = await DataStore.AgregarCorte(new AddCorteToEmpresaRequest { EmpresaId = LocalModel.Id, CorteId = corte.Id });

                var empresaCorte = App.Container.Resolve<IMapper>().Map<EmpresaCorteModel>(response);

                Cortes.Add(empresaCorte);
            }

        }

        private async Task OpenAddFormaPago()
        {
            //if (ViewRequestType == ViewRequestType.Read && LocalModel.Estatus == EstatusRegistro.Activo)
            //    await Navigation.PushModalAsync(new SearchFormaPagoListPage(OnFormasPagoAdded));
        }

        private async Task OnFormasPagoAdded(List<FormaPagoResponse> formasPago)
        {
            if (formasPago.Count <= 0)
                return;

            foreach (var formaPago in formasPago.Where(e => !FormasPagos.Select(d => d.FormaPagoId).Contains(e.Id)))
            {
                var response = await DataStore.AgregarFormaPago(new AddFormaPagoToEmpresaRequest { EmpresaId = LocalModel.Id, FormaPagoId = formaPago.Id });

                FormasPagos.Add(App.Container.Resolve<IMapper>().Map<EmpresaFormaPagoModel>(response));
            }
        }

        private async Task EliminarFormaPago(EmpresaFormaPagoModel item)
        {
            await DataStore.EliminarFormaPago(LocalModel.Id, item.Id);

            FormasPagos.Remove(item);
        }

        private async Task OpenImage()
        {

            var result = await App.Container.Resolve<IFilePickerService>().OpenImagePickerAsync();

            if (result != null)
            {
                LocalModel.Logo = result.ImageBytes;

                ImagenLogo = await ImageHelper.ImageFromBytes(result.ImageBytes);
            }

        }

        //private void SetOnViewRequestTypeChanged(Action onViewRequestChanged)
        //{
        //    OnViewRequestTypeChanged = async () =>
        //    {
        //        onViewRequestChanged();

        //        IsDataGridEnabledActions = ViewRequestType == ViewRequestType.Read && LocalModel.Estatus == EstatusRegistro.Activo;

        //        await OnModelUpdated(Model);
        //    };
        //}

        internal class Messages
        {
            internal const string ConfirmDiscardMessage = "¿Está seguro que desea descartar los cambios?";
            internal const string ConfirmSaveMessage = "¿Está seguro que desea guardar los cambios?";
            internal const string SuccessSaveMessage = "Empresa guardada exitosamente";
            public const string RazonSocialCannotEmpty = "La razón social no puede estar vacía.";
            public const string RfcCannotEmpty = "El RFC no puede estar vacío.";
            public const string CancelarMessageConfirm = "¿Desea cancelar la empresa?";
            public const string CancelarMessageSuccess = "Empresa cancelada correctamente";
            public const string EliminarCorteMessageConfirm = "¿Desea eliminar el corte seleccionado?";
            public const string EliminarCorteMessageSuccess = "Corte eliminado correctamente";
            public const string EliminarFormaPagoMessageConfirm = "¿Desea eliminar la forma de pago seleccionada?";
            public const string EliminarFormaPagoMessageSuccess = "Forma de pago eliminada correctamente";
            public const string LogoMaxSize = "El tamaño de la imagen no debe ser mayor a a 4.00 MB";
            public const string LogoCanntotBeEmpty = "Favor de seleccionar una imagen para el logo";
        }
    }
}
