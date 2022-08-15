using App1.Controls;
using App1.Exceptions;
using App1.Models.Common;
using App1.Models.Locals.Finanzas.Turnos;
using App1.Models.Requests.Finanzas.Turnos;
using App1.Models.Response.Backbone.Cortes;
using App1.Models.Responses.Backbone.Empresas;
using App1.Models.Responses.Finanzas.Turnos;
using App1.Services.DataStores.Backbone;
using App1.Services.DataStores.Finanzas;
using App1.ViewModels.Base;
using App1.Views.Finanzas.Turnos;
using Autofac;
using AutoMapper;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Core;

namespace App1.ViewModels.Finanzas.Turnos
{
    public sealed class TurnoDetailViewModel : BaseDetailViewModel<TurnoResponse, TurnoModel, TurnoRequest, Guid, TurnoDataStore>
    {
        public TurnoDetailViewModel(Func<TurnoResponse, Task> onModelCreated, Func<TurnoResponse, Task> onModelUpdated,Action onViewRequestTypeChanged) :base(onModelCreated,onModelUpdated,onViewRequestTypeChanged)
        {
            LocalModel.Detalles = new ObservableCollection<TurnoDetalleModel>();
            LocalModel.FormasPago = new ObservableCollection<TurnoFormaPagoModel>();
            IsModeCreate = true;
            IsEnableAction = true;

            Task.WaitAll(Task.Run(async () => await LoadElements()));

            SetOnViewRequestTypeChanged(onViewRequestTypeChanged);
        }

        public TurnoDetailViewModel(TurnoResponse model, Func<TurnoResponse, Task> onModelCreated, Func<TurnoResponse, Task> onModelUpdated, Action onViewRequestTypeChanged, ViewRequestType viewRequestType) :base(model, onModelCreated, onModelUpdated,onViewRequestTypeChanged, viewRequestType)
        {
            SetOnViewRequestTypeChanged(onViewRequestTypeChanged);
        }

        public EmpresaDataStore EmpresaDataStore => App.Container.Resolve<EmpresaDataStore>();

        //public VariableSistemaDataStore VariableSistemaDataStore => DependencyService.Get<VariableSistemaDataStore>();

        public ObservableCollection<EmpresaResponse> Empresas { get; set; } = new ObservableCollection<EmpresaResponse>();

        public bool IsLoading { get; set; } = true;

        public int Dias
        {
            get => GetProperty<int>();
            set => SetProperty(value);
        }

        public bool IsModeCreate
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public bool IsEnableAction
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public bool IsEditable
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public DateTimeOffset MininumDate
        {
            get => GetProperty<DateTimeOffset>();
            set => SetProperty(value);
        }

        public EmpresaResponse Empresa
        {
            get => GetProperty<EmpresaResponse>();
            set => SetProperty(value, async () =>
            {
                if (value != null && (ViewRequestType == ViewRequestType.Create))
                {
                    if (IsLoading)
                        return;

                    LocalModel.EmpresaId = value.Id;
                    LocalModel.Empresa = value.RazonSocial;


                    await ExecutingBusy(async () =>
                    {
                        await LoadConceptos(value.Id);
                    });
                }
            });
        }

        public override ICommand SaveCommand => new RelayCommand(async () => await ExecutingBusy(OnSave, Messages.ConfirmSaveMessage, Messages.SuccessSaveMessage));

        public ICommand CancelarCommand => new RelayCommand(async () => await ExecutingBusy(CancelarTurno, Messages.ConfirmCancelMessage, Messages.SuccessCancelMessage));

        public ICommand EditarCommand => new RelayCommand(() => AllowModelEdition());

        protected override TurnoRequest CreateRequestForDataStore() => App.Container.Resolve<IMapper>().Map<TurnoRequest>(LocalModel);

        protected override Task SetModel(TurnoResponse model)
        {
            Model = model;

            App.Container.Resolve<IMapper>().Map(model, LocalModel);

            LocalModel.Detalles = new ObservableCollection<TurnoDetalleModel>();
            LocalModel.FormasPago = new ObservableCollection<TurnoFormaPagoModel>();

            foreach (var item in model.Detalles)
            {
                LocalModel.Detalles.Add(App.Container.Resolve<IMapper>().Map<TurnoDetalleResponse, TurnoDetalleModel>(item, new TurnoDetalleModel(CalcularTotal)));
            }

            foreach (var item in model.FormasPago)
            {
                LocalModel.FormasPago.Add(App.Container.Resolve<IMapper>().Map<TurnoFormaPagoResponse, TurnoFormaPagoModel>(item, new TurnoFormaPagoModel(CalcularTotal)));
            }


            Empresa = new EmpresaResponse { Id = LocalModel.EmpresaId, RazonSocial = LocalModel.Empresa };
            Empresas.Add(Empresa);

            IsEnableAction = false;

            return Task.CompletedTask;
        }

        protected override void ValidateModel()
        {
            //if (ViewRequestType == ViewRequestType.Create)
            //    if (LocalModel.Fecha.Date > DateTime.Now.Date)
            //        throw new FriendlyException(Messages.FechaNoValida);

            if (LocalModel.EmpresaId == null || LocalModel.EmpresaId == Guid.Empty)
                throw new FriendlyException(Messages.EmpresaCannotBeNull);

            if (LocalModel.ImporteFiscal <= 0)
                throw new FriendlyException(Messages.ImporteFiscalCannotBeZero);

            if (LocalModel.Detalles.Count() == 0)
                throw new FriendlyException(Messages.DetallesCannotBeEmpty);

            foreach (var detalle in LocalModel.Detalles)
            {
                if (detalle.Valor <= 0 && !detalle.PermiteNegativos)
                    throw new FriendlyException(string.Format(Messages.ConceptoValorCannotBeNegative, detalle.Concepto));

                if (detalle.Valor == 0)
                    throw new FriendlyException(string.Format(Messages.ConceptoValorCannotBeNegative, detalle.Concepto));
            }

            if (LocalModel.FormasPago.Sum(e => e.Importe) <= 0)
                throw new FriendlyException(Messages.FormasPagoImporteCannotBeZero);

        }

        private async Task LoadElements()
        {
            IEnumerable<EmpresaResponse> empresas = null;

            //var variableSistema = new VariableSistemaResponse();

            await Task.Run(async () =>
            {
                await ExecutingBusy(async () =>
                {
                    empresas = await EmpresaDataStore.GetAll();
                });
            });


            IsLoading = false;

            if (empresas.Any())
            {
                Empresas?.Clear();

                foreach (var empresa in empresas)
                    Empresas.Add(empresa);

                Empresa = Empresas.FirstOrDefault();
            }

            LocalModel.Fecha = DateTime.Now.AddDays(-3);

            //MininumDate = variableSistema != null && variableSistema.Value.IsInteger() ? DateTime.Now.AddDays(-Convert.ToInt32(variableSistema.Value)) : DateTime.Now.AddDays(-3);
        }

        private async Task LoadConceptos(Guid empresaId)
        {
            LocalModel.Detalles?.Clear();
            LocalModel.FormasPago?.Clear();

            IEnumerable<CorteConceptoResponse> cortesConceptos = null;
            IEnumerable<EmpresaFormaPagoResponse> formasPago = null;

            cortesConceptos = (await EmpresaDataStore.GetConceptos(Empresa.Id)).OrderBy(e => e.CorteOrden).OrderBy(e => e.ConceptoOrden);

            formasPago = (await EmpresaDataStore.GetFormasPago(empresaId)).Results;

            if (cortesConceptos.Any())
                cortesConceptos.Select(x => new TurnoDetalleModel(CalcularTotal)
                {
                    Concepto = x.Concepto,
                    ConceptoId = x.ConceptoId,
                    Corte = x.Corte,
                    CorteId = x.CorteId,
                    EsVenta = x.EsVenta,
                    PermiteNegativos = x.PermiteNegativos,
                    Valor = 0
                }).ForEach(x => LocalModel.Detalles.Add(x));

            if (formasPago.Any())
                formasPago.Select(x => new TurnoFormaPagoModel(CalcularTotal)
                {
                    FormaPago = x.FormaPago,
                    FormaPagoId = x.FormaPagoId,
                    EsBancaria = x.EsBancaria,
                    Importe = 0
                }).ForEach(x => LocalModel.FormasPago.Add(x));

            CalcularTotal();
        }

        private async Task CancelarTurno()
        {
            await DataStore.CancelarTurno(LocalModel.Id);

            Model.Estatus = EstatusRegistro.Cancelado;
            LocalModel.Estatus = EstatusRegistro.Cancelado;

            OnViewRequestTypeChanged();
        }

        private void SetOnViewRequestTypeChanged(Action onViewRequestChanged)
        {
            OnViewRequestTypeChanged = async () =>
            {
                onViewRequestChanged();

                IsModeCreate = ViewRequestType == ViewRequestType.Create;

                IsEnableAction = ViewRequestType == ViewRequestType.Create || ViewRequestType == ViewRequestType.Update;

                await OnModelUpdated(Model);
            };
        }


        private void CalcularTotal()
        {
            LocalModel.Venta = LocalModel.Detalles.Where(e => e.EsVenta).Sum(e => e.Valor);

            LocalModel.Saldo = LocalModel.FormasPago.Where(e => e.FormaPago == Constants.FormaPagoEfectivo).Sum(e => e.Importe) - (LocalModel.FormasPago.Where(e => e.FormaPago == Constants.ConceptoPropina || e.FormaPago == Constants.ConceptoAjustePropina).Sum(e => e.Importe));

            LocalModel.Deposito = (LocalModel.Venta - LocalModel.FormasPago.Where(e => e.EsBancaria).Sum(e => e.Importe)) - LocalModel.F7;

            
        }




        internal class Messages
        {
            internal const string ConfirmDiscardMessage = "¿Está seguro que desea descartar los cambios?";
            internal const string ConfirmSaveMessage = "¿Está seguro que desea guardar los cambios?";
            internal const string SuccessSaveMessage = "Turno guardado exitosamente";
            internal const string ConfirmCancelMessage = "¿Está seguro que desea cancelar el turno?";
            internal const string SuccessCancelMessage = "Turno cancelado exitosamente";
            internal const string EmpresaCannotBeNull = "Seleccione la empresa";
            internal const string DetallesCannotBeEmpty = "La empresa no cuenta con conceptos";
            internal const string FechaNoValida = "La fecha no puede ser mayor a la de hoy";
            internal const string VentaCannotBeZero = "La venta no puede ser cero o menor a cero.";
            internal const string SaldoCannotBeZero = "El saldo no pueder ser cero o menor a cero.";
            internal const string DepositoCannotBeZero = "El deposito no puede ser cero o menor a cero.";
            internal const string ImporteFiscalCannotBeZero = "El importe fiscal no puede ser cero o menor a cero";

            internal const string ConceptoValorCannotBeNegative = "El concepto {0} no puede tener el valor menor a cero ";
            internal const string ConceptoValorCannotBeZero = "El concepto {0} no puede tener el valor igual a cero ";
            internal const string FormasPagoImporteCannotBeZero = "Tiene que existir al menos una forma de pago con importe mayor a cero";
        }
    }
}
