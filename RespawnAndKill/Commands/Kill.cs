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
        public string Description => "Позволяет игроку совершить самоубийство с указанием причины.";

        private readonly Config _config = RespawnAndKillPlugin.Instance.Config;
        private static readonly Random Rng = new Random();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!_config.IsKillCommandEnabled || Helpers.CommandStateManager.IsKillTemporarilyDisabled)
            {
                response = "Команда .kill в данный момент отключена.";
                return false;
            }
            
            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "Эта команда может быть использована только игроком.";
                return false;
            }

            Player player = Player.Get(playerSender);

            if (!player.IsAlive)
            {
                response = "Вы не можете использовать эту команду, так как вы уже мертвы.";
                return false;
            }

            string reason;
            if (arguments.Count == 0)
            {
                var reasons = RespawnAndKillPlugin.Instance.Config.RandomKillReasons;
                if (reasons == null || reasons.Count == 0)
                {
                    reason = "Решил покинуть этот мир.";
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
                    response = $"Причина смерти слишком длинная. Максимальное количество символов: {charLimit}.";
                    return false;
                }
            }

            player.Kill(reason);
            response = "Вы успешно совершили самоубийство.";
            return true;
        }
    }
}