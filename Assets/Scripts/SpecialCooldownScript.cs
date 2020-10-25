using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCooldownScript : MonoBehaviour
{

  public Text specialWeaponCDText;
  public Text homingWeaponCDText;
  public Image homingWeaponCDImage;
  public Image specialWeaponCDImage;
  public WeaponCooldownScript specialWeaponCD;
  public PlayerWeaponsScript playerWeapons;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (specialWeaponCD.shootCooldown > 0)
    {
      specialWeaponCDText.text = specialWeaponCD.shootCooldown.ToString("0.00");
      specialWeaponCDImage.color = new Color(255, 255, 255, 0.1f);
    }
    else
    {
      specialWeaponCDText.text = "Right Click";
      specialWeaponCDImage.color = new Color(255, 255, 255, 1);
    }
    homingWeaponCDText.text = playerWeapons.homingWeaponCount.ToString();
  }
}
