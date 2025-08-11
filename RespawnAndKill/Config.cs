using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace RespawnAndKill
{
    public class Config : IConfig
    {
        // General
        [Description("Enable or disable the plugin.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enable or disable debug mode (detailed information in the console).")]
        public bool Debug { get; set; } = false;

        [Description("Enable the .respawn command?")]
        public bool IsRespawnCommandEnabled { get; set; } = true;

        [Description("Enable the .kill command?")]
        public bool IsKillCommandEnabled { get; set; } = true;

        // RSP
        [Description("Maximum time (in seconds) from the start of the round during which the .respawn command can be used. Set to 0 to disable the limit.")]
        public float RespawnTimeLimit { get; set; } = 240f;

        [Description("RSP: Enable the 'Dangerous Respawn' feature? If true, the player will receive a speed boost if respawning near an SCP.")]
        public bool IsDangerSpawnEnabled { get; set; } = true;

        [Description("RSP: Hint text that the player will see during a dangerous respawn.")]
        public string DangerSpawnHint { get; set; } = "<br><br><size=26>You have respawned in a <color=red>dangerous</color> area. Run!";

        [Description("RSP: Duration (in seconds) of the speed boost effect during a dangerous respawn.")]
        public float DangerSpawnBoostDuration { get; set; } = 10f;

        [Description("RSP: Intensity of the speed boost effect (in percentage). 20 = 20% speed boost.")]
        public byte DangerSpawnBoostIntensity { get; set; } = 60;

        [Description("RSP: Cooldown time (in seconds) before the .respawn command can be used again.")]
        public float RespawnCooldown { get; set; } = 30f;

        [Description("RSP: Chance (in percentage) to respawn as Class-D.")]
        public int DClassSpawnChance { get; set; } = 70;

        [Description("RSP: Chance (in percentage) to respawn as a Scientist. Note: the sum of chances with DClass does not necessarily have to be 100.")]
        public int ScientistSpawnChance { get; set; } = 30;

        // KILL
        [Description("KILL: Maximum number of characters for a custom death reason.")]
        public int KillReasonCharLimit { get; set; } = 100;

        [Description("KILL: List of random death reasons if no reason is specified.")]
        public List<string> RandomKillReasons { get; set; } = new List<string>
        {
            "Decided to test if fall damage exists.",
            "Got lost in thoughts.",
            "Saw a scary dream.",
            "Slipped on a banana peel.",
            "Tried to hug SCP-173.",
            "SGkgOykKU2b252ZXJ0",
            "Tried to pet SCP-049.",
            "Thought it was a good idea to play with SCP-096.",
            "Wanted to see if SCP-106's pocket dimension is cozy.",
            "Decided to take a nap in the middle of the warhead detonation.",
            "Tried to play hide and seek with SCP-939.",
            "Wanted to see if SCP-079 would let them in.",
            "Thought it was a good idea to play tag with SCP-173.",
            "Tried to pet SCP-049's plague doctor mask.",
            "Wanted to see if SCP-079 would let them in for a chat.",
        };
    }
}