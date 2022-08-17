using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discord_bot.Models
{
    public class ConfigModel
    {

        public ConfigModel(string apiToken)
        {
            ApiToken = apiToken;
        }

        public string ApiToken { get; }


    }
}
