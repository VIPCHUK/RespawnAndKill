using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using RemoteAdmin;

namespace RespawnAndKill.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class RespawnCommand : ICommand
    {
        public string Command => "respawn";
        public string[] Aliases => new[] { "rsp" };
        public string Description => "Возрождает игрока в роли Класса-D или Ученого с перезарядкой.";

        private static readonly Dictionary<string, DateTime> Cooldowns = new Dictionary<string, DateTime>();
        private static readonly Random Rng = new Random();
        private readonly Config _config = RespawnAndKillPlugin.Instance.Config;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!_config.IsKillCommandEnabled || Helpers.CommandStateManager.IsRespawnTemporarilyDisabled)
            {
                response = "Команда .respawn (.rsp) в данный момент отключена.";
                return false;
            }
            
            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "Эта команда может быть использована только игроком.";
                return false;
            }
            
            if (!Round.IsStarted)
            {
                response = "Вы не можете использовать эту команду, пока раунд не начался.";
                return false;
            }
            
            if (_config.RespawnTimeLimit > 0 && Round.ElapsedTime.TotalSeconds > _config.RespawnTimeLimit)
            {
                response = $"Вы больше не можете использовать эту команду. Она доступна только в течение первых {_config.RespawnTimeLimit} секунд раунда.";
                return false;
            }
            
            Player player = Player.Get(playerSender);
            
            if (player.Role.Type != RoleTypeId.Spectator)
            {
                response = "Вы должны быть мертвы, чтобы использовать эту команду.";
                return false;
            }

            if (Cooldowns.TryGetValue(player.UserId, out DateTime lastUse))
            {
                TimeSpan elapsed = DateTime.Now - lastUse;
                float cooldown = RespawnAndKillPlugin.Instance.Config.RespawnCooldown;
                if (elapsed.TotalSeconds < cooldown)
                {
                    response = $"Пожалуйста, подождите. Вы сможете использовать эту команду через {Math.Ceiling(cooldown - elapsed.TotalSeconds)} секунд.";
                    return false;
                }
            }
            
            int dClassChance = RespawnAndKillPlugin.Instance.Config.DClassSpawnChance;
            int roll = Rng.Next(1, 101);

            RoleTypeId roleToSpawn = roll <= dClassChance ? RoleTypeId.ClassD : RoleTypeId.Scientist;
            
            player.Role.Set(roleToSpawn, RoleSpawnFlags.All);
            
            CheckForDanger(player);
            
            Cooldowns[player.UserId] = DateTime.Now;
            response = $"Вы были возрождены как {roleToSpawn}.";
            return true;
        }
        private void CheckForDanger(Player player)
        {
            if (!_config.IsDangerSpawnEnabled)
                return;

            Room spawnRoom = player.CurrentRoom;
            if (spawnRoom == null) 
                return;

            foreach (Player p in Player.List)
            {
                if (p.IsScp && p.CurrentRoom == spawnRoom)
                {
                    player.EnableEffect(EffectType.MovementBoost, _config.DangerSpawnBoostDuration);
                    
                    if (player.TryGetEffect(EffectType.MovementBoost, out var effect))
                    {
                        effect.Intensity = _config.DangerSpawnBoostIntensity;
                    }

                    player.ShowHint(_config.DangerSpawnHint, 10f);

                    break;
                }
            }
        }
    }
}
