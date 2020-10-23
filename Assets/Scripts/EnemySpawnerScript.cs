using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
  public GameObject prefab;
  public float minSpawnTime = 2f;
  public float maxSpawnTime = 4f;
  public float distFromCamera = 10.0f;
  public int doubleSpawnThreshold = 50;

  private float timer = 0.0f;
  private float nextTime;
  private bool active = true;
  private int spawnRate = 1;

  void Start()
  {
    nextTime = Random.Range(minSpawnTime, maxSpawnTime);
    /*SpawnEnemy(-3f);
    SpawnEnemy(-3f);
    SpawnEnemy(-3f);*/
  }

  // Update is called once per frame
  void Update()
  {
    if (!active)
    {
      return;
    }
    timer += Time.deltaTime;

    if (timer > nextTime)
    {
      for (int i = 0; i < spawnRate; i++)
      {
        SpawnEnemy();
      }
      timer = 0.0f;
      nextTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
  }

  public void IncreaseSpawnRate()
  {
    spawnRate++;
  }

  public int GetSpawnRate()
  {
    return spawnRate;
  }

  public void Stop()
  {
    this.active = false;
  }

  private void SpawnEnemy(float rndX = 5.0f)
  {
    var enemy = Instantiate(prefab, transform);
    var pos = new Vector3(
      Camera.main.transform.position.x + Camera.main.orthographicSize * 2 + Random.Range(0f, rndX),
      -50 + Random.Range(-10.0f, 10.0f),
      0
    );
    enemy.transform.position = pos;
    Destroy(enemy, 20f);
  }
}
