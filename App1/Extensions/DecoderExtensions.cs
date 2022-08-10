using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace App1.Extensions
{
    public static class DecoderExtensions
    {
        public static string TryGetValue(this WwwFormUrlDecoder decoder, string name)
        {
            try
            {
                return decoder.GetFirstValueByName(name);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
