using UnityEngine;
using UnityEngine.UI;

public class BlinkingScript : MonoBehaviour
{
  public float blinkDuration = 0f;

  public bool isImage = false;

  private float previousDuration = 100f;

  private Color color;

  private void Start()
  {
    if (isImage)
    {
      color = GetComponent<Image>().color;
    }
    else
    {
      color = GetComponent<SpriteRenderer>().color;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (blinkDuration > 0f)
    {
      blinkDuration -= Time.deltaTime;
      if ((previousDuration - blinkDuration) > 0.1f)
      {
        color = new Color(
          color.r,
          color.b,
          color.g,
          color.a < 1f ? 1f : 0.5f
        );
        SetColor();
        previousDuration = blinkDuration;
      }
    }
  }

  public void Blink(float duration = 2f)
  {
    previousDuration = 100f;
    blinkDuration = duration;
  }

  private void SetColor()
  {
    if (isImage)
    {
      GetComponent<Image>().color = color;
    }
    else
    {
      color = GetComponent<SpriteRenderer>().color = color;
    }
  }
}
