using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class PongEndPanel : MonoBehaviour 
{
    public Text DisplayText;
    public Button ValidButton;
    public Button CancelButton;

    public void Display(string text, UnityAction okAction, UnityAction cancelAction)
    {
        gameObject.SetActive(true);
        DisplayText.text = text;

        ValidButton.transform.GetChild(0).GetComponent<Text>().text = "Replay";
        ValidButton.onClick.RemoveAllListeners();
        ValidButton.onClick.AddListener(delegate { ValidButton.transform.GetChild(0).GetComponent<Text>().text = "Waiting..."; okAction.Invoke(); });

        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(cancelAction);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
