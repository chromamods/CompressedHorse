# Compressed Horse

![Compressed Horse](icon.png)

A Stardew Valley mod that lets your horse squeeze through one-tile gaps.

In the base game the horse's hitbox is a tile and a half wide, so while riding you can only pass
through gaps at least two tiles wide. Compressed Horse narrows the ridden horse's hitbox to the
farmer's own width, so anywhere you can walk, you can ride.

## Features
- **Slimmer ridden hitbox:** the horse fits through one-tile gaps, fence openings and doorways.
  Mounting, dismounting and clicking a parked horse work exactly as before.
- **Walk through parked horses** (optional, off by default): players pass through horses nobody is
  riding instead of being blocked by them.

## Requirements
- Stardew Valley 1.6
- [SMAPI](https://smapi.io) 4.0 or later
- [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098) (optional, for the
  in-game settings page)

## Install
- **Vortex:** drag the release zip onto the Mods page, then enable and deploy it.
- **Manual:** unzip the release into your `Stardew Valley/Mods` folder.

Launch the game through SMAPI. In multiplayer, every player who wants the effect needs the mod.

## Settings
Open Generic Mod Config Menu from the title screen or in game, or edit `config.json` in the mod
folder after the first launch.

| Setting | Default | Effect |
| --- | --- | --- |
| `PlayersPassThroughHorses` | `false` | Players walk through parked horses. |

## Build from source
Requires the .NET 6 SDK or later and the game with SMAPI installed.

```
dotnet build -c Release
```

The release zip is written to `dist/`.

## License
Copyright (C) 2026 ChromaMods.

This program is free software: you can redistribute it and/or modify it under the terms of the GNU
General Public License as published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version. See [LICENSE](LICENSE).
