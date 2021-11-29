# Zucchini Universe - Empyrion Galactic Survival

I am using a slightly modified version of Zucchini Universe's EmpyrionModdingFramework.

# Initial Setup Notes
Note: If you do this once, all mods linking to this section will work.

I found that running on different Empyrion servers, my machines and different hosting services would run the Empyrion Server executable from different locations. eg. Run from: "Empyrion - Dedicated Server" or from "Empyrion - Dedicated Server/DedicatedServer"

This unfortunately means it is difficult to locate a config file and the database folder the mod uses. To solve this, a file named "modfolder.path" must be added to location that Empyrion runs from. This file will contain a single line, that is the relative path from the run folder, to the database folder.

The Database path it is looking for is: "Empyrion - Dedicated Server\Content\Mods" 

So example modfolder.path content would be:

-If ran from: "Empyrion - Dedicated Server"

The relative path would be "Content/Mods"

-If ran from "Empyrion - Dedicated Server/DedicatedServer"

The relative path would be "../Content/Mods"

The relative path above should be the only text in the file(without quotations)

# Initial Setup Steps
In order to find the run directory, copy a mod folder and it's content to your Empyrion mods folder i.e: "Empyrion - Dedicated Server\Content\Mods"

Boot up your server and look for the log line similar to: 

"-LOG- {EmpyrionModdingFramework} [ModName] {ModName} is running from directory: L:\SteamLibrary\steamapps\common\Empyrion - Dedicated Server\DedicatedServer"

Then copy a modfolder.path file with the correct relative path to the run folder.

Reboot the server and you should see the log line:

"{ModName} will use the database path: L:\SteamLibrary\steamapps\common\Empyrion - Dedicated Server\Content\Mods\{ModName}\Database"
