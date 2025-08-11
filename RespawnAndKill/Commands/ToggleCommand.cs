using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using RespawnAndKill.Helpers;

namespace RespawnAndKill.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ToggleCommand : ICommand
    {
        public string Command => "togglecmd";
        public string[] Aliases => new[] { "tcmd" };
        public string Description => "Temporarily enables or disables the .respawn or .kill commands for the current round.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("respawnkill.toggle"))
            {
                response = "You do not have permission to use this command. Required permission: respawnkill.toggle";
                return false;
            }

            if (arguments.Count != 2)
            {
                response = "Usage: togglecmd <respawn|kill> <on|off>";
                return false;
            }

            string commandToToggle = arguments.At(0).ToLower();
            string state = arguments.At(1).ToLower();
            bool disable;

            switch (state)
            {
                case "on":
                    disable = false;
                    break;
                case "off":
                    disable = true;
                    break;
                default:
                    response = "Invalid state. Use 'on' or 'off'.";
                    return false;
            }

            switch (commandToToggle)
            {
                case "respawn":
                    CommandStateManager.IsRespawnTemporarilyDisabled = disable;
                    response = $".respawn command has been temporarily {(disable ? "disabled" : "enabled")} for this round.";
                    return true;
                case "kill":
                    CommandStateManager.IsKillTemporarilyDisabled = disable;
                    response = $".kill command has been temporarily {(disable ? "disabled" : "enabled")} for this round.";
                    return true;
                default:
                    response = "Invalid command. Use 'respawn' or 'kill'.";
                    return false;
            }
        }
    }
}