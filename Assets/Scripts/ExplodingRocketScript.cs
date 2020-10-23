using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodingRocketScript : MonoBehaviour
{
  public float speed = 200.0f;
  private BoxCollider2D boxCollider;
  private SpriteRenderer spriteRenderer;
  private Transform target;
  private bool moving = true;
  // Start is called before the first frame update
  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerObject.GetComponent<Collider2D>());
    Destroy(gameObject, 5f);
  }

  // Update is called once per frame
  void Update()
  {
    // Move our position a step closer to the target.
    float step = speed * Time.deltaTime; // calculate distance to move
    if (!target)
    {
      Rigidbody2D rigidbodyComponent = GetComponent<Rigidbody2D>();
      rigidbodyComponent.velocity = new Vector2( 5 * 1, 5 * 0);
      FindTarget();
    }
    if (moving)
    {
      Debug.Log("Move to target");
      transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // Collision with enemy
    EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
    if (enemy != null)
    {
      this.moving = false;
      this.spriteRenderer.sprite = null;
      SpecialEffectsHelper.Instance.Explosion(transform.position, true);
      boxCollider.size = new Vector2(2.5f, 2.5f);
      Destroy(gameObject, 0.5f);
    }
  }

  private void FindTarget()
  {
    GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
    IEnumerable<Transform> transforms = targets.Select((target) => target.transform);
    target = GetClosestEnemy(transforms.ToArray());
  }

  Transform GetClosestEnemy(Transform[] enemies)
  {
    Transform bestTarget = null;
    float closestDistanceSqr = Mathf.Infinity;
    Vector3 currentPosition = transform.position;
    foreach (Transform potentialTarget in enemies)
    {
      Vector3 directionToTarget = potentialTarget.position - currentPosition;
      float dSqrToTarget = directionToTarget.sqrMagnitude;
      if (dSqrToTarget < closestDistanceSqr)
      {
        closestDistanceSqr = dSqrToTarget;
        bestTarget = potentialTarget;
      }
    }

    return bestTarget;
  }
}
