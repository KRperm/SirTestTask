using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFormatter : MonoBehaviour
{
    public string format;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        Assert.IsNotNull(text);
    }

    public void FormatAndDisplayText(float item)
    {
        FormatAndDisplayText((object)item);
    }

    public void FormatAndDisplayText(int item)
    {
        FormatAndDisplayText((object)item);
    }

    public void FormatAndDisplayText(object item)
    {
        var result = string.Format(format, item);
        text.text = result;
    }
}
