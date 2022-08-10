using App1.Models.Common;
using App1.Models.Responses.Backbone.Empresas;
using App1.Services;
using App1.ViewModels.Backbone.Empresas;
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

namespace App1.Views.Backbone.Empresas
{
    public sealed partial class EmpresaDetailPage : Page
    {
        public EmpresaDetailViewModel ViewModel { get; set; }

        public EmpresaDetailPage()
        {
            this.InitializeComponent();
            DataContext = ViewModel = new EmpresaDetailViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.OnNavigatedTo(e);
        }
    }
}
