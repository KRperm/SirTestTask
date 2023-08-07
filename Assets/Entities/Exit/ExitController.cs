using UnityEngine;
using UnityEngine.Assertions;

public class ExitController : MonoBehaviour
{
    public Color closedColor;
    public Color openedColor;

    public SpriteRenderer sprite;

    private void Awake()
    {
        Assert.IsNotNull(sprite);
        sprite.color = enabled ? openedColor : closedColor;
    }

    private void OnEnable()
    {
        sprite.color = openedColor;
    }

    private void OnDisable()
    {
        sprite.color = closedColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("exited");
            // a
        }
    }
}
