## Stat names

# Mainstats 

These are the 6 main stats that are primarily affected by character growth, attributes and their equipment.

* ´´HP´´ & ´´MP´´ - Maximum values, not current

* ´´PAtk´´ & ´´MAtk´´ - used for damage calculation, modulated by element alignments

* ´´PDef´´ & ´´MDef´´ - raw defense values, modulated by element alignments

# Secondary stats

These are used for specific things and have visible effect, directly or indirectly

* ´´movement_speed´´ - running speed ingame

* ´´hpregen´´ & ´´mpregen´´ - regen per second, not used by healing effects

# Elenemt alignments

These are multipliers, default to 1.0f, that specify how affected a character is if struck by that element.

Two sets exist for attacking and defending. 

This allows to have a magic character be able to cast extra strong elemental attacks while still being weaker than normal to that element (or resist it on the contrary).

Negative values in the defensive multipliers will mean that element will heal the character - this is likely to be made impossible to achieve for players, but allows for certain enemies to require strategies not involving using that element.

Actual elements and their relation, if any, are to be formalised at a later time.

This will likely include the usual RPG roster of fire, ice, lightning is likely to be present, some kind of nature / plant element, undead, metal / machine, earth, energy / void, possibly water as a separate element.

# Technical stats

These are never shown on the character sheet and are used as a workaround to implement special effects.