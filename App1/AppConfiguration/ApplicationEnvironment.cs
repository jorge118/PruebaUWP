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
    public struct ApplicationEnvironment
    {
        private const string _develop = "Develop";
        private const string _qa = "QA";
        private const string _production = "Production";

        private ApplicationEnvironment(string environment)
        {
            Environment = string.IsNullOrWhiteSpace(environment) ? throw new ArgumentNullException(nameof(environment)) : environment;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationEnvironment Develop => new ApplicationEnvironment(_develop);

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationEnvironment QA => new ApplicationEnvironment(_qa);

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationEnvironment Production => new ApplicationEnvironment(_production);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Environment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(ApplicationEnvironment lhs, ApplicationEnvironment rhs) => lhs.Environment == rhs.Environment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(ApplicationEnvironment lhs, ApplicationEnvironment rhs) => !(lhs == rhs);
    }
}
