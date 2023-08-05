using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : CharacterBase
{
    public CoinsChangedEvent coinsChangedEvent;

    public int coins = 0;

    private Vector2 MovementDirection { get; set; }

    protected override void Start()
    {
        coinsChangedEvent.Invoke(coins);
        base.Start();
    }

    private void FixedUpdate()
    {
        rigidBody2d.velocity = MovementDirection * speed * Time.deltaTime;
    }

    public void ChangeMovement(Vector2 newDirection)
    {
        MovementDirection = newDirection;
    }

    public void RecieveCoins(int recievedCoins)
    {
        coins += recievedCoins;
        coinsChangedEvent.Invoke(coins);
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

[Serializable]
public class CoinsChangedEvent: UnityEvent<int> { }