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
        public string Description => "Временно включает или отключает команды .respawn или .kill на текущий раунд.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("respawnkill.toggle"))
            {
                response = "У вас нет прав на использование этой команды. Требуется право: respawnkill.toggle";
                return false;
            }

            if (arguments.Count != 2)
            {
                response = "Использование: togglecmd <respawn|kill> <on|off>";
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
                    response = "Неверное состояние. Используйте 'on' или 'off'.";
                    return false;
            }

            switch (commandToToggle)
            {
                case "respawn":
                    CommandStateManager.IsRespawnTemporarilyDisabled = disable;
                    response = $"Команда .respawn была временно {(disable ? "отключена" : "включена")} на этот раунд.";
                    return true;
                case "kill":
                    CommandStateManager.IsKillTemporarilyDisabled = disable;
                    response = $"Команда .kill была временно {(disable ? "отключена" : "включена")} на этот раунд.";
                    return true;
                default:
                    response = "Неверная команда. Используйте 'respawn' или 'kill'.";
                    return false;
            }
        }
    }
}