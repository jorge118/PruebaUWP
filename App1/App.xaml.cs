using System;
using App1.AppConfiguration;
using App1.Services;
using Autofac;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace App1
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        public static IContainer Container { get; set; }

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
            Container = ServiceLocator.ConfigureServices();        
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzMyMTczQDMxMzkyZTMzMmUzMGQ5alRyZjJEVUFZOTR2TUd4SnhSNGl5T3VxWmlweDdRU1VTL010Z21IQlU9");
            EnviromentSetup.Configure();
            SetupManager.Apply();
            OnStart();
        }

        

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.Backbone.Empresas.EmpresaListPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        private async void OnStart()
        {
            await EnviromentSetup.ApplyConfiguration();
        }
    }
}
