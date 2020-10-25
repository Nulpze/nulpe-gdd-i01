using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{

  public Transform weapon;
  public WeaponCooldownScript weaponCooldown;
  public float destroyWeaponAfter = 10f;

  void Update()
  {
  }

  public void Fire(int amount = 1)
  {
    if (!weaponCooldown.CanAttack)
    {
      return;
    }
    for (int i = 0; i < amount; i++)
    {
      var weaponTransform = Instantiate(weapon) as Transform;

      // Assign current position
      weaponTransform.position = transform.position;
      // Make the weapon shot always towards it
      MoveScript move = weaponTransform.gameObject.GetComponent<MoveScript>();
      if (move != null)
      {
        move.direction = this.transform.right; // towards in 2D space is the right of the sprite
      }
      weaponCooldown.CoolDown();
      Destroy(weaponTransform, destroyWeaponAfter);
    }
  }
}
