using System;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Models.Base;
using App1.Models.Common;
using App1.Models.Responses.Base;
using App1.Services;
using App1.Services.Base;
using App1.Services.Infrastructure;
using Autofac;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;

namespace App1.ViewModels.Base
{
    public abstract class BaseDetailViewModel<TModelResponse, TLocalModel, TModelRequest, TKey, TDataStore> : DetailBaseViewModel<TModelResponse, TKey, TDataStore> where TModelResponse : BaseModelResponse where TModelRequest : class where TDataStore : class, IDataStore, IWriteDataStore<TModelRequest, TModelResponse>, IQueryDataStore<TModelResponse>
    {
        public BaseDetailViewModel()
        {
        }

        public BaseDetailViewModel(Action onViewRequestTypeChanged)
        {
            OnViewRequestTypeChanged = onViewRequestTypeChanged ?? throw new ArgumentNullException(nameof(onViewRequestTypeChanged));

            ViewRequestType = ViewRequestType.Create;
        }


        public BaseDetailViewModel(Action onViewRequestTypeChanged, ViewRequestType viewRequestType)
        {
            OnViewRequestTypeChanged = onViewRequestTypeChanged ?? throw new ArgumentNullException(nameof(onViewRequestTypeChanged));
           
        }


        /// <summary>
        /// 
        /// </summary>
        public TLocalModel LocalModel { get; set; } = Activator.CreateInstance<TLocalModel>();

        /// <summary>
        /// 
        /// </summary>
        public ViewRequestType ViewRequestType
        {
            get => GetProperty<ViewRequestType>();
            set => SetProperty(value, OnViewRequestTypeChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        public Action OnViewRequestTypeChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<TModelResponse, Task> OnModelCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<TModelResponse, Task> OnModelUpdated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public abstract ICommand SaveCommand { get; }

        private ViewLifetimeControl ViewLifetimeControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Func<Task<bool>> OnBackButton => async () =>
        {
            if (ViewRequestType != ViewRequestType.Create && ViewRequestType != ViewRequestType.Update)
                return false;

            // TODO: Implement for UWP
            //if (await Dialog.ShowMessage("¿Desea descartar los cambios?", "Confirmar", "Si", "No"))
            //    return false;

            return true;
        };

        public virtual async void OnClosePageRequested()
        {
            if (await OnBackButton?.Invoke())
                return;

            NavigationService.GoBack();
        }

        public void ChangeViewRequest(ViewRequestType viewRequest) => ViewRequestType = viewRequest;

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = (ViewParameter)e.Parameter;

            Initialize(parameter.ViewLifetimeControl);

            if (parameter.ModelResponse != null)
            {
                GetAndSetModelData((TModelResponse)parameter.ModelResponse, ViewRequestType.Read);
            }
        }

        public void Initialize(ViewLifetimeControl viewLifetimeControl)
        {
            ViewLifetimeControl = viewLifetimeControl;
            ViewLifetimeControl.Released += OnViewLifetimeControlReleased;
        }

        private async void OnViewLifetimeControlReleased(object sender, EventArgs e)
        {
            ViewLifetimeControl.Released -= OnViewLifetimeControlReleased;
            await WindowManagerService.Current.MainDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                WindowManagerService.Current.SecondaryViews.Remove(ViewLifetimeControl);
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual async Task<TModelResponse> GetModel(TModelResponse model) => await ExecutingBusy(model.Id, DataStore.GetById);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected abstract Task SetModel(TModelResponse model);

        /// <summary>
        /// 
        /// </summary>
        protected abstract void ValidateModel();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract TModelRequest CreateRequestForDataStore();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task<TModelResponse> CreateModelWithDataStore(TModelRequest request) => await DataStore.Create(request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual async Task<TModelResponse> UpdateModelWithDataStore(TModelRequest request) => await DataStore.Update(request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="viewRequestType"></param>
        protected virtual async void GetAndSetModelData(TModelResponse model, ViewRequestType viewRequestType)
        {
            Model = await GetModel(model);
            await SetModel(Model);

            ViewRequestType = viewRequestType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual async Task OnSave()
        {
            ValidateModel();
            var request = CreateRequestForDataStore();

            if (ViewRequestType == ViewRequestType.Create)
            {
                Model = await CreateModelWithDataStore(request);
                //await OnModelCreated(Model);
                await SetModel(Model);
                ViewRequestType = ViewRequestType.Read;
            }

            if (ViewRequestType == ViewRequestType.Update)
            {
                Model = await UpdateModelWithDataStore(request);
                //await OnModelUpdated(Model);
                await SetModel(Model);
                ViewRequestType = ViewRequestType.Read;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Task AllowModelEdition()
        {
            ViewRequestType = ViewRequestType.Update;
            OnViewRequestTypeChanged();
            return Task.CompletedTask;
        }


        private class Messages
        {
            internal const string DefaultConfirmMessage = "";
            internal const string DefaultSuccessMessage = "";
            internal const string PleaseWaitMessage = "Por favor espere...";
            internal const string FailsWhileInvokingMessage = "Fails while invoking the method '{0}'.";
            internal const string FailsWhileCallingApiMessage = "Has failed API Call. Fails while invoking the method '{0}'.";
            internal const string UnexpectedError = "Unexpected Error";
            internal const string SuccessTitle = "Éxito";
            internal const string ErrorTitle = "Error";
            internal const string AcceptButtonText = "Ok";
            internal const string YesButtonText = "Sí";
            internal const string NoButtonText = "No";
            internal const string ConfirmTitle = "Confirmar";
            internal const string CallToWebApi = "Call to Web API";
            internal const string InvalidCredentialsMessage = "Usuario o contraseña incorrecto. Verifique su información.";
        }

    }


}
