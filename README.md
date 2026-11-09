<div align="center">

<img src="Assets/Menu%20Assets/NGB-LOGO.png" alt="Never Go Back logo" width="520">

### A multiplayer 3D race to the top – one wrong jump and there is no going back.

![Unity](https://img.shields.io/badge/Unity-6000.0.26f1-000000?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-.NET-512BD4?logo=dotnet&logoColor=white)
![URP](https://img.shields.io/badge/Render%20Pipeline-URP-2A9D8F)
![Photon PUN 2](https://img.shields.io/badge/Multiplayer-Photon%20PUN%202-004480)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows&logoColor=white)
![EPITA](https://img.shields.io/badge/EPITA-1st%20year%20project-E30613)

</div>

---

## About

**Never Go Back** is an online multiplayer 3D platformer developed in **C# with Unity** by a team of five
students during our **first year at EPITA** (2024 – 2025).

Players join the same room and wait in a lobby box until four of them are ready (or the host starts the game),
then race through a vertical obstacle course full of moving platforms, falling blocks, pendulums and traps.
Fall, and you go back to your last checkpoint. Players who reach the end of the map become spectators and
watch the others until the final ranking is shown.

## Features

- **Online multiplayer** – create or join a room by name thanks to **Photon PUN 2**, with synchronised
  movement, animations and nametags.
- **Three maps / game modes**
  | Mode | Scene | Description |
  |------|-------|-------------|
  | 🏔️ **Mountains** | `MapScene` | A mountain parkour course with moving platforms, conveyor belts, falling blocks and pendulums. |
  | 🌆 **Vertigo** | `Orange_PresetScene Lite` | A retrowave vertical climb above the void. |
  | 🔥 **Hell Tower** | `OnlyUp` | Climb a demonic tower while **lava rises** below you, on platforms **procedurally generated** by the host. |
- **Procedural maze & AI chaser** – the end of Hell Tower drops players into a randomly generated maze
  where a demon (Unity **NavMesh** agent) hunts the closest player.
- **Archer enemies** on the Mountains map that track and shoot at players within range.
- **Checkpoints & respawn**, kill zones, ejectors and teleporters.
- **Spectator mode & live ranking** once you finish or get caught.
- **Skin selection** (police officer or biker), saved between sessions.
- **Cinematics** with Timeline, post-processing, ambient music and sound effects.
- **Settings menu** – volume, mouse sensitivity, FOV, fullscreen / windowed.
- **Discord Rich Presence** integration.

## Controls

| Action | Key |
|--------|-----|
| Move | `W` `A` `S` `D` (physical keys – `Z` `Q` `S` `D` on AZERTY) |
| Look around | Mouse |
| Jump | `Space` |
| Pause menu | `Esc` |
| Force-start the game (host only, in the lobby) | `F1` |

## Download & play (Windows)

A ready-to-play Windows build is included at the root of the repository: [`Build.rar`](Build.rar).

1. Download `Build.rar` and extract it (with 7-Zip, WinRAR…).
2. Run `Build/NeverGoBack.exe` (keep it next to the `NeverGoBack_Data/` folder).
3. Pick a skin, choose a game mode, enter a username and a room name.
4. Share the room name with your friends so they can join you.

## Open the project in Unity

### Requirements

- [Unity Hub](https://unity.com/download) with **Unity 6000.0.26f1** (Unity 6) – *Windows Build Support* module
- About 6 GB of free disk space (Unity generates a large `Library/` folder on first import)

### Steps

```bash
git clone https://github.com/AlexLaGrinta/NeverGoBack.git
```

1. In Unity Hub, click **Add → Add project from disk** and select the cloned folder.
2. Open it with Unity **6000.0.26f1** – the first import can take several minutes.
3. Open `Assets/Scenes/Main Menu.unity` and press **Play**.

> **Photon:** the project uses the team's Photon App ID (`Assets/Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset`).
> If you fork the project, create your own free App ID on the [Photon dashboard](https://dashboard.photonengine.com/)
> and paste it in *Window → Photon Unity Networking → Highlight Server Settings*.

> **Discord SDK:** the Discord package is fetched from a third-party scoped registry declared in
> `Packages/manifest.json`. If it cannot be resolved, remove the `com.discord.game-sdk` entry and
> `Assets/Scripts/DiscordManager.cs`; the game works without it.

## Project structure

```text
NeverGoBack/
├── Assets/
│   ├── Scenes/                   # Main Menu, SkinSelection, MapScene (Mountains),
│   │                             # Orange_PresetScene Lite (Vertigo), OnlyUp (Hell Tower)
│   ├── Scripts/                  # Gameplay scripts
│   │   ├── Controller/           # First-person controller & Input System bindings
│   │   ├── Spectator/            # Spectator mode & ranking
│   │   ├── disapear/             # Lobby box & game start countdown
│   │   ├── Knife/  Collision/
│   │   └── *.cs                  # Moving / falling platforms, obstacles, checkpoints, archers, Discord…
│   ├── IA/                       # Procedural maze & Hell Tower platforms, NavMesh chaser, final cup
│   ├── Multiplayer/              # Room manager, player setup & network sync (Photon)
│   ├── Menu/                     # Main menu, settings, game modes, pause menu (Echap/), room lobby
│   ├── Resources/                # Networked player prefabs spawned by Photon
│   ├── Prefabs/  Models/  Animations/  Materials/  Fonts/
│   ├── Player/                   # Character textures
│   ├── Menu Assets/  Medias/  Sounds/   # UI art, music, video & sound effects
│   ├── Timeline/                 # Map intro cinematics
│   ├── PostProcessingProfile/  Settings/  # Post-processing & URP pipeline assets
│   ├── Photon/  TextMesh Pro/    # SDKs
│   └── AssetMap/, Assets hell tower/, …  # Asset Store packs (models, textures, VFX, skyboxes…)
├── Packages/                     # Unity package manifest
├── ProjectSettings/
└── Build.rar                     # Windows build
```

## Tech stack

| | |
|---|---|
| **Engine** | Unity 6 (6000.0.26f1), Universal Render Pipeline |
| **Language** | C# |
| **Networking** | Photon PUN 2 (rooms, RPCs, `PhotonView` synchronisation) |
| **AI** | Unity AI Navigation (NavMesh agents) |
| **Input** | Unity Input System |
| **Other** | Cinemachine, Timeline, TextMesh Pro, Discord Game SDK |

## Team – *Kawok Enjoyers*

| Member | GitHub | Main contributions |
|--------|--------|--------------------|
| **Alexandre Pern** – *project lead* | [@AlexLaGrinta](https://github.com/AlexLaGrinta) | Hell Tower procedural generation, end-of-map maze AI, player movement, multiplayer |
| **Eliot Soumeillan** | [@Olyex](https://github.com/Olyex) | Mountains map (first map) |
| **Cédric Tailheuret** | – | Vertigo map (second map) |
| **Ioen Moliard** | – | Hell Tower art direction, UI & VFX |
| **Alexandre Peraldi** | – | Player skins |

## Credits

This was a non-commercial student project. Third-party content belongs to its respective authors:

- **Networking:** Photon PUN 2 – Exit Games
- **Characters & animations:** Mixamo (Adobe), *Humanoid Giant* – Kevin Iglesias, *Demon Horror Creature with Weapon*,
  *Warrior Pack Bundle 2 FREE* – ExplosiveLLC
- **Environment:** *Fantasy Skybox FREE*, *RETROWAVE SKIES Lite* – Suggo Creations, *Demonic Altar / Hell Gate / Blood Pool* – Paulos Creations,
  *Stylized Rock Pack*, *HDRP Oak Tree*, *polygonTrees*, *Fantasy Village House*, *CS – Signs Free*, *Trophy Cups*, *Old Torch*,
  *Defender Knife Set*
- **Textures & shaders:** *4K Tiled Ground Textures*, *Yughues Free Concrete / Ground Materials*, *Stylized Lava PBR Texture*,
  *Stylized Wood Textures*, *Stylize Snow Texture*, *Handpainted Grass & Ground Textures*, *ADG Textures*,
  *Lava Flowing Shader*, *Simple Water Shader* – IgniteCoders
- **VFX:** *Full Opaque VFX*, *VFX Pack Fire Wall*
- **Audio:** Mixkit and other royalty-free sources
- **Paintings in Hell Tower:** public-domain artworks (e.g. John Martin – *Pandemonium*, Louvre)

Asset Store packages are subject to the [Unity Asset Store EULA](https://unity.com/legal/as-terms).

---

<div align="center">
Made with ❤️ at EPITA
</div>
