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

  public HealthBarScript healthBarScript;
  private BlinkingScript blinkingScript;
  private float invincibleFor = 0f;
  private int currentHp = 0;

  public void Awake()
  {
    currentHp = hp;
    if (healthBarScript)
    {
      healthBarScript.SetHealth(hp);
    }
    blinkingScript = GetComponent<BlinkingScript>();
  }

  public void Update()
  {
    if (invincibleFor > 0)
    {
      invincibleFor -= Time.deltaTime;
      isInvincible = true;
    }
    if (invincibleFor < 0 && isInvincible)
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
      currentHp -= damageCount;

      if (isEnemy)
      {
        SoundEffectsHelper.Instance.MakeExplosionSound();
      }
      else
      {
        if (currentHp > 1)
        {
          SoundEffectsHelper.Instance.PlayDamageSound();
        }
        healthBarScript.TakeDamage();
        invincibleFor = 1f;
        GetComponent<PlayerWeaponsScript>().RemovePowerUp();
      }

      if (blinkingScript)
      {
        blinkingScript.Blink(invincibleFor);
      }
    }

    if (currentHp <= 0)
    {
      GameObject.FindGameObjectWithTag("GameScripts").GetComponent<ScoreScript>().Score(hp);
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