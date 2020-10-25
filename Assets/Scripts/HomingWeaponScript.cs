using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingWeaponScript : MonoBehaviour
{
  public float speed = 200.0f;
  public int maxTargets = 100;
  public bool rotateToTarget = true;

  private Transform target;
  private SpriteRenderer rendererComponent;
  private Rigidbody2D rigidbodyComponent;
  private int targetCount = 0;

  // Start is called before the first frame update
  void Start()
  {
    rigidbodyComponent = GetComponent<Rigidbody2D>();
    rendererComponent = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update()
  {
    if (rendererComponent.IsVisibleFrom(Camera.main) == false)
    {
      Destroy(gameObject);
    }
    float step = speed * Time.deltaTime; // calculate distance to move
    if (!target)
    {
      rigidbodyComponent.velocity = new Vector2(10, 0);
      FindTarget();
    }

    if (transform && target)
    {
      rigidbodyComponent.velocity = new Vector2(0, 0);
      transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    /* Rotate not working properly :(
     * if (rotateToTarget)
    {
      var q = Quaternion.LookRotation(target.position - transform.position);
      transform.rotation = Quaternion.RotateTowards(Quaternion.Inverse(transform.rotation), q, speed * Time.deltaTime);
    }*/
  }

  public void FindTarget()
  {
    if (targetCount == maxTargets)
    {
      Destroy(gameObject);
      return;
    }
    GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
    IEnumerable<Transform> transforms = targets.Select((target) => target.transform);
    target = GetClosestEnemy(transforms.ToArray());
    if (target)
    {
      target.GetComponent<EnemyScript>().Target();
      targetCount++;
    }
  }

  Transform GetClosestEnemy(Transform[] enemies)
  {
    Transform bestTarget = null;
    float closestDistanceSqr = Mathf.Infinity;
    Vector3 currentPosition = transform.position;
    foreach (Transform potentialTarget in enemies)
    {
      if (potentialTarget.GetComponent<EnemyScript>().IsTargeted())
      {
        continue;
      }
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
