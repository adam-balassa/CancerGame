using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAppearanceBehaviour : MonoBehaviour
{
    CanvasRenderer canvas;
    bool isAppearing = true;
    const float opacitySpeed = 0.1f;
    float currentOpacity = 0.0f;

    void Awake() {
        canvas = GetComponent<CanvasRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetAlpha(0.5f);
    }

    void Update()
    {
        // if (!isAppearing) return;
        // if (currentOpacity >= 0.99f) {
        //     canvas.SetAlpha(1.0f);
        //     isAppearing = false;
        // }
        // else canvas.SetAlpha(currentOpacity);
        canvas.SetAlpha(0.5f);
    }

    void FixedUpdate() {
        float opacity = currentOpacity + Time.fixedDeltaTime * opacitySpeed;
        if (opacity > 1) currentOpacity = 1;
        else currentOpacity = opacity;
    }
}
