using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonCultuerLocalizerLibrary
{
    public class jsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache cache;

        public jsonStringLocalizerFactory(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new jsonStringLocalizer(cache);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new jsonStringLocalizer(cache);
        }
    }
}
