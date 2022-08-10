using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.AppConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnvironmentManager
    {
        private EnvironmentManager() { }

        /// <summary>
        /// 
        /// </summary>
        public static EnvironmentManager Instance { get; private set; }

        private IList<EnvironmentVault> Vaults { get; } = new List<EnvironmentVault>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static EnvironmentManager Init()
        {
            Instance = new EnvironmentManager();
            return Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public EnvironmentVault CreateEnvironmentVault(ApplicationEnvironment environment)
        {
            var vault = new EnvironmentVault(environment);
            Vaults.Add(vault);

            return vault;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public EnvironmentVault SetEnvironment(ApplicationEnvironment environment) => Vaults.FirstOrDefault(x => x.Environment == environment);
    }
}
