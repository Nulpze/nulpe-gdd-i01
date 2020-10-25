using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of sounds from code with no effort
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{

  /// <summary>
  /// Singleton
  /// </summary>
  public static SoundEffectsHelper Instance;

  public AudioSource explosionSound;
  public AudioSource gameOverSound;
  public AudioSource getPowerUpSound;
  public AudioSource damageSound;
  public AudioSource gameMusic;

  void Awake()
  {
    // Register the singleton
    if (Instance != null)
    {
      Debug.LogError("Multiple instances of SoundEffectsHelper!");
    }
    Instance = this;
  }

  public void MakeExplosionSound()
  {
    explosionSound.Play();
  }

  public void MakePlayGameOverSound()
  {
    gameMusic.Stop();
    gameOverSound.Play();
  }

  public void PlayGetPowerUpSound()
  {
    getPowerUpSound.Play();
  }

  public void PlayDamageSound()
  {
    damageSound.Play();
  }
}