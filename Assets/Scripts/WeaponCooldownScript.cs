using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponCooldownScript : MonoBehaviour
{

  /// <summary>
  /// Cooldown in seconds between two shots
  /// </summary>
  public float shootingRate = 0.25f;

  public bool initWithCooldown = false;

  public float shootCooldown;

  void Start()
  {
    if (initWithCooldown)
    {
      shootCooldown = shootingRate;
      return;
    }
    shootCooldown = 0f;
  }

  void Update()
  {
    if (shootCooldown > 0)
    {
      shootCooldown -= Time.deltaTime;
    }
  }

  public void CoolDown()
  {
    if (CanAttack)
    {
      shootCooldown = shootingRate;
    }
  }

  /// <summary>
  /// Is the weapon ready to create a new projectile?
  /// </summary>
  public bool CanAttack
  {
    get
    {
      return shootCooldown <= 0f;
    }
  }
}
