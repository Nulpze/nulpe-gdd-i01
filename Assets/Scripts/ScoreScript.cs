using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

  [SerializeField] GameObject uiText = null;


  private float score = 0;
  private float timer = 0.0f;

  void Start()
  {
    SetScore();
  }

  void Update()
  {
    timer += Time.deltaTime;
    float minutes = timer / 60;
    score = Mathf.Floor(timer % 60) + Mathf.Floor(minutes) * 60;
    SetScore();
  }

  public float GetScore()
  {
    return score;
  }

  private void SetScore()
  {
    this.uiText.GetComponent<UnityEngine.UI.Text>().text = $"Score: {this.score:0}";
  }
}
