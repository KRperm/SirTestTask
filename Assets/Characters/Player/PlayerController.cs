using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody2d;
    public GameObject projectilePrefab;

    private Vector2 MovementDirection { get; set; }

    private void Start()
    {
        Assert.IsNotNull(rigidBody2d);
        Assert.IsNotNull(projectilePrefab);
        StartCoroutine(ContinuousShooting());
    }

    private void FixedUpdate()
    {
        rigidBody2d.velocity = MovementDirection * speed * Time.deltaTime;
    }

    public void ChangeMovement(Vector2 newDirection)
    {
        MovementDirection = newDirection;
    }

    private IEnumerator ContinuousShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            yield return new WaitWhile(() => rigidBody2d.velocity != Vector2.zero);
            print("a");
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}
