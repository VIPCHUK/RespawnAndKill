using System;
using Exiled.API.Features;
using RespawnAndKill.Helpers;

namespace RespawnAndKill
{
    public class RespawnAndKillPlugin : Plugin<Config>
    {
        public override string Name => "RespawnAndKill";
        public override string Author => "honvert";
        public override Version Version => new Version(1, 2, 0);
        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        public static RespawnAndKillPlugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Exiled.Events.Handlers.Server.RestartingRound += OnRoundRestart;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= OnRoundRestart;
        }

        private void OnRoundRestart()
        {
            CommandStateManager.Reset();
        }
    }
}
