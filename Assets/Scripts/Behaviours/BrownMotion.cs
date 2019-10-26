using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class BrownMotion : MonoBehaviour {

  public float radius;
  public float scale;
  public float NucleusRadius;
  float time;

	void Awake() {
    this.transform.localScale = new Vector3(scale, scale, 0);
    NucleusRadius = this.transform.localScale.x/2;
    this.transform.Translate(new Vector3(-NucleusRadius/1.65f, 0f,0f));
    time = Random.Range(0f, 2*Mathf.PI);
  }
  void Update() {
    time += Time.deltaTime;
    float movementAngle = Random.Range(0f, (2*Mathf.PI));
    Vector3 vectorToBeAdded = new Vector3(radius*Mathf.Sin(Time.time/2)/4, radius*Mathf.Cos(2*time/2)/4, 0f);
    this.transform.localPosition = (vectorToBeAdded);
  }
}
