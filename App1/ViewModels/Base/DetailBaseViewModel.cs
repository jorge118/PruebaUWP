using App1.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels.Base
{
    public abstract class DetailBaseViewModel<TEntity, TKey, TDataStore> : BaseViewModel<TDataStore> where TEntity : class where TDataStore : class, IDataStore
    {
        protected DetailBaseViewModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSaving { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsDirty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TEntity Model { get; set; }
    }
}
