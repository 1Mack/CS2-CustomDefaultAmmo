using System.Text.Json.Serialization;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;


namespace CustomDefaultAmmo;

public class CustomDefaultAmmoConfig : BasePluginConfig
{
  [JsonPropertyName("Weapons")] public string Weapons { get; set; } = "weapon_awp,11,-1|weapon_deagle,-1,15";
}
public class CustomDefaultAmmo : BasePlugin, IPluginConfig<CustomDefaultAmmoConfig>
{
  public override string ModuleName => "CustomDefaultAmmo";
  public override string ModuleDescription => "Change the default ammo on a weapon";
  public override string ModuleAuthor => "1MaaaaaacK";
  public override string ModuleVersion => "1.0";


  public required CustomDefaultAmmoConfig Config { get; set; }

  public void OnConfigParsed(CustomDefaultAmmoConfig config)
  {

    if (string.IsNullOrEmpty(config.Weapons))
    {
      throw new Exception($"Invalid value has been set to config value 'Weapons'");
    }

    Config = config;
  }

  public override void Load(bool hotReload)
  {
    RegisterListener<Listeners.OnEntityCreated>(entity =>
    {
      if (entity == null || entity.Entity == null || !entity.IsValid || !entity.DesignerName.Contains("weapon_")) return;

      foreach (var item in Config.Weapons.Split("|"))
      {
        string[] weaponValues = item.Split(",");

        if (weaponValues.Length < 2 || weaponValues[0].Trim() != entity.DesignerName) continue;

        var weapon = new CBasePlayerWeapon(entity.Handle);
        Server.NextFrame(() =>
        {
          if (!weapon.IsValid) return;

          CCSWeaponBase _weapon = weapon.As<CCSWeaponBase>();
          if (_weapon == null) return;

          if (weaponValues[1] != "-1")
          {
            if (_weapon.VData != null)
            {
              _weapon.VData.MaxClip1 = int.Parse(weaponValues[1]);
              _weapon.VData.DefaultClip1 = int.Parse(weaponValues[1]);
            }

            _weapon.Clip1 = int.Parse(weaponValues[1]);

            Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_iClip1");

          }

          if (weaponValues[2].Length > 0 && weaponValues[2] != "-1")
          {
            if (_weapon.VData != null)
            {
              _weapon.VData.PrimaryReserveAmmoMax = int.Parse(weaponValues[2]);
            }
            _weapon.ReserveAmmo[0] = int.Parse(weaponValues[2]);

            Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_pReserveAmmo");
          }
        });
      }
    });
  }
}
