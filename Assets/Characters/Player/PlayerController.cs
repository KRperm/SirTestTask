using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D Rigidbody { get; set; }
    private Vector2 Movement { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(Rigidbody);
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Movement * speed * Time.deltaTime;
    }

    public void ChangeMovement(Vector2 newMovement)
    {
        Movement = newMovement;
    }
}
