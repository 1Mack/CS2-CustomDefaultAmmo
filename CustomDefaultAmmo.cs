using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;


namespace CustomDefaultAmmo;


[MinimumApiVersion(199)]
public partial class CustomDefaultAmmo : BasePlugin, IPluginConfig<CustomDefaultAmmoConfig>
{
  public override string ModuleName => "CustomDefaultAmmo";
  public override string ModuleDescription => "Change the default ammo on a weapon";
  public override string ModuleAuthor => "1MaaaaaacK";
  public override string ModuleVersion => "1.0.1";


  public override void Load(bool hotReload)
  {
    RegisterListener<Listeners.OnEntityCreated>(entity =>
    {
      if (entity == null || entity.Entity == null || !entity.IsValid || !entity.DesignerName.Contains("weapon_")) return;

      foreach (var item in Config.Weapons)
      {
        if (string.IsNullOrEmpty(item.Name) || (!item.Clip1.HasValue && !item.ReserveAmmo.HasValue)) continue;


        Server.NextFrame(() =>
        {

          CBasePlayerWeapon weapon = new(entity.Handle);

          if (!weapon.IsValid) return;

          item.Name = item.Name.Trim();

          if (!checkIfWeapon(item.Name, weapon.AttributeManager.Item.ItemDefinitionIndex)) return;

          CCSWeaponBase _weapon = weapon.As<CCSWeaponBase>();
          if (_weapon == null) return;

          if (item.Clip1.HasValue && item.Clip1 != -1)
          {
            if (_weapon.VData != null)
            {
              _weapon.VData.MaxClip1 = item.Clip1.Value;
              _weapon.VData.DefaultClip1 = item.Clip1.Value;
            }

            _weapon.Clip1 = item.Clip1.Value;

            Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_iClip1");

          }

          if (item.ReserveAmmo.HasValue && item.ReserveAmmo != -1)
          {
            if (_weapon.VData != null)
            {
              _weapon.VData.PrimaryReserveAmmoMax = item.ReserveAmmo.Value;
            }
            _weapon.ReserveAmmo[0] = item.ReserveAmmo.Value;

            Utilities.SetStateChanged(weapon.As<CCSWeaponBase>(), "CBasePlayerWeapon", "m_pReserveAmmo");
          }
        });
      }
    });

    static bool checkIfWeapon(string weaponName, int weaponDefIndex)
    {
      Dictionary<int, string> WeaponDefindex = new()
      {
        { 1, "weapon_deagle" },
        { 2, "weapon_elite" },
        { 3, "weapon_fiveseven" },
        { 4, "weapon_glock" },
        { 7, "weapon_ak47" },
        { 8, "weapon_aug" },
        { 9, "weapon_awp" },
        { 10, "weapon_famas" },
        { 11, "weapon_g3sg1" },
        { 13, "weapon_galilar" },
        { 14, "weapon_m249" },
        { 16, "weapon_m4a1" },
        { 17, "weapon_mac10" },
        { 19, "weapon_p90" },
        { 23, "weapon_mp5sd" },
        { 24, "weapon_ump45" },
        { 25, "weapon_xm1014" },
        { 26, "weapon_bizon" },
        { 27, "weapon_mag7" },
        { 28, "weapon_negev" },
        { 29, "weapon_sawedoff" },
        { 30, "weapon_tec9" },
        { 32, "weapon_hkp2000" },
        { 33, "weapon_mp7" },
        { 34, "weapon_mp9" },
        { 35, "weapon_nova" },
        { 36, "weapon_p250" },
        { 38, "weapon_scar20" },
        { 39, "weapon_sg556" },
        { 40, "weapon_ssg08" },
        { 60, "weapon_m4a1_silencer" },
        { 61, "weapon_usp_silencer" },
        { 63, "weapon_cz75a" },
        { 64, "weapon_revolver" },
      };

      if (WeaponDefindex.TryGetValue(weaponDefIndex, out string? value) && value == weaponName) return true;

      return false;
    }
  }
}
