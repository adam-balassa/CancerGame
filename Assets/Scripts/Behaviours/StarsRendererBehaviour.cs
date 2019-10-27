using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsRendererBehaviour : MonoBehaviour
{
    public GameObject stars;
    void Awake() {
        LevelManager m = LevelManager.Instance;
        m.OnGameWonEvent.AddListener((short raysFired) => {
            Debug.Log(raysFired);
            StarManager.Instance.CalculateResult(new short[]{m.pointsFor5, m.pointsFor4, m.pointsFor3, m.pointsFor2}, raysFired);
            Instantiate(stars);
        });
    }
}
