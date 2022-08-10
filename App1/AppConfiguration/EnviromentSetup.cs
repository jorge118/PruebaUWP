using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.AppConfiguration
{
    internal static class EnviromentSetup
    {
        internal static void Configure()
        {
            EnvironmentManager.Init();

            EnvironmentManager.Instance
                .CreateEnvironmentVault(ApplicationEnvironment.Develop)
                .SetVariable<SecurityConstants, string>(c => SecurityConstants.Authority, "https://www.benol.com.mx:5000/auth/dev")
                .SetVariable<Constants, string>(c => Constants.UrlCheckUpdates, "https://www.benol.com.mx:5000/reyma/appmobileinfo/dev")
                .SetVariable<Constants, string>(c => Constants.ApiUrlBase, "https://www.benol.com.mx:5000/finanzas/indicadores/dev");

            EnvironmentManager.Instance
                .CreateEnvironmentVault(ApplicationEnvironment.QA)
                .SetVariable<SecurityConstants, string>(c => SecurityConstants.Authority, "https://www.benol.com.mx:5000/auth/dev")
                .SetVariable<Constants, string>(c => Constants.UrlCheckUpdates, "https://www.benol.com.mx:5000/reyma/appmobileinfo/dev")
                .SetVariable<Constants, string>(c => Constants.ApiUrlBase, "https://www.benol.com.mx:5000/finanzas/indicadores/dev");

            EnvironmentManager.Instance
                .CreateEnvironmentVault(ApplicationEnvironment.Production)
                .SetVariable<SecurityConstants, string>(c => SecurityConstants.Authority, "https://www.benol.com.mx:5000/auth")
                .SetVariable<Constants, string>(c => Constants.UrlCheckUpdates, "https://www.benol.com.mx:5000/reyma/appmobileinfo")
                .SetVariable<Constants, string>(c => Constants.ApiUrlBase, "https://www.benol.com.mx:5000/finanzas/indicadores");

            EnvironmentManager.Instance
                .SetEnvironment(Constants.Environment)
                .ApplyVariables();
        }

        internal static async Task ApplyConfiguration()
        {
            await EnvironmentManager.Instance
                    .SetEnvironment(Constants.Environment)
                    .ApplyConfiguration()
                    .ApplyConfigurationAsync();
        }
    }
}

