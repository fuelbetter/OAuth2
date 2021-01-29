using System;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.helpers
{
    public static class RandomStringGenerator
    {
        /// <summary>
        /// JB. Randomly generates a string
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GeneratedString()
        {
            string result = "";
            await Task.Run(() => {
                result = Guid.NewGuid().ToString("N");
            });
            return result;
        }

    }
}
