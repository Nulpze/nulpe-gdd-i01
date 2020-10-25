using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
  public bool isHomingPowerUp = true;

  void OnCollisionEnter2D(Collision2D collision)
  {
    // Collision with enemy
    PlayerScript player = collision.gameObject.GetComponent<PlayerScript>();
    if (player != null)
    {
      SoundEffectsHelper.Instance.PlayGetPowerUpSound();
      if (isHomingPowerUp)
      {
        player.GetComponent<PlayerWeaponsScript>().AddHomingWeapon();
        Destroy(gameObject);
      }
    }
  }
}
