using App1.AppConfiguration;
using App1.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App1
{
    public class Constants
    {

#if DEBUG
        public static ApplicationEnvironment Environment => ApplicationEnvironment.Develop;
#elif QA
        public static ApplicationEnvironment Environment => ApplicationEnvironment.QA;
#else
       public static ApplicationEnvironment Environment => ApplicationEnvironment.Production;
#endif

        public static string ApplicationName => "App Finanzas Restaurantes";

        public static string ApplicationId => "fnz.indicadores";

        public static string AppVersion => $"v{Version}";

        public static string Version => "1.0.0";

        public static string ImageSource => ImageHelper.ImageFromAssetsFile("Logo.png");

        public static Color PrimaryColor => Color.Blue;

        public static string PrimaryColorHex => "#203674";

        public static string PoweredBy => "Powered by Reyma Development Team";

        public static string ApiUrlBase { get; private set; }

        public static int RefreshingTokenTimerDevelop => 60;

        public static int RefreshBeforeTokenExpires => 15;

        public static int SecondsToKill => 60;

        public static string UrlCheckUpdates { get; private set; }

        public static string FormaPagoEfectivo = "EFECTIVO";

        public static string ConceptoPropina = "PROPINA";

        public static string ConceptoAjustePropina = "AJUSTE PROPINA";

        public static string TurnoDiasCrearPermitidos = "DIASPERCREATURN";

        public const decimal IVA = 0.16M;
    }
}
