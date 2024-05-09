# CS2 CustomDefaultAmmo
Plugin for CS2 that modify the default value of clip1 and reserve ammo.

## Installation
1. Install **[CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp/releases)** and **[Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)**;
3. Download **[CustomDefaultAmmo](https://github.com/1Mack/CS2-CustomDefaultAmmo/releases)**;
4. Unzip the archive and upload it into **`csgo/addons/counterstrikesharp/plugins`**;

## Config
The config is created automatically. ***(Path: `csgo/addons/counterstrikesharp/configs/plugins/CustomDefaultAmmo`)***
```
{
  "Version": 1,
  "Weapons": [
    {
      "Name": "weapon_awp",
      "Clip1": 10,
      "ReserveAmmo": -1
    },
    {
     ...
    }
  ]
}
```
