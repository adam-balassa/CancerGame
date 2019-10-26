using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class BrownMotion : MonoBehaviour {

  public float radius;
  float time;

	void Awake() {
    time = Random.Range(0f, 2 * Mathf.PI);
  }
  
  void Update() {
    time += Time.deltaTime;
    float movementAngle = Random.Range(0f, (2 * Mathf.PI));
    Vector3 vectorToBeAdded = new Vector3(radius * Mathf.Sin(Time.time / 2) / 4, radius * Mathf.Cos(2 * time / 2) / 4, 0f);
    this.transform.localPosition = (vectorToBeAdded);
  }
}
