using App1.Controls;
using App1.Helpers;
using App1.Models.Base;
using App1.Models.Common;
using App1.Models.Responses.Base;
using App1.Services;
using App1.Services.Base;
using App1.Services.Infrastructure;
using Reyma.Utils.Collections.Extensions;
using Reyma.Utils.Http.Models;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Controls.DataPager;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace App1.ViewModels.Base
{
    public abstract class BaseListViewModel<TDataStore, TResponse, TDetailPage> : BaseViewModel<TDataStore> where TDataStore : class, IDataStore, IQueryDataStore<TResponse> where TResponse : BaseModelResponse where TDetailPage : Page
    {
        public BaseListViewModel()
        {
            Items = new ObservableCollection<TResponse>();
            Dispatcher = new DispatcherManager(Constants.ViewId);
            InvokeGetModelsOnLoad();
        }

        /// <summary>
        /// 
        /// </summary>
        public DispatcherManager Dispatcher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public int PageCount { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; } = 2;

        /// <summary>
        /// 
        /// </summary>
        public bool ShowNoDataBanner
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowLoadingBanner
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowList
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRefreshing
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        public ObservableCollection<TResponse> Items { get; set; }

        public PagedCollectionView PagedCollection
        {
            get => GetProperty<PagedCollectionView>();
            set => SetProperty(value);
        }


        /// <summary>
        /// 
        /// </summary>
        public virtual ICommand RefreshCommand => new RelayCommand(async () => await ExecutingBusy(async () => await GetModelsOnLoad(1, PageSize, "")));

        /// <summary>
        /// 
        /// </summary>
        public virtual ICommand LoadMoreCommand => new RelayCommand(async () => await OnLoadMore(), () => CurrentPage < PageCount);

        public virtual ICommand AddNewCommand => new RelayCommand(async () => await OnAddNew());

        /// <summary>
        /// 
        /// </summary>
        protected Func<object, Task> OnModelCreatedDelegate => async (model) => await Dispatcher.AwaitableRunAsync(async () => { await OnModelCreated(model as TResponse); });

        /// <summary>
        /// 
        /// </summary>
        protected Func<object, Task> OnModelUpdatedDelegate => async (model) => await Dispatcher.AwaitableRunAsync( async () => {  await OnModelUpdated(model as TResponse); });


        public virtual async Task ItemTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var dataGrid = sender as SfDataGrid;

            var modelResponse = dataGrid?.SelectedItem as TResponse;

            if (modelResponse == null)
                return;

            var parameters = new ViewParameter
            {
                ModelResponse = modelResponse,
                OnModelCreated = OnModelCreatedDelegate,
                OnModelUpdated = OnModelUpdatedDelegate,
            };

            //var page = ((TDetailPage)Activator.CreateInstance(typeof(TDetailPage), model, ViewRequestType.Read)).GetType();

            Debug.WriteLine("Inicia la llamada al servicio para abrir una nueva ventana");

            await WindowManagerService.Current.TryShowAsStandaloneAsync($"{Title} Detalle", typeof(TDetailPage), parameters);
            //await WindowManagerService.Current.TryShowAsStandaloneAsync($"{Title} Detalle", (TDetailPage)Activator.CreateInstance(Type.GetType(openType)));

            Debug.WriteLine("Termina la llamada al servicio para abrir una nueva venta");
        }


        public virtual async Task OnAddNew()
        {
            var parameters = new ViewParameter
            {
                OnModelCreated = OnModelCreatedDelegate,
                OnModelUpdated = OnModelUpdatedDelegate,
            };

            await WindowManagerService.Current.TryShowAsStandaloneAsync("Nuevo Registro", typeof(TDetailPage), parameters);
        }
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual Task OnModelCreated(TResponse model)
        {
            Items.Insert(0, model);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual Task OnModelUpdated(TResponse model)
        {
            var index = Items.FindIndex(x => x.Id == model.Id);
            Items.RemoveAt(index);
            Items.Insert(index, model);

            return Task.CompletedTask;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        protected virtual async Task GetModelsOnLoad(long pageNumber, int pageSize, string filters = "")
        {
            ShowList = false;
            ShowLoadingBanner = true;
            ShowNoDataBanner = false;

            var items = await GetModels(pageNumber, pageSize, filters);
            Items.Clear();

            foreach (var item in items)
                Items.Add(item);

            IsRefreshing = false;
        }

        protected virtual async Task OnLoadMore()
        {
            await ExecutingBusy(async () =>
            {
                var items = await GetModels(CurrentPage + 1, PageSize, "");

                foreach (var item in items)
                    Items.Add(item);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        protected virtual async Task<IEnumerable<TResponse>> GetModels(long pageNumber, int pageSize, string filters = "")
        {
            var response = await GetPagedModels(pageNumber, pageSize, filters);
            CurrentPage = Convert.ToInt32(response.CurrentPage);
            PageCount = Convert.ToInt32(response.PageCount);

            ShowLoadingBanner = false;
            ShowList = response.RowCount > 0;
            ShowNoDataBanner = !ShowList;

            return response.Results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        protected virtual async Task<PagedResults<TResponse>> GetPagedModels(long pageNumber, int pageSize, string filters = "") => await DataStore.Get(pageNumber, pageSize, filters);

        /// <summary>
        /// Esté método sirve para invalidar la llamada de la carga de la lista en el constructor sin tener que sobrescribir el método 'GetModelsOnLoad'.
        /// </summary>
        protected virtual async void InvokeGetModelsOnLoad()
        {
            await ExecutingBusy(async () => await GetModelsOnLoad(1, PageSize));
        }


    }
}
