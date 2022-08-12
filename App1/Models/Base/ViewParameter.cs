using App1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models.Base
{
    public class ViewParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public ViewLifetimeControl ViewLifetimeControl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object ModelResponse { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<object, Task> OnModelCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<object, Task> OnModelUpdated { get; set; }

    }
}
