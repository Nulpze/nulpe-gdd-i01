using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour
{
  /// <summary>
  /// Scrolling speed
  /// </summary>
  public Vector2 speed = new Vector2(2, 2);

  /// <summary>
  /// Moving direction
  /// </summary>
  public Vector2 direction = new Vector2(-1, 0);

  /// <summary>
  /// Movement should be applied to camera
  /// </summary>
  public bool isLinkedToCamera = false;

  public bool isLooping = false;
  public bool isLoopingPlatforms = false;
  
  private List<SpriteRenderer> backgroundPart;

  private void Start()
  {
    if (isLooping || isLoopingPlatforms)
    {
      backgroundPart = new List<SpriteRenderer>();

      for(int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);
        SpriteRenderer r = child.GetComponent<SpriteRenderer>();

        if (r != null)
        {
          backgroundPart.Add(r);
        }
      }

      // Sort by position.
      // Note: Get the children from left to right.
      // We would need to add a few conditions to handle
      // all the possible scrolling directions.
      backgroundPart = backgroundPart.OrderBy(
        t => t.transform.position.x
      ).ToList();
    }
  }

  void Update()
  {
    // Movement
    Vector3 movement = new Vector3(
      speed.x * direction.x,
      speed.y * direction.y,
      0
    );

    movement *= Time.deltaTime;
    transform.Translate(movement);

    // Move the camera
    if (isLinkedToCamera)
    {
      Camera.main.transform.Translate(movement);
    }

    if (isLoopingPlatforms)
    {
      SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
      if (firstChild.transform.position.x < Camera.main.transform.position.x)
      {
        if (firstChild.IsVisibleFrom(Camera.main) == false)
        {
          Debug.Log("Spawn new platform now");
          
          firstChild.transform.position = new Vector3(
            Camera.main.transform.position.x + Camera.main.orthographicSize * 2 + firstChild.bounds.size.x,
            firstChild.transform.position.y,
            0
          );
          backgroundPart.Remove(firstChild);
          backgroundPart.Add(firstChild);
        }
      }
    }

    if (isLooping)
    {
      SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
      if (firstChild.transform.position.x < Camera.main.transform.position.x)
      {
        if (firstChild.IsVisibleFrom(Camera.main) == false)
        {
          SpriteRenderer lastChild = backgroundPart.LastOrDefault();
          Vector3 lastPosition = lastChild.transform.position;
          Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

          // Set the position of the recyled one to be AFTER
          // the last child.
          // Note: Only work for horizontal scrolling currently.
          firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

          // Set the recycled child to the last position
          // of the backgroundPart list.
          backgroundPart.Remove(firstChild);
          backgroundPart.Add(firstChild);
        }
      }
    }
  }
}