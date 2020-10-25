using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
  public void StartGame()
  {
    SceneManager.LoadScene("Stage1");
  }

  public void HowTo()
  {
    SceneManager.LoadScene("HowTo");
  }

  public void Menu()
  {
    SceneManager.LoadScene("Menu");
  }
}