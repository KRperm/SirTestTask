using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class EnemyBase : CharacterBase
{
    public int coinsAward;

    public float travelDistance;
    public float stopDuration;

    public float touchDamagePerSecond;

    public Odometer odometer;

    protected bool IsTraveling { get; private set; }

    protected override void Start()
    {
        Assert.IsNotNull(odometer);
        StartCoroutine(ContinuousMoving());
        base.Start();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController?.RecieveDamage(touchDamagePerSecond * Time.deltaTime);
    }

    protected override GameObject GetShootTarget()
    {
        var player = GameObject.FindWithTag("Player");
        return CanSeePlayer() ? player : null;
    }

    protected override bool CanShoot()
    {
        return !IsTraveling;
    }

    protected override void OnDeath()
    {
        var player = GameObject.FindWithTag("Player");
        var playerController = player?.GetComponent<PlayerController>();
        playerController.RecieveCoins(coinsAward);
    }

    protected bool CanSeePlayer()
    {
        var player = GameObject.FindWithTag("Player");
        if (player == null)
            return false;
        var directionToPlayer = (player.transform.position - transform.position).normalized;
        var raycastPosition = Vector2.MoveTowards(transform.position, player.transform.position, projectileSpawnDistance); ;
        var raycastMask = LayerMask.GetMask("Ground Entity", "Air Entity", "Player");
        var hitResult = Physics2D.Raycast(raycastPosition, directionToPlayer, float.PositiveInfinity, raycastMask);
        return hitResult.collider != null && hitResult.collider.CompareTag("Player");
    }

    private IEnumerator ContinuousMoving()
    {
        while (true)
        {
            IsTraveling = false;
            yield return new WaitForSeconds(stopDuration);
            odometer.ResetDistance();
            IsTraveling = true;
            yield return new WaitUntil(() => odometer.TraveledDistance >= travelDistance);
        }
    }
}
