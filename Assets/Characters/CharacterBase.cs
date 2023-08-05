using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class CharacterBase : MonoBehaviour
{
    public float healthPoints;
    public float speed;

    public float delayBetweenShots;
    public float projectileDamage;
    public float projectileSpawnDistance;

    public Rigidbody2D rigidBody2d;
    public GameObject projectilePrefab;

    private void Start()
    {
        Assert.IsNotNull(rigidBody2d);
        Assert.IsNotNull(projectilePrefab);
        StartCoroutine(ContinuousShooting());
    }

    public void RecieveDamage(float damageAmount)
    {
        healthPoints -= damageAmount;
        if (healthPoints <= 0)
            Destroy(gameObject);
    }

    protected void SpawnProjectile(Vector2 targetPosition)
    {
        var projectileSpawnPoint = Vector2.MoveTowards(transform.position, targetPosition, projectileSpawnDistance);

        var angleToEnemy = Vector2.SignedAngle(Vector2.right, targetPosition - (Vector2)transform.position);
        var projectileRotation = Quaternion.Euler(0, 0, angleToEnemy);

        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint, projectileRotation);
        var projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.damage = projectileDamage;
    }

    protected abstract GameObject GetShootTarget();
    protected abstract bool CanShoot();

    private IEnumerator ContinuousShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenShots);
            yield return new WaitUntil(CanShoot);

            var target = GetShootTarget();
            if (target is null)
                break;

            SpawnProjectile(target.transform.position);
        }
    }
}
