using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStarsBehaviour : MonoBehaviour
{
    public GameObject emptyStar;
    public GameObject halfStar;
    public GameObject fullStar;
    // Start is called before the first frame update
    void Start()
    {
        short result = StarManager.Instance.Result;
        result = 4;
        switch (result) {
            case 1 :
                drawStar(fullStar, 0);
                drawStar(emptyStar, 1);
                drawStar(emptyStar, 2);
                break;
            case 2 : 
                drawStar(fullStar, 0);
                drawStar(halfStar, 1);
                drawStar(emptyStar, 2);
                break;
            case 3 : 
                drawStar(fullStar, 0);
                drawStar(fullStar, 1);
                drawStar(emptyStar, 2);
                break;
            case 4 : 
                drawStar(fullStar, 0);
                drawStar(fullStar, 1);
                drawStar(halfStar, 2);
                break;
            case 5 : 
                drawStar(fullStar, 0);
                drawStar(fullStar, 1);
                drawStar(fullStar, 2);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void drawStar(GameObject s, short position) {
        Transform child = transform.GetChild(position);
        GameObject star = Instantiate(s, child);
        star.transform.localScale = new Vector3(.5f, .5f, 1);
        
        RectTransform rt = (RectTransform)child.transform;
        star.transform.Translate(new Vector3(rt.rect.width / 2, rt.rect.height / 2, 0));
    }
}
