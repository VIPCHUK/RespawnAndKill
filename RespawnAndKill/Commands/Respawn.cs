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
        public string Description => "Respawns a player as a Class-D or Scientist with a cooldown.";

        private static readonly Dictionary<string, DateTime> Cooldowns = new Dictionary<string, DateTime>();
        private static readonly Random Rng = new Random();
        private readonly Config _config = RespawnAndKillPlugin.Instance.Config;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!_config.IsRespawnCommandEnabled || Helpers.CommandStateManager.IsRespawnTemporarilyDisabled)
            {
                response = "The .respawn (.rsp) command is currently disabled.";
                return false;
            }

            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "This command can only be used by a player.";
                return false;
            }

            if (!Round.IsStarted)
            {
                response = "You cannot use this command until the round has started.";
                return false;
            }

            if (_config.RespawnTimeLimit > 0 && Round.ElapsedTime.TotalSeconds > _config.RespawnTimeLimit)
            {
                response = $"You can no longer use this command. It is only available during the first {_config.RespawnTimeLimit} seconds of the round.";
                return false;
            }

            Player player = Player.Get(playerSender);

            if (player.Role.Type != RoleTypeId.Spectator)
            {
                response = "You must be dead to use this command.";
                return false;
            }

            if (Warhead.IsDetonated)
            {
                response = "You cannot use this command because the nuke has detonated.";
                return false;
            }

            if (Cooldowns.TryGetValue(player.UserId, out DateTime lastUse))
            {
                TimeSpan elapsed = DateTime.Now - lastUse;
                float cooldown = RespawnAndKillPlugin.Instance.Config.RespawnCooldown;
                if (elapsed.TotalSeconds < cooldown)
                {
                    response = $"Please wait. You can use this command in {Math.Ceiling(cooldown - elapsed.TotalSeconds)} seconds.";
                    return false;
                }
            }

            int dClassChance = RespawnAndKillPlugin.Instance.Config.DClassSpawnChance;
            int roll = Rng.Next(1, 101);

            RoleTypeId roleToSpawn = roll <= dClassChance ? RoleTypeId.ClassD : RoleTypeId.Scientist;

            player.Role.Set(roleToSpawn, RoleSpawnFlags.All);

            CheckForDanger(player);

            Cooldowns[player.UserId] = DateTime.Now;
            response = $"You have been respawned as {roleToSpawn}.";
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