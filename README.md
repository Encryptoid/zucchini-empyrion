# Zucchini Universe - Empyrion Galactic Survival

I am using a slightly modified version of Zucchini Universe's EmpyrionModdingFramework.

# Initial Setup
I found that running on different Empyrion servers, my machines and different hosting services would run the Empyrion Server executable from different locations. eg. Run from: "Empyrion - Dedicated Server" or from "Empyrion - Dedicated Server/DedicatedServer"

This unfortunately means it is difficult to locate a config file and the database folder the mod uses. To solve this, a file named "modfolder.path" must be added to location that Empyrion runs from. This file will contain a single line, that is the relative path from the run folder, to the database folder.

The Database path it is looking for is: "Empyrion - Dedicated Server\Content\Mods" So example modfolder.path content would be:

-If ran from: "Empyrion - Dedicated Server"

The relative path would be "Content/Mods"

-If ran from "Empyrion - Dedicated Server/DedicatedServer"

The relative path would be "../Content/Mods"
