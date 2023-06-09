using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anomoly.SalvageTime.Commands
{
    public class CommandSalvageTime : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "salvagetime";

        public string Help => "View your salvage time & admins can manage players salvage times";

        public string Syntax => "[<player> <time>]";

        public List<string> Aliases => new List<string>()
        {
            "stime"
        };

        public List<string> Permissions => new List<string>() { "salvagetime" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if(command.Length > 2)
            {
                UnturnedChat.Say(caller, SalvageTimePlugin.Instance.Translate("command_salvagetime_invalid", Syntax), true);
                return;
            }

            if(command.Length == 0)
            {

                if(caller is ConsolePlayer)
                {
                    Logger.Log($"{Name} {Syntax}");
                }
                else
                {
                    var salvageTime = SalvageTime.SalvageTimePlugin.Instance.GetLowestSalvageTime((UnturnedPlayer)caller);
                    UnturnedChat.Say(caller, SalvageTimePlugin.Instance.Translate("command_salvagetime_display", salvageTime), true);
                }
                
                return;
            }

            if(command.Length != 2)
            {
                if (caller is ConsolePlayer)
                    Logger.Log($"{Name} {Syntax}");
                else
                    UnturnedChat.Say(caller, SalvageTimePlugin.Instance.Translate("command_salvagetime_invalid", Syntax), true);
                return;
            }

            var isConsolePlayer = caller is ConsolePlayer;

            if(!isConsolePlayer && !caller.HasPermission("salvagetime.set"))
            {
                UnturnedChat.Say(caller, SalvageTimePlugin.Instance.Translate("command_salvagetime_insufficent_permissions"), true);
                return;
            }

            var targetPlayer = command.GetUnturnedPlayerParameter(0);

            if(targetPlayer == null)
            {
                var noPlayerMsg = SalvageTimePlugin.Instance.Translate("command_salvagetime_no_player", command[0]);
                if (isConsolePlayer)
                {
                    Logger.Log(noPlayerMsg);
           
                }
                else
                {
                    UnturnedChat.Say(caller, noPlayerMsg, true);
                }

                return;
            }

            float requestedTime = 8f;

            if (!float.TryParse(command[1], out requestedTime))
            {
                UnturnedChat.Say(caller, SalvageTimePlugin.Instance.Translate("command_salvagetime_invalid_float", command[1]), true);
                return;
            }
            
            targetPlayer.Player.interact.sendSalvageTimeOverride(requestedTime);

            var msg = SalvageTimePlugin.Instance.Translate("command_salvagetime_updated", targetPlayer.DisplayName, requestedTime);
            if (isConsolePlayer)
                Logger.Log(msg);
            else
                UnturnedChat.Say(caller, msg, true);

        }
    }
}
