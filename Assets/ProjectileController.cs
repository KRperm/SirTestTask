using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody2D;

    private void Start()
    {
        Assert.IsNotNull(rigidBody2D);
        rigidBody2D.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
