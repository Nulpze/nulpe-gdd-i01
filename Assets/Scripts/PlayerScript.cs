using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

  /// <summary>
  /// 1 - The speed of the ship
  /// </summary>
  public Vector2 speed = new Vector2(50, 50);
  public bool flipOnShoot = false;
  public Vector3 defaultScale = new Vector3(0, 0, 0);
  private Vector3 flipScale = new Vector3(0, 0, 0);
  private float flipTimer = 0f;

  // 2 - Store the movement and the component
  private Vector2 movement;
  private Rigidbody2D rigidbodyComponent;

  private void Awake()
  {
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

    if (flipTimer > 0)
    {
      flipTimer -= Time.deltaTime;
    }

    // 5 - Shooting
    bool shoot = Input.GetButton( "Fire1");
    shoot |= Input.GetButton("Fire2");
    // Careful: For Mac users, ctrl + arrow is a bad idea

    if (shoot)
    {
      WeaponScript weapon = GetComponent<WeaponScript>();
      if (weapon != null)
      {
        // Flip the image on the x axis
        if (flipOnShoot)
        {
          flipTimer = 0.5f;
          transform.localScale = flipScale;
        }
        // false because the player is not an enemy
        weapon.Attack(false);
      }
    }
    if (flipTimer < 0)
    {
      transform.localScale = defaultScale;
      flipTimer = 0;
    }
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
}
