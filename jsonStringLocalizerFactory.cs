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
        public IStringLocalizer Create(Type resourceSource)
        {
            return new jsonStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new jsonStringLocalizer();
        }
    }
}
