using Rocket.API;
using Rocket.API.Collections;
using Rocket.API.Serialisation;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.SalvageTime
{
    public class SalvageTimePlugin: RocketPlugin<SalvageTimeConfiguration>
    {
        public static SalvageTimePlugin Instance { get; private set; }
        protected override void Load()
        {
            base.Load();
            Instance = this;

            U.Events.OnPlayerConnected += Events_OnPlayerConnected;

            Logger.Log($"{string.Format("SalvageTime v{0}", Assembly.GetName().Version)} by Anomoly has loaded!");
            Logger.Log("Need support? Join my Discord server @ https://discord.gg/rVH9e7Kj9y");
        }

        protected override void Unload()
        {
            base.Unload();

            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;

            Logger.Log($"{string.Format("SalvageTime v{0}", Assembly.GetName().Version)} by Anomoly has unloaded!");
            Logger.Log("Need support? Join my Discord server @ https://discord.gg/rVH9e7Kj9y");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            {"command_salvagetime_invalid","Please do /salvagetime {0}!" },
            {"command_salvagetime_display","SalvageTime: {0} seconds!" },
            {"command_salvagetime_invalid_float","Invalid time! '{0}' is not a valid float!" },
            {"command_salvagetime_no_player","No player could be found by the name of '{0}'!" },
            {"command_salvagetime_insufficent_permissions","You do not have permission to set another player's salvage time!" },
            {"command_salvagetime_updated","Succesfully set {0}'s salvage time to {1} seconds!" }
        };

        private void Events_OnPlayerConnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            player.Player.interact.sendSalvageTimeOverride(GetLowestSalvageTime(player));
        }

        public float GetLowestSalvageTime(UnturnedPlayer player)
        {
            float defaultSalvageTime = Configuration.Instance.DefaultSalvageTime;
            float salvageTime = player.IsAdmin ? 1f : 8f;

            if (defaultSalvageTime < salvageTime)
            {
                salvageTime = defaultSalvageTime;
            }

            player.GetPermissions().ForEach(p =>
            {
                if (!p.Name.StartsWith("salvagetime."))
                    return;

                if (float.TryParse(p.Name.Substring("salvagetime.".Length), out float time))
                {
                    if (time < salvageTime)
                    {
                        salvageTime = time;
                    }
                }
                else
                {
                    Logger.Log("Invalid salvage time permission: " + p.Name);
                }
            });


            return salvageTime;
        }
    }
}
