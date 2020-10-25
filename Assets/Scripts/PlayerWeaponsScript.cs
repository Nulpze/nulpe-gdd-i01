using UnityEngine;

public class PlayerWeaponsScript : MonoBehaviour
{

  public GameObject normalWeapon;
  public GameObject specialWeapon;
  public GameObject homingWeapon;
  public GameObject lightningWeapon;
  public int homingWeaponCount = 0;

  void Start()
  {
  }

  void Update()
  {
    if (Input.GetButton("Fire1"))
    {
      FireWeapon(homingWeapon, homingWeaponCount);
      FireWeapon(normalWeapon);
    }

    if (Input.GetButtonDown("Fire2"))
    {
      FireWeapon(specialWeapon);
    }
  }
  public void AddHomingWeapon()
  {
    homingWeaponCount++;
  }

  public void RemovePowerUp()
  {
    if (homingWeaponCount > 0)
    {
      homingWeaponCount--;
    }
  }

  public void RemovePowerUps()
  {
    homingWeaponCount = 0;
  }

  private void FireWeapon(GameObject weapon, int amount = 1)
  {
    if (amount == 0)
    {
      return;
    }
    if (weapon)
    {
      weapon.GetComponent<WeaponScript>().Fire(amount);
    }
  }

}
