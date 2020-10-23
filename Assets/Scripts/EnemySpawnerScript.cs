using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
  public ScoreScript scoreScript;
  public GameObject prefab;
  public float minSpawnTime = 2f;
  public float maxSpawnTime = 4f;
  public float distFromCamera = 10.0f;
  public int doubleSpawnThreshold = 50;

  private float timer = 0.0f;
  private float nextTime;

  void Start()
  {
    nextTime = Random.Range(minSpawnTime, maxSpawnTime);
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime;

    if (timer > nextTime)
    {
      if (scoreScript.GetScore() > doubleSpawnThreshold)
      {
        SpawnEnemy();
        SpawnEnemy();
      } else
      {
        SpawnEnemy();
      }
      timer = 0.0f;
      nextTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
  }

  private void SpawnEnemy()
  {
    var enemy = Instantiate(prefab, transform);
    var pos = new Vector3(
      Camera.main.transform.position.x + Camera.main.orthographicSize * 2 + 2,
      -50 + Random.Range(-7.0f, 12.0f),
      0
    );
    enemy.transform.position = pos;
    Debug.Log($"Object created x: {pos.x} y: {pos.y}");
    Destroy(enemy, 20f);
  }
}
