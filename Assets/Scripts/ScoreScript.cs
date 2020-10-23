using UnityEngine;

public class ScoreScript : MonoBehaviour
{

  [SerializeField] GameObject uiText = null;
  public EnemySpawnerScript enemySpawnerScript;

  private bool active = true;
  private float score = 0;
  private float timer = 0.0f;

  void Start()
  {
    SetScore();
  }

  void Update()
  {
    if (active)
    {
      timer += Time.deltaTime;
      float minutes = timer / 60;
      score = Mathf.Floor(timer % 60) + Mathf.Floor(minutes) * 60;
      SetScore();
    }
  }

  public void Stop()
  {
    this.active = false;
    var text = uiText.GetComponent<UnityEngine.UI.Text>();
    text.fontStyle = FontStyle.Bold;
    text.alignment = TextAnchor.LowerCenter;
    text.rectTransform.localScale = new Vector3(1, 1, 1);
  }

  public float GetScore()
  {
    return score;
  }

  private void SetScore()
  {
    uiText.GetComponent<UnityEngine.UI.Text>().text = $"Score: {this.score:0}";
    if (score > 20 && enemySpawnerScript.GetSpawnRate() == 1)
    {
      enemySpawnerScript.IncreaseSpawnRate();
    }
    if (score > 50 && enemySpawnerScript.GetSpawnRate() == 2)
    {
      enemySpawnerScript.IncreaseSpawnRate();
    }
    if (score > 100 && enemySpawnerScript.GetSpawnRate() == 3)
    {
      enemySpawnerScript.IncreaseSpawnRate();
    }
  }
}
