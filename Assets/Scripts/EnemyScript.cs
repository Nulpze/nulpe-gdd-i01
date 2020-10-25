using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
  public bool randomWeaponActivation = true;
  public bool canBeTargeted = true;

  private bool hasSpawn;
  private MoveScript moveScript;
  private WeaponScript[] weapons;
  private Collider2D coliderComponent;
  private SpriteRenderer rendererComponent;
  private HealthScript healthScript;
  private float randomShotCooldown = -1;
  private bool isTargetedByRocket = false;

  void Awake()
  {
    // Retrieve the weapon only once
    weapons = GetComponentsInChildren<WeaponScript>();

    // Retrieve scripts to disable when not spawn
    moveScript = GetComponent<MoveScript>();

    coliderComponent = GetComponent<Collider2D>();

    rendererComponent = GetComponent<SpriteRenderer>();
    healthScript = GetComponent<HealthScript>();
  }

  // 1 - Disable everything
  void Start()
  {
    hasSpawn = false;

    // Disable everything
    // -- collider
    coliderComponent.enabled = false;
    // -- Moving
    moveScript.enabled = false;
    // -- Shooting
    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = false;
    }
  }

  void Update()
  {
    if (randomShotCooldown > 0)
    {
      randomShotCooldown -= Time.deltaTime;
    }
    // 2 - Check if the enemy has spawned.
    if (hasSpawn == false)
    {
      if (rendererComponent.IsVisibleFrom(Camera.main))
      {
        Spawn();
      }
    }
    else
    {
      if (randomShotCooldown < 0)
      {
        // Auto-fire
        foreach (WeaponScript weapon in weapons)
        {
          if (weapon != null && weapon.enabled)
          {
            weapon.Fire();
          }
        }
        randomShotCooldown = Random.Range(0.01f, 1f);
      }

      // 4 - Out of the camera ? Destroy the game object.
      if (rendererComponent.IsVisibleFrom(Camera.main) == false)
      {
        Destroy(gameObject);
      }
    }
  }


  void OnCollisionEnter2D(Collision2D collision)
  {
    HomingWeaponScript test = collision.gameObject.GetComponent<HomingWeaponScript>();
    if (test != null)
    {
      healthScript.Damage(100);
      test.FindTarget();
    }
    // Collision with enemy
    ExplodingRocketScript explosionScript = collision.gameObject.GetComponent<ExplodingRocketScript>();
    if (explosionScript != null)
    {
      healthScript.Damage(100);
    }
  }

  public void Target()
  {
    if (canBeTargeted)
    {
      isTargetedByRocket = true;
    }
  }

  public bool IsTargeted()
  {
    return isTargetedByRocket;
  }

  // 3 - Activate itself.
  private void Spawn()
  {
    hasSpawn = true;

    // Enable everything
    // -- Collider
    coliderComponent.enabled = true;
    // -- Moving
    moveScript.enabled = true;
    // -- Shooting
    foreach (WeaponScript weapon in weapons)
    {
      if (randomWeaponActivation)
      {
        weapon.enabled = Random.Range(0, 100) > 90;
      }
      else
      {
        weapon.enabled = true;
      }
    }
  }
}