using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPanelBehaviour : MonoBehaviour
{
    ChooseLevelManager manager;
    bool isAppearing = false;
    bool isDisappearing = false;
    const float dS = 150.0f;
    float translation = -60.0f;
    void Awake() {
        isAppearing = true;
        manager = ChooseLevelManager.Instance;
    
        manager.OnChooseLevel.AddListener((ChooseLevelManager.Level _) => {
            Destroy(gameObject);
        });

        manager.OnSelectLevel.AddListener((ChooseLevelManager.Level level) => {
            if (level == ChooseLevelManager.Level.NONE) isDisappearing = true;
        });

        this.transform.localPosition = new Vector3(0, translation, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isAppearing || isDisappearing) this.transform.localPosition = new Vector3(0, translation, 0);
        else this.transform.localPosition = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (isAppearing) {
            translation += dS * Time.fixedDeltaTime;
            if (translation > 0) {
                translation = 0;
                isAppearing = false;
            }
        }
        if (isDisappearing) {
            translation -= dS * Time.fixedDeltaTime;
            if (translation < -60.0) {
                Destroy(gameObject);
            }
        }
    }
}
