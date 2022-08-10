using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class FriendlyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventName"></param>
        /// <param name="trackException"></param>
        public FriendlyException(string message, string eventName, bool trackException = false) : base(message)
        {
            TrackException = trackException;
            EventName = eventName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FriendlyException(string message) : base(message) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public FriendlyException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FriendlyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        /// <summary>
        /// 
        /// </summary>
        public bool TrackException { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventName { get; private set; }
    }
}
