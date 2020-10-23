using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
  private bool hasSpawn;
  private MoveScript moveScript;
  private WeaponScript[] weapons;
  private Collider2D coliderComponent;
  private SpriteRenderer rendererComponent;
  private HealthScript healthScript;

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
      // Auto-fire
      foreach (WeaponScript weapon in weapons)
      {
        if (weapon != null && weapon.enabled && weapon.CanAttack)
        {
          weapon.Attack(true);
        }
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
    // Collision with enemy
    ExplodingRocketScript explosionScript = collision.gameObject.GetComponent<ExplodingRocketScript>();
    if (explosionScript != null)
    {
      Debug.Log("EXPLODED!!");
      healthScript.Damage(100);
    }
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
      weapon.enabled = true;
    }
  }
}