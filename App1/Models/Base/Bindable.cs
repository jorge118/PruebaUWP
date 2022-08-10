using App1.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Bindable : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        #region INotifyPropertyChanged Members

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            OnPropertyChanged(property.GetMemberInfo().Name);
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion

        #region Property Bindings

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>T.</returns>
        protected T GetProperty<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            if (_properties.TryGetValue(propertyName, out var property))
                return (T)property;

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="changedCallback">The changed callback.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void SetProperty<T>(T value, Action changedCallback, [CallerMemberName] string propertyName = null)
        {
            if (changedCallback == null)
                throw new ArgumentNullException(nameof(changedCallback));

            UpdateProperty(value, null, propertyName);
            changedCallback();
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="changedCallback">The changed callback.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void SetProperty<T>(T value, Action<T> changedCallback, [CallerMemberName] string propertyName = null)
        {
            if (changedCallback == null)
                throw new ArgumentNullException(nameof(changedCallback));

            UpdateProperty(value, changedCallback, propertyName);
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void SetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            UpdateProperty(value, null, propertyName);
        }

        /// <summary>
        /// Updates the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="changedCallback">The changed callback.</param>
        /// <param name="propertyName">Name of the property.</param>
        private void UpdateProperty<T>(T value, Action<T> changedCallback, string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            var oldValue = GetProperty<T>(propertyName);
            if (EqualityComparer<T>.Default.Equals(oldValue, value))
                return;

            _properties[propertyName] = value;
            OnPropertyChanged(propertyName);

            changedCallback?.Invoke(oldValue);
        }

        #endregion
    }
}
