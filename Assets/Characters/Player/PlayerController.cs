using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody2d;

    private Vector2 MovementDirection { get; set; }

    private void Start()
    {
        Assert.IsNotNull(rigidBody2d);
    }

    private void FixedUpdate()
    {
        rigidBody2d.velocity = MovementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("a");
    }

    public void ChangeMovement(Vector2 newDirection)
    {
        MovementDirection = newDirection;
    }
}
