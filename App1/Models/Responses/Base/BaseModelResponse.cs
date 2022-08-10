using App1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Responses.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseModelResponse : Bindable
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            get => GetProperty<Guid>();
            set => SetProperty(value);
        }
    }
}
