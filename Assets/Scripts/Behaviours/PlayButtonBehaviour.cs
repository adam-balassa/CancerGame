using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnPointerClick(PointerEventData _) {
        ChooseLevelManager manager = ChooseLevelManager.Instance;
        manager.LevelChosen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
