using App1.Models.Base;
using App1.Models.Common;
using App1.Models.Responses.Finanzas.Turnos;
using App1.ViewModels.Finanzas.Turnos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.Views.Finanzas.Turnos
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TurnoDetailPage : Page
    {
        private AppBarButton SaveItem;
        private AppBarButton EditItem;
        private AppBarButton CancelItem;

        public TurnoDetailViewModel ViewModel { get; set; }

        public TurnoDetailPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel;
            //DataContext = ViewModel = new TurnoDetailViewModel(CustomizeToolBar);
            //BuildToolbarItems();
        }

        //public TurnoDetailPage(TurnoResponse model, ViewRequestType viewRequestType = ViewRequestType.Update)
        //{
        //    this.InitializeComponent();
        //    DataContext = ViewModel = new TurnoDetailViewModel(model, CustomizeToolBar, viewRequestType);
        //    BuildToolbarItems();
        //}

        public void Init(Func<TurnoResponse, Task> onModelCreated, Func<TurnoResponse, Task> onModelUpdated)
        {
            DataContext = ViewModel = new TurnoDetailViewModel(onModelCreated, onModelUpdated, CustomizeToolBar);
            BuildToolbarItems();
        }

        public void Init(TurnoResponse model, Func<TurnoResponse, Task> onModelCreated, Func<TurnoResponse, Task> onModelUpdated, ViewRequestType viewRequestType = ViewRequestType.Update)
        {
            DataContext = ViewModel = new TurnoDetailViewModel(model,onModelCreated, onModelUpdated, CustomizeToolBar, viewRequestType);
            BuildToolbarItems();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = (ViewParameter)e.Parameter;

            if (parameter?.ModelResponse != null)
                Init((TurnoResponse)parameter.ModelResponse, parameter?.OnModelCreated, parameter?.OnModelUpdated, ViewRequestType.Read);
            else
                Init(parameter?.OnModelCreated, parameter?.OnModelUpdated);

            ViewModel.OnNavigatedTo(e);
            CustomizeToolBar();
        }


        private void BuildToolbarItems()
        {
            SaveItem = new AppBarButton
            {
                Label = "Guardar",
                Icon =  new SymbolIcon(Symbol.Save),
                Command = ViewModel?.SaveCommand
            };

            EditItem = new AppBarButton
            {
                Label = "Editar",
                Icon = new SymbolIcon(Symbol.Edit),
                Command = ViewModel?.EditarCommand
            };

            CancelItem = new AppBarButton
            {
                Label = "Cancelar",
                Icon = new SymbolIcon(Symbol.Cancel),
                Command = ViewModel?.CancelarCommand
            };
        }

        private void CustomizeToolBar()
        {
            CommandBar bottomAppBar = this.MainCommandBar as CommandBar;

            bottomAppBar.PrimaryCommands?.Clear();

            if (ViewModel?.ViewRequestType == ViewRequestType.Create || ViewModel?.ViewRequestType == ViewRequestType.Update)
                bottomAppBar.PrimaryCommands.Add(SaveItem);

            if (ViewModel?.ViewRequestType == ViewRequestType.Read && ViewModel.LocalModel.Estatus == EstatusRegistro.Activo)
            {
                bottomAppBar.PrimaryCommands.Add(EditItem);
                bottomAppBar.PrimaryCommands.Add(CancelItem);
            }
                
        }
    }
}
