using System.Collections;
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
        return GameObject.FindWithTag("Player");
    }

    protected override bool CanShoot()
    {
        return !IsTraveling;
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
