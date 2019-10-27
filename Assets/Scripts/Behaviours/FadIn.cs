
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FadIn : MonoBehaviour
{
    const float fadingFactor = 0.6f;
    float currentFade = 0.0f;
    bool fadingOut = true;
    Image image;
    ChooseLevelManager.Level level;
    void Awake() {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!fadingOut) return;
        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Min(1 - currentFade, 0));
        if (currentFade >= 1) fadingOut = false;
    }

    void FixedUpdate() {
        if (!fadingOut) return;
        currentFade += Time.fixedDeltaTime * fadingFactor;
    }
}

