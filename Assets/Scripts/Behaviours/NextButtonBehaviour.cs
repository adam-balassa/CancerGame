
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
    const string mainScene = "LevelChooser";
    public void OnPointerClick(PointerEventData e) {
        SceneManager.LoadScene(mainScene);
    }
}
