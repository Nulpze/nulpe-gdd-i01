using UnityEngine;

public class ScoreScript : MonoBehaviour
{

  [SerializeField] GameObject uiText = null;
  [SerializeField] GameObject uiTime = null;
  public EnemySpawnerScript enemySpawnerScript;

  private bool active = true;
  private float score = 0;
  private float time = 0;
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
      time = Mathf.Floor(timer % 60) + Mathf.Floor(minutes) * 60;
      SetScore();
      uiTime.GetComponent<UnityEngine.UI.Text>().text = $"{time}";
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
  public float GetTime()
  {
    return time;
  }


  public void Score(int points = 1)
  {
    score += points;
    SetScore();
  }

  private void SetScore()
  {
    uiText.GetComponent<UnityEngine.UI.Text>().text = $"Score: {GetScore()}";
  }
}
