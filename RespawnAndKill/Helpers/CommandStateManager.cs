namespace RespawnAndKill.Helpers
{
    public static class CommandStateManager
    {
        public static bool IsRespawnTemporarilyDisabled { get; set; } = false; 
        public static bool IsKillTemporarilyDisabled { get; set; } = false;
        
        public static void Reset()
        {
            IsRespawnTemporarilyDisabled = false;
            IsKillTemporarilyDisabled = false;
        }
    }
}