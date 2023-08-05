using UnityEngine;

public class PlayerController : CharacterBase
{
    private Vector2 MovementDirection { get; set; }


    private void FixedUpdate()
    {
        rigidBody2d.velocity = MovementDirection * speed * Time.deltaTime;
    }

    public void ChangeMovement(Vector2 newDirection)
    {
        MovementDirection = newDirection;
    }

    protected override GameObject GetShootTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var distanceToClosestEnemy = float.PositiveInfinity;
        GameObject closestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToClosestEnemy > distance)
            {
                distanceToClosestEnemy = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    protected override bool CanShoot()
    {
        return MovementDirection == Vector2.zero;
    }
}
