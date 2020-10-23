
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

  public Image healthPrefab;
  private Canvas canvas;

  private List<Image> hearts;
  private float destroyAfter = 0f;
  private Image toDestroy = null;

  public void Update()
  {
    if (destroyAfter >= 0)
    {
      destroyAfter -= Time.deltaTime;
    }
    if (destroyAfter < 0 && toDestroy)
    {
      DestroyHeart();
    }
  }

  public void SetHealth(int health)
  {
    hearts = new List<Image>();
    canvas = GetComponent<Canvas>();
    for (int i = 0; i < health; i++)
    {
      var heart = Instantiate(healthPrefab, canvas.transform) as Image;
      heart.rectTransform.position = new Vector3(
        heart.rectTransform.position.x - heart.sprite.rect.width * i,
        heart.rectTransform.position.y,
        heart.rectTransform.position.z
      );
      hearts.Add(heart);
    }
  }

  public void TakeDamage()
  {
    toDestroy = hearts.LastOrDefault();
    var blinkingScript = toDestroy.GetComponent<BlinkingScript>();
    if (blinkingScript)
    {
      blinkingScript.Blink(1f);
    }
    destroyAfter = blinkingScript ? 1f : 0.1f;
  }

  private void DestroyHeart()
  {
    hearts.Remove(toDestroy);
    Destroy(toDestroy.gameObject);
    toDestroy = null;
  }
}
