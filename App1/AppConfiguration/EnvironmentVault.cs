using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App1.AppConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnvironmentVault
    {
        internal EnvironmentVault(ApplicationEnvironment environment)
        {
            Environment = environment;
        }

        /// <summary>
        /// 
        /// </summary>
        public ApplicationEnvironment Environment { get; }

        private IList<ApplicationVariable> Variables { get; } = new List<ApplicationVariable>();

        private IList<Action> Configurations { get; } = new List<Action>();

        private IList<Func<Task>> AsyncConfigurations { get; } = new List<Func<Task>>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public EnvironmentVault SetVariable<T, TProperty>(Expression<Func<T, TProperty>> variable, TProperty value)
        {
            var appVariable = new ApplicationVariable
            {
                MemberInfo = (variable.Body as MemberExpression).Member,
                Value = value
            };

            Variables.Add(appVariable);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public EnvironmentVault SetConfigAsync(Action config)
        {
            Configurations.Add(config);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public EnvironmentVault SetConfigAsync(Func<Task> config)
        {
            AsyncConfigurations.Add(config);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ApplyVariables()
        {
            foreach (var variable in Variables)
            {
                var setMethod = variable.MemberInfo.ReflectedType.GetProperty(variable.MemberInfo.Name).GetSetMethod(true);

                if (setMethod is null)
                    throw new InvalidOperationException($"Member '{variable.MemberInfo.Name}' has not a set method.");

                setMethod.Invoke(null, new object[] { variable.Value });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EnvironmentVault ApplyConfiguration()
        {
            foreach (var config in Configurations)
            {
                config();
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ApplyConfigurationAsync()
        {
            foreach (var config in AsyncConfigurations)
            {
                await config();
            }
        }
    }
}
