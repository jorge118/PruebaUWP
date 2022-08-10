using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Exceptions
{
    [Serializable]
    public class AuthServiceInitializationException : Exception
    {
        public AuthServiceInitializationException() { }
        public AuthServiceInitializationException(string message) : base(message) { }
        public AuthServiceInitializationException(string message, Exception inner) : base(message, inner) { }
        protected AuthServiceInitializationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
