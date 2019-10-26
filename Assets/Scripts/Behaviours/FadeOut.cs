
 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    const float fadingFactor = 0.6f;
    float currentFade = 0.0f;
    bool fadingOut = false;
    Image image;
    ChooseLevelManager.Level level;
    void Awake() {
        ChooseLevelManager.Instance.OnChooseLevel.AddListener((ChooseLevelManager.Level l) => {
            level = l;
            fadingOut = true;
        });
        image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!fadingOut) return;
        image.color = new Color(image.color.r, image.color.g, image.color.b, currentFade * currentFade * currentFade);
    }

    void FixedUpdate() {
        if (!fadingOut) return;
        currentFade += Time.fixedDeltaTime * fadingFactor;
        if (currentFade >= 1) {
            SceneManager.LoadScene(ChooseLevelManager.getLevelScene(level));
        } 
    }
}
