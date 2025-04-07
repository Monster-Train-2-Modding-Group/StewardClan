# Clan Design Document

This is a basic design document to help outline the design of a new clan for Monster Train 2. The goal is to provide a structured approach to development and ensure a cohesive theme and mechanics.

## Themes

### Visual
The Visuals are to be based on the Train Stewards, Mechanical, Tech Priest, etc... There should be some pseudo-britishness.
Colors should be orange-brown, furnace-like.
Shapes should be pipe-like, built out of cutlery and piping.
Effects should be fiery, but mostly contained to mechanical
inspirations  include: 'Brazil', 'Warhammer 40k', 'Steampunk', 'Stanley Kubrick' and 'Batman'

### Mechanical
The Steward Clan focuses on several key mechanical themes:

1. Resource Management 
   - Generate and Sacrifice Many different resources, from Pyre Health, Ember, Cards in Hand, Equipment, Room Size, etc...
   - Ember generation (Overclock, Dependabot)
   - Card draw and discard (Cache, Sanity, Memory Leak)
   - Room manipulation (Winged Air Conditioner)

2. Pyre Interaction 
   - The Pyre is not a Passive item to be defended, but rather an active thing to balance.
   - Direct Pyre healing (Sanity, Wrench-Head, Mod Forger)
   - Pyre damage for benefits (Mechanical Outrage, Overclock)
   - Pyre attack scaling (Ludo-Developer)

3. Offensive Capabilities 
   - Powerful Direct Damage, at a Cost. Weak Direct Damage, for a gain.
   - Direct damage (Decompile, Steam Burst)
   - Multi-hit effects (Bypass, Scrap)
   - Scaling damage (Ludo-Developer)

4. Auto-Defense Mechanics 
   - Focus on Defense that applies as you try to achieve your other goals.
   - Armor application (Try, Catch)
   - Health restoration (Process Data)
   - Damage mitigation (Titanskin)

5. Production
   - Making more Equipment and Equipping it is good in support of what you are doing already.
   - Equipment manipulation (Fabricator, Disarm)
   - Artifact creation and modification (Mod Forger)

6. Steward Synergy 
   - Tribal Buffs to Stewards taking advantage of your starting units
   - Steward-specific buffs (Steward Tome)
   - Steward generation (Automate)
   - Steward equipment interaction (Fabricator)

7. Construct
   - A Status Effect that gives +X/+X at the end of the turn to whatever it is equipped to. 
   - Gives a Strong Buff effect allowing things to grow
   - 

## Clan

### Champions

#### Albert, the Mad Butler
Focuses in on X-Cost, Train Stewards, Extra-Deploy

Base:
Summon: Gain +2X/+4X (X/3/10/5)

Upgrade Path 1 (Utility)

- Reduce X by 3.
- Reduce X by 3. Summon: Gain +4X/+6X
- Reduce X by 4. Summon: Gain +6X/+9X

Upgrade Path 2 (Stewardly)

- Summon: Attach a Room that gives Stewards +10/+10 and Titanskin 2.
- Summon: Attach a Room that gives Stewards +20/+20 and Titanskin 4.
- Summon: Attach a Room that gives Stewards +40/+40 and Titanskin 6.

Upgrade Path 3 (Overengineered)

- Summon: Gain 2 Ember (+10/+3)
- Summon: Gain 4 Ember (+10/+3)
- Summon: Gain 6 Ember (+15/+9)


#### Grug the Debugger
Focuses in on Pyre-Healing, Titan Skin, and Discard Synergy 


### Starter Cards
Mechanical Outrage: Piercing. Deal 5 damage and 1 to the Pyre.

## Cards

### Spells

#### Common
Overclock - Gain 2 Ember. Pyre takes 1 Damage. Costs 1. (Utility) +
Cache - Permafrost. Draw +2 Next Turn. Costs 1. (Utility) +

Process Data - Advance. Apply +5 Health. Costs 0. (Defense) +
Try, Catch - Offering. Apply 20 armor to the front friendly unit. Costs 3. (Defense) +
Sythesize - Consume. Apply Rage 3 and Regen 3. Costs 1. +

Decompile - Deal 25 Damage. Slay: Discard a card at random. Cost 1. (Offense) +
Steam Burst - Deal 2 Damage to Everything, gain an Ember. Costs 1. (Offense) +
Bypass - Explosive. Piercing. Deal 12 Damage to the front enemy unit. Costs 1. (Offense) +
Console.Writeline - Apply Construct 3. 2 (Offense) +

#### Uncommon
Back to Basics - Consume. Remove all Buff Effects. Apply Multistrike 1. Costs 2. +
Galvanize - Consume. -2 Capacity and 3 ember this turn. +
Gravitas - Reduce a Cooldown to 0. Costs 0. +
Scrap - Sacrifice. Deal 50 damage twice to the front enemy unit. Costs 1. +
Disarm - Return an Equipment from a Unit to your hand. Apply +10 Health. Costs 1. +
Automate - Add a Random Steward with two random upgrades to Hand. Costs 1.
Pyre Refactor - Your Pyre gets +10 attack and +2 Health +

#### Rare
Memory Leak - Consume. Discard your Hand, then Draw 3 random spells from your consume pile that isn't memory leak. Costs 1.
Breaking Point - Deal 100 Damage to all enemy units in the tower. Pyre takes 7 Damage. Costs 3. +
Steward Tome - Consume. Apply sweep to a friendly unit. 3
Spike of the Stewards - Consume. Apply Titanskin X. Pyre lose X. Costs X. +
Sanity - Discard your Hand. Restore 10 Pyre Health. Costs 3. 
Ctrl-V - Consume. Add a Copy of your Champion to your Hand. Costs 2.

### Units

#### Common
Rusted Steward: Summon: Gain Damage Shield 1 and Multistrike 1. Train Steward (1/1/5/8) (ember/size/attack/health)

#### Uncommon
Dependabot: Endless. Ability: Gain an Ember. cooldown 3. (1/1/0/2)
Imptern: Summon: Heal the Pyre for 1. Ability: Repeat the last summon trigger. Cooldown 3.(1/1/1/1)
Winged Air Conditioner: Quick. Ability: Friendly Units gain Construct 1. Cooldown 4. (1/2/18/8)
Overheating Computer: Extinguish: Deal Damage equal to this Units Attack to Enemy Units Twice (1/2/20/3)
Wrench-Head: Strike: Heal the Pyre by 1 (1/2/15/30) +
Fabricator: Ability: Add a Copy of the Equipment to your Hand. Cooldown 4. (1/3/10/20)
Steward Amalgamm: Titanskin 2 (1/3/14/25) +

#### Rare
Ludo-Developer: Resolve: Gain Construct 2. (2/2/0/30)
Mod Forger: Artificer: Heal the Pyre by 2. (1/3/40/20) 
Unit Tester: Artificer: Apply Melee Weakness 1 to enemy units. (2/2/35/20)

### Rooms

### Equipment

## Relics

### Common Relics
- **Maintenance Manual** - Train Stewards Enters with Construct 3.
- **Emergency Protocol** - When your Pyre takes damage, apply Damage Shield 1 to all friendly units.
- **Backup Generator** - Whenever you play a room, gain 2 energy.
- **Storage Locker** - At the beginning of Combat, spawn three rusted stewards on the middle floor.

### Rare Relics
- **The Butler's Pocketwatch** - At the start of your turn, add a random consumed card back to your hand.
- **Mechanical Heart** - When your Pyre would be destroyed, set its HP to 10 instead. (Once per battle)
- **Construct Core** - Double Construct and its give Armor.

Each relic reinforces one or more of the clan's core themes:
1. Resource Management (Backup Generator, Resource Recycler)
2. Pyre Interaction (Pyre Capacitor, Mechanical Heart, Overclocked Pyre)
3. Production (Spare Parts Box, Steward's Legacy)
4. Auto-Defense (Emergency Protocol)
5. Steward Synergy (Maintenance Manual, Steward's Toolkit)
6. Construct (Construct Core)

## Upgrades
- Unit upgrades: Custom or shared with other clans?
- Special mechanics related to upgrading units.
- Champion upgrade trees and how they define different viable strategies.

## Additional Notes
- Any special mechanics or rules that set the clan apart.
- Interaction with existing game systems.
- Potential challenges and balance concerns.

