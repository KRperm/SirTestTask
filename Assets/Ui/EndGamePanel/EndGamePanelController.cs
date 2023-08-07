using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EndGamePanelController : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        Assert.IsNotNull(text);
    }

    public void SetWinPanel()
    {
        gameObject.SetActive(true);
        text.text = "Победа!";
    }

    public void SetLosePanel()
    {
        gameObject.SetActive(true);
        text.text = "Поражение!";
    }
}
