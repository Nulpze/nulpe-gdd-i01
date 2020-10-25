
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

  public Image healthPrefab;

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
    for (int i = 0; i < health; i++)
    {
      Image heart = Instantiate(healthPrefab, transform);
      Debug.Log($"Heart width: {heart.sprite.rect.width} height: {heart.sprite.rect.height} x: {heart.transform.position.x}");
      heart.rectTransform.anchoredPosition = new Vector3(-heart.sprite.rect.width * i, 0, 0);
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
