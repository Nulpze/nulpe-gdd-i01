using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
  public GameObject normalEnemy;
  public GameObject bigEnemy;
  public ScoreScript scoreScript;
  public Vector2 normalEnemySpawnBounds;
  public Vector2 bigEnemySpawnBounds;
  public float distFromCamera = 10.0f;
  public float nextBigEnemy = 20f;

  private float normalEnemyTimer = 0.0f;
  private float bigEnemyTimer = 0.0f;
  private float nextNormalEnemy;
  private bool active = true;
  private int spawnRate = 6;
  private int bigSpawnRate = 1;
  private float spawnRateTimer = 2f;

  void Start()
  {
    nextNormalEnemy = Random.Range(normalEnemySpawnBounds.x, normalEnemySpawnBounds.y);
    // nextBigEnemy = Random.Range(normalEnemySpawnBounds.x, normalEnemySpawnBounds.y);
  }

  // Update is called once per frame
  void Update()
  {
    if (!active)
    {
      return;
    }
    normalEnemyTimer += Time.deltaTime;
    bigEnemyTimer += Time.deltaTime;
    spawnRateTimer -= Time.deltaTime;
    if (normalEnemyTimer > nextNormalEnemy)
    {
      for (int i = 0; i < spawnRate; i++)
      {
        SpawnEnemy(normalEnemy);
      }
      normalEnemyTimer = 0.0f;
      nextNormalEnemy = Random.Range(normalEnemySpawnBounds.x, normalEnemySpawnBounds.y);
    }
    if (bigEnemyTimer > nextBigEnemy)
    {
      for (int i = 0; i < bigSpawnRate; i++)
      {
        SpawnEnemy(bigEnemy);
      }
      nextBigEnemy = Random.Range(bigEnemySpawnBounds.x, bigEnemySpawnBounds.y);
      bigEnemyTimer = 0f;
    }
    if (spawnRateTimer < 0)
    {
      CheckSpawnRate();
      spawnRateTimer = 2f;
    }
  }

  public int GetSpawnRate()
  {
    return spawnRate;
  }

  public void Stop()
  {
    this.active = false;
  }

  private void SpawnEnemy(GameObject prefab, float rndX = 5.0f)
  {
    var enemy = Instantiate(prefab, transform);
    var pos = new Vector3(
      Camera.main.transform.position.x + Camera.main.orthographicSize * 2 + Random.Range(0f, rndX),
      Random.Range(-10.0f, 10.0f),
      0
    );
    enemy.transform.position = pos;
    Destroy(enemy, 20f);
  }

  private void CheckSpawnRate()
  {
    if (scoreScript.GetTime() == 30)
    {
      IncreaseSpawnRate(2);
      bigSpawnRate++;
    }
    if (scoreScript.GetTime() == 60)
    {
      IncreaseSpawnRate(2);
      bigSpawnRate++;
    }
    if (scoreScript.GetTime() == 100)
    {
      IncreaseSpawnRate(2);
      bigSpawnRate++;
    }
    if (scoreScript.GetTime() == 150)
    {
      IncreaseSpawnRate(2);
      bigSpawnRate++;
    }
    if (scoreScript.GetTime() == 200)
    {
      IncreaseSpawnRate(2);
      bigSpawnRate++;
    }
    if (scoreScript.GetTime() == 250)
    {
      IncreaseSpawnRate(2);
      bigSpawnRate++;
    }
  }

  private void IncreaseSpawnRate(int amount = 1)
  {
    spawnRate += amount;
    Debug.Log("Increased spawn rate: " + spawnRate);
  }
}
