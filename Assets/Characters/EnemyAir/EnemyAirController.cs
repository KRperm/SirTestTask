using UnityEngine;
using UnityEngine.Assertions;

public class EnemyAirController : EnemyBase
{
    public Rigidbody2D rigidBody2D;

    protected override void Start()
    {
        Assert.IsNotNull(rigidBody2D);
        base.Start();
    }

    private void FixedUpdate()
    {
        var player = GameObject.FindWithTag("Player");
        if (!IsTraveling || player == null)
        {
            rigidBody2D.velocity = Vector2.zero;
            return;
        }
        var direction = (player.transform.position - transform.position).normalized;
        rigidBody2D.velocity = direction * speed * Time.deltaTime;
    }
}
