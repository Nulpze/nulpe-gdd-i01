using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnScript : MonoBehaviour
{
  public GameObject prefab;
  public float minSpawnTime = 2f;
  public float maxSpawnTime = 4f;
  public float initCooldown = 30f;

  private float timer = 0.0f;
  private float nextTime;
  private bool active = true;

  // Start is called before the first frame update
  void Start()
  {
    nextTime = Random.Range(minSpawnTime, maxSpawnTime);
  }

  // Update is called once per frame
  void Update()
  {
    if (!active)
    {
      return;
    }
    timer += Time.deltaTime;

    if (timer < initCooldown)
    {
      return;
    }

    if (timer > nextTime)
    {
      SpawnPowerUp();
      timer = 0.0f;
      nextTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
  }

  public void Stop()
  {
    active = false;
  }

  private void SpawnPowerUp()
  {
    var powerUp = Instantiate(prefab, transform);
    var pos = new Vector3(
      Camera.main.transform.position.x + Camera.main.orthographicSize * 2 + 20f,
      Random.Range(-5.0f, 5.0f),
      0
    );
    powerUp.transform.position = pos;
    Destroy(powerUp, 50f);
  }
}
