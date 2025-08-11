### RespawnAndKill
Adds the ability to respawn as a scientist or D-class in the first X seconds.

## Installation
1. **Prerequisites**: Ensure you have EXILED 9.6.0+ installed on your SCP:SL server
2. **Download**: Get the latest release from the releases page
3. **Install**: Place `RespawnAndKill.dll` in your `EXILED/Plugins` folder
4. **Configure**: Edit the generated config file to your preferences
5. **Restart**: Restart your server to load the plugin
   
## ⚙️ Configuration

```yaml
# RespawnAndKill Configuration
respawnandkill:
  is_enabled: true
  debug: false

  # Enable the .respawn command?
  IsRespawnCommandEnabled: true

  # Enable the .kill command?
  IsKillCommandEnabled: true

  # RESPAWN CONFIG
  # Maximum time (in seconds) from the start of the round during which the .respawn command can be used. Set to 0 to disable the limit.
  RespawnTimeLimit: 240

  # RSP: Enable the 'Dangerous Respawn' feature? If true, the player will receive a speed boost if respawning near an SCP.
  IsDangerSpawnEnabled: true

  # RSP: Hint text that the player will see during a dangerous respawn
  DangerSpawnHint: "<br><br><size=26>You have respawned in a <color=red>dangerous</color> area. Run!"

  # RSP: Duration (in seconds) of the speed boost effect during a dangerous respawn
  DangerSpawnBoostDuration: 10

  # RSP: Intensity of the speed boost effect (in percentage). 20 = 20% speed boost.
  DangerSpawnBoostIntensity: 60

  # RSP: Cooldown time (in seconds) before the .respawn command can be used again
  RespawnCooldown: 30

  # RSP: Duration (in seconds) of the speed boost effect during a dangerous respawn
  DangerSpawnBoostDuration: 10

  # RSP: Chance (in percentage) to respawn as Class-D
  DClassSpawnChance: 70

  # RSP: Chance (in percentage) to respawn as a Scientist. Note: the sum of chances with DClass does not necessarily have to be 100
  ScientistSpawnChance: 30

  # RSP: Duration (in seconds) of the speed boost effect during a dangerous respawn
  DangerSpawnBoostDuration: 10

  # KILL CONFIG
  # KILL: Maximum number of characters for a custom death reason
  KillReasonCharLimit: 100

  # KILL: List of random death reasons if no reason is specified
  RandomKillReasons: ...
```
## Commands

### **Player Commands**
- `.rsp` / `.respawn` - Respawns a player as a Class-D or Scientist with a cooldown.
- `.kill <message>` / `.kill` - Allows a player to commit suicide with a specified reason.

### **Admin Commands** (Remote Admin)
- `togglecmd` / `tcmd` - Toggle .kill and .rsp on/off

## Compatibility
- **EXILED**: 9.6.0+
- **SCP:SL**: Compatible with latest versions
- **.NET Framework**: 4.8
