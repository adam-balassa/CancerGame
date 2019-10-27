using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlickeringBehaviour : MonoBehaviour {

	public float minValue;
	public float maxValue;

	new SpriteRenderer renderer;
	float elapsed;

	void Awake() {
		renderer = GetComponent<SpriteRenderer>();

		elapsed = Random.Range(0f, 2 * Mathf.PI);
	}

	void Update() {
		elapsed += Time.deltaTime;

		float strength =
			Mathf.Sin(elapsed) +
			Mathf.Sin(elapsed * 2) / 2 +
			Mathf.Sin(elapsed * 4) / 4;

		strength = Mathf.Clamp((strength + 1.22f) / 2.44f, 0, 1);

		renderer.color = new Color(
			renderer.color.r,
			renderer.color.g,
			renderer.color.b,
			strength * (maxValue - minValue) + minValue
		);
	}
}
