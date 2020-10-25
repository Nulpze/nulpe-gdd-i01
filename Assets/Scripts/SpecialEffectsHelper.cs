using UnityEngine;

/// <summary>
/// Creating instance of particles from code with no effort
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
  /// <summary>
  /// Singleton
  /// </summary>
  public static SpecialEffectsHelper Instance;

  public ParticleSystem smokeEffect;
  public ParticleSystem fireEffect;
  public GameObject explosionPrefab;

  void Awake()
  {
    // Register the singleton
    if (Instance != null)
    {
      Debug.LogError("Multiple instances of SpecialEffectsHelper!");
    }

    Instance = this;
  }

  /// <summary>
  /// Create an explosion at the given location
  /// </summary>
  /// <param name="position"></param>
  public void Explosion(Vector3 position, float scale = 1.0f)
  {
    // instantiate(smokeEffect, position, big);
    // instantiate(fireEffect, position, big);
    instantiate(explosionPrefab, position, scale);
  }

  /// <summary>
  /// Instantiate a Particle system from prefab
  /// </summary>
  /// <param name="prefab"></param>
  /// <returns></returns>
  private GameObject instantiate(GameObject prefab, Vector3 position, float scale = 1.0f)
  {

    GameObject newParticleSystem = Instantiate(
      prefab,
      position,
      Quaternion.identity
    ) as GameObject;
    newParticleSystem.transform.localScale = new Vector3(
      newParticleSystem.transform.localScale.x * scale,
      newParticleSystem.transform.localScale.y * scale,
      1
    );

    // Make sure it will be destroyed
    Destroy(
      newParticleSystem.gameObject,
      1.2f
    );

    return newParticleSystem;
  }
}