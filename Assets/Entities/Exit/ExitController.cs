using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class ExitController : MonoBehaviour
{
    public UnityEvent exitReached;

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
        if (collision.gameObject.CompareTag("Player") && enabled)
            exitReached.Invoke();
    }
}
