using System.Linq;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

  /// <summary>
  /// 1 - The speed of the ship
  /// </summary>

  public Vector2 speed = new Vector2(50, 50);
  public bool flipOnShoot = false;
  private Vector3 defaultScale = new Vector3(0, 0, 0);
  private Vector3 flipScale = new Vector3(0, 0, 0);

  // 2 - Store the movement and the component
  private Vector2 movement;
  private Rigidbody2D rigidbodyComponent;

  private void Awake()
  {
    defaultScale = transform.localScale;
    flipScale = defaultScale;
    flipScale.x *= -1;
  }

  // Update is called once per frame
  void Update()
  {
    // 3 - Retrieve axis information
    float inputX = Input.GetAxis("Horizontal");
    float inputY = Input.GetAxis("Vertical");

    // 4 - Movement per direction
    movement = new Vector2(
      speed.x * inputX,
      speed.y * inputY
    );

    var dist = (transform.position - Camera.main.transform.position).z;

    var leftBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(0, 0, dist)
    ).x;

    var rightBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(1, 0, dist)
    ).x;

    var topBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(0, 0, dist)
    ).y;

    var bottomBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(0, 1, dist)
    ).y;

    transform.position = new Vector3(
      Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
      Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
      transform.position.z
    );

  }

  void FixedUpdate()
  {
      // 5 - Get the component and store the reference
      if (rigidbodyComponent == null) {
          rigidbodyComponent = GetComponent<Rigidbody2D>();
      } 

      // 6 - Move the game object
      rigidbodyComponent.velocity = movement;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // Collision with enemy
    EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
    if (enemy != null)
    {
      // Kill the enemy
      HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
      if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

      HealthScript playerHealth = this.GetComponent<HealthScript>();
      if (playerHealth != null) playerHealth.Damage(1);
    }
  }

  void OnDestroy()
  {
    // Game Over.
    var gameOver = FindObjectOfType<GameOverScript>();
    if (gameOver)
    {
      gameOver.ShowButtons();
    }
    SoundEffectsHelper.Instance.MakePlayGameOverSound();
  }
}
