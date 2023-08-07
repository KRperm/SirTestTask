using UnityEngine;
using UnityEngine.Assertions;

public class EnemyGroundController : EnemyBase
{
    public NavigationTilemapAgent navigationAgent;

    protected override void Start()
    {
        Assert.IsNotNull(navigationAgent);
        navigationAgent.speed = speed;
        base.Start();
    }

    private void Update()
    {
        navigationAgent.enabled = IsTraveling;
    }

    protected override void OnDeath()
    {
        var player = GameObject.FindWithTag("Player");
        var playerController = player?.GetComponent<PlayerController>();
        playerController.RecieveCoins(coinsAward);       
    }
}
