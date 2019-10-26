using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class BrownMotion : MonoBehaviour {

  public float radius;
  public Vector3 position;
  public float scale;
  public float NucleusRadius;

	void Awake() {
    position = this.transform.position;
    this.transform.localScale = new Vector3(scale, scale, 0);
  }
  void FixedUpdate() {
    float movementAngle = Random.Range(0f, (2*Mathf.PI));
    Vector3 vectorToBeAdded = new Vector3(0.025f*Mathf.Cos(movementAngle), 0.025f*Mathf.Sin(movementAngle), 0f);
    this.transform.Translate(vectorToBeAdded);
    if (Mathf.Sqrt(this.transform.localPosition.x*this.transform.localPosition.x + this.transform.localPosition.y*this.transform.localPosition.y)>=(radius-NucleusRadius)*(radius-NucleusRadius)) {
      this.transform.Translate(-vectorToBeAdded);
    }
  }
}
