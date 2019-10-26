using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FireAnimator : MonoBehaviour {

    public float chargeTime = 1;
    public float blastTime = 0.5f;
    public float sparkTime = 0.2f;

    public Sprite chargeSprite;
    public Sprite blastSprite;
    public SpriteRenderer sparkRenderer;

    new SpriteRenderer renderer;

    float elapsed = 0;

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        sparkRenderer.enabled = false;
    }

    void Update() {
        elapsed += Time.deltaTime;

        if (elapsed < chargeTime) {
            float opacity = elapsed / chargeTime;
            float scale = Mathf.Pow(1 - elapsed / chargeTime, 0.8f);

            renderer.sprite = chargeSprite;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, opacity);
            transform.localScale = new Vector2(scale, scale);
        } else if (elapsed < chargeTime + blastTime) {
            float opacity = 1 - (elapsed - chargeTime) / blastTime;
            float scale = Mathf.Pow((elapsed - chargeTime) / blastTime, 0.5f);

            renderer.sprite = blastSprite;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, opacity);
            transform.localScale = new Vector2(scale, scale);

            if (elapsed < chargeTime + sparkTime) {
                float rotation = (elapsed - chargeTime) / sparkTime * 180;
                float sparkScale = 1 - Mathf.Pow(((elapsed - chargeTime) / sparkTime - 1 * 2), 2);

                sparkRenderer.enabled = true;
                sparkRenderer.transform.eulerAngles = new Vector3(0, 0, rotation);
                sparkRenderer.transform.localScale = new Vector2(sparkScale, sparkScale);
            } else {
                sparkRenderer.enabled = false;
            }
        } else {
            Destroy(gameObject);
        }
    }
}
