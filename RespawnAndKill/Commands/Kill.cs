using System;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;

namespace RespawnAndKill.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class KillCommand : ICommand
    {
        public string Command => "kill";
        public string[] Aliases => new string[0];
        public string Description => "Allows a player to commit suicide with a specified reason.";

        private readonly Config _config = RespawnAndKillPlugin.Instance.Config;
        private static readonly Random Rng = new Random();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!_config.IsKillCommandEnabled || Helpers.CommandStateManager.IsKillTemporarilyDisabled)
            {
                response = "The .kill command is currently disabled.";
                return false;
            }
            
            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "This command can only be used by a player.";
                return false;
            }

            Player player = Player.Get(playerSender);

            if (!player.IsAlive)
            {
                response = "You must be alive to use this command.";
                return false;
            }

            string reason;
            if (arguments.Count == 0)
            {
                var reasons = RespawnAndKillPlugin.Instance.Config.RandomKillReasons;
                if (reasons == null || reasons.Count == 0)
                {
                    reason = "Unknown Reason.";
                }
                else
                {
                    reason = reasons[Rng.Next(reasons.Count)];
                }
            }
            else
            {
                reason = string.Join(" ", arguments);
                int charLimit = RespawnAndKillPlugin.Instance.Config.KillReasonCharLimit;
                
                if (reason.Length > charLimit)
                {
                    response = $"Cause of death is too long. Maximum characters: {charLimit}.";
                    return false;
                }
            }

            player.Kill(reason);
            response = "You have successfully committed suicide.";
            return true;
        }
    }
}