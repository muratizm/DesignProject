using UnityEngine;
using UnityEngine.UI;

public class BugButtonHandler : MonoBehaviour
{
    public KillTheBugs killTheBugs;

    void Start()
    {
        // Get the Button component and add a listener to its onClick event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // Call OnClickBug and pass this GameObject
        killTheBugs.OnClickBug(gameObject);
    }
}