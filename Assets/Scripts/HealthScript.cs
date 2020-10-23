using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
  /// <summary>
  /// Total hitpoints
  /// </summary>
  public int hp = 1;

  /// <summary>
  /// Enemy or player?
  /// </summary>
  public bool isEnemy = true;
  public bool isInvincible = false;

  public Canvas healthBar;
  private HealthBarScript healthBarScript;
  private BlinkingScript blinkingScript;
  private float invincibleDurationAfterDamage = 0f;

  public void Awake()
  {
    if (!isEnemy)
    {
      healthBarScript = healthBar.GetComponent<HealthBarScript>();
      healthBarScript.SetHealth(hp);
    }
    blinkingScript = GetComponent<BlinkingScript>();
  }

  public void Update()
  {
    if (invincibleDurationAfterDamage > 0)
    {
      invincibleDurationAfterDamage -= Time.deltaTime;
      isInvincible = true;
    }
    if (invincibleDurationAfterDamage < 0 && isInvincible)
    {
      isInvincible = false;
    }
  }

  /// <summary>
  /// Inflicts damage and check if the object should be destroyed
  /// </summary>
  /// <param name="damageCount"></param>
  public void Damage(int damageCount)
  {
    if (!isInvincible)
    {
      SoundEffectsHelper.Instance.MakeExplosionSound();
      hp -= damageCount;

      if (!isEnemy)
      {
        healthBarScript.TakeDamage();
        invincibleDurationAfterDamage = 1f;
      }

      if (blinkingScript)
      {
        blinkingScript.Blink(invincibleDurationAfterDamage);
      }
    }

    if (hp <= 0)
    {
      SpecialEffectsHelper.Instance.Explosion(transform.position);
      // Dead!
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D otherCollider)
  {
    // Is this a shot?
    ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
    if (shot != null)
    {
      // Avoid friendly fire
      if (shot.isEnemyShot != isEnemy)
      {
        Damage(shot.damage);

        // Destroy the shot
        Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
      }
    }
  }
}