using UnityEngine;

public class ExplodingRocketScript : MonoBehaviour
{
  public Vector2 boxColliderEnhance = new Vector2(5.0f, 5.0f);
  public float destroyAfter = 0.5f;
  private BoxCollider2D boxCollider;
  private SpriteRenderer spriteRenderer;
  private bool exploded = false;
  private bool blinking = false;
  private BlinkingScript blinkingScript;

  // Start is called before the first frame update
  void Start()
  {
    boxCollider = GetComponent<BoxCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    blinkingScript = GetComponent<BlinkingScript>();
  }

  // Update is called once per frame
  void Update()
  {
    var dist = (transform.position - Camera.main.transform.position).z;
    var rightBorder = Camera.main.ViewportToWorldPoint(
      new Vector3(1, 0, dist)
    ).x;
    var critical = Camera.main.ViewportToWorldPoint(
      new Vector3(0.9f, 0, dist)
    ).x;
    var center = Camera.main.ViewportToWorldPoint(
      new Vector3(0.5f, 0, dist)
    ).x;
    if (transform.position.x > center && !blinking)
    {
      blinking = true;
      blinkingScript.Blink();
    }
    if (transform.position.x > critical && !exploded)
    {
      Explode();
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // Collision with enemy
    EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
    if (enemy != null)
    {
      Explode();
    }
  }

  private void Explode()
  {
    exploded = true;
    GetComponent<Animator>().enabled = false;
    spriteRenderer.enabled = false;
    SpecialEffectsHelper.Instance.Explosion(transform.position, boxColliderEnhance.x * 2);
    boxCollider.size = new Vector2(boxColliderEnhance.x, boxColliderEnhance.y);
    Destroy(gameObject, destroyAfter);
  }
}
