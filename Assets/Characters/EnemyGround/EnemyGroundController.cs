using UnityEngine;

public class EnemyGroundController : CharacterBase
{
    public int coinsAward;

    public float travelDistance;
    public float stopDuration;

    public float touchDamagePerSecond;

    private void OnCollisionStay2D(Collision2D collision)
    {
        var playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController?.RecieveDamage(touchDamagePerSecond * Time.deltaTime);
    }

    protected override void OnDeath()
    {
        var player = GameObject.FindWithTag("Player");
        var playerController = player?.GetComponent<PlayerController>();
        playerController.RecieveCoins(coinsAward);       
    }

    protected override GameObject GetShootTarget()
    {
        return GameObject.FindWithTag("Player");
    }

    protected override bool CanShoot()
    {
        return rigidBody2d.velocity == Vector2.zero;
    }
}
