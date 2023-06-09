# Anomoly.SalvageTime
A RocketMod plugin to set the salvage time of a player via command or permission.

## Commands
- `/salvagetime [<player> <time>]` - Show the current salvage time / set a player's salvage time
  - Aliases: `/stime`
  - Permission: `salvagetime`,`salvagetime.set`

## Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<SalvageTimeConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <DefaultSalvageTime>8</DefaultSalvageTime>
</SalvageTimeConfiguration>
```

## Translations
```
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="command_salvagetime_invalid" Value="Please do /salvagetime {0}!" />
  <Translation Id="command_salvagetime_display" Value="SalvageTime: {0} seconds!" />
  <Translation Id="command_salvagetime_invalid_float" Value="Invalid time! '{0}' is not a valid float!" />
  <Translation Id="command_salvagetime_no_player" Value="No player could be found by the name of '{0}'!" />
  <Translation Id="command_salvagetime_insufficent_permissions" Value="You do not have permission to set another player's salvage time!" />
  <Translation Id="command_salvagetime_updated" Value="Succesfully set {0}'s salvage time to {1}!" />
</Translations>```
