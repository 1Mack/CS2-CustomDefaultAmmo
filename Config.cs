using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace CustomDefaultAmmo;

public partial class CustomDefaultAmmo
{
  public required CustomDefaultAmmoConfig Config { get; set; }

  public void OnConfigParsed(CustomDefaultAmmoConfig config)
  {

    if (config.Weapons.Length == 0)
    {
      throw new Exception($"Invalid value has been set to config value 'Weapons'");
    }

    Config = config;
  }
}


public class CustomDefaultAmmoConfig : BasePluginConfig
{
  [JsonPropertyName("Weapons")]
  public Weapons[] Weapons { get; set; } = new Weapons[] { new() };
}
public class Weapons
{
  public string Name { get; set; } = "weapon_awp";
  public int? Clip1 { get; set; } = 10;
  public int? ReserveAmmo { get; set; } = -1;

}