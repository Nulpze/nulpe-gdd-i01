using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Start or quit the game
/// </summary>
public class GameOverScript : MonoBehaviour
{
  public ScoreScript scoreScript;
  public EnemySpawnerScript enemySpawnerScript;

  private Button[] buttons;

  void Awake()
  {
    // Get the buttons
    buttons = GetComponentsInChildren<Button>();

    // Disable them
    HideButtons();
  }

  public void HideButtons()
  {
    foreach (var b in buttons)
    {
      b.gameObject.SetActive(false);
    }
  }

  public void ShowButtons()
  {
    foreach (var b in buttons)
    {
      b.gameObject.SetActive(true);
    }
    scoreScript.Stop();
    enemySpawnerScript.Stop();
    DespawnGameObjects("Projectile");
    DespawnGameObjects("Enemy");
    DespawnGameObjects("PowerUp");

  }

  public void ExitToMenu()
  {
    // Reload the level
    SceneManager.LoadScene("Menu");
  }

  public void RestartGame()
  {
    // Reload the level
    SceneManager.LoadScene("Stage1");
  }

  private void DespawnGameObjects(string tag)
  {
    GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
    foreach (var obj in objects)
    {
      Destroy(obj);
    }

  }
}