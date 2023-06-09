using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.SalvageTime
{
    public class SalvageTimeConfiguration: IRocketPluginConfiguration
    {
        public float DefaultSalvageTime { get; set; }

        public void LoadDefaults()
        {
            DefaultSalvageTime = 8f;
        }
    }
}
