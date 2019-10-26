using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelCameraBehaviour : MonoBehaviour
{
    bool isZooming = false;
    const float deltaZoom = 100.0f;
    const float maxZoom = 178.4f;
    float currentZoom = maxZoom;
    new Camera camera;
    Vector3 zoomTarget;
    Vector3 zoomSource = new Vector3(388, 183, -1);
    Vector3 deltaVec;
    Vector3 translation;
    void Awake() {
        ChooseLevelManager manager = ChooseLevelManager.Instance;
        manager.OnChooseLevel.AddListener(zoomOnOrgan);
        camera = GetComponent<Camera>();
        translation = new Vector3(zoomSource.x, zoomSource.y, -1);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isZooming) return;
        camera.orthographicSize = currentZoom;
        camera.transform.position = translation;
    }

    void FixedUpdate() {
        if (!isZooming) return;
        currentZoom -= Time.fixedDeltaTime * deltaZoom;
        if (currentZoom <= 0) isZooming = false;
        translation += Time.fixedDeltaTime * deltaVec;
    }

    void zoomOnOrgan(ChooseLevelManager.Level level) {
        isZooming = true;
        switch(level) {
            case ChooseLevelManager.Level.BARIN:
                zoomTarget = new Vector3(386, 316, -1);
                break;
            case ChooseLevelManager.Level.BONE:
                zoomTarget = new Vector3(344, 237, -1);
                break;
            case ChooseLevelManager.Level.LEUCHEMIA:
                zoomTarget = new Vector3(368, 142, -1);
                break;
            case ChooseLevelManager.Level.LYMPHOMA:
                zoomTarget = new Vector3(388, 280, -1);
                break;
            case ChooseLevelManager.Level.RECTUM:
                zoomTarget = new Vector3(405, 189, -1);
                break;
        }
        deltaVec = (zoomTarget - zoomSource) * (deltaZoom / maxZoom);
        Debug.Log(deltaVec.x + " " + deltaVec.y + " " + deltaVec.z);
    }
}
