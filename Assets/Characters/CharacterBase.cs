using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public abstract class CharacterBase : MonoBehaviour
{
    public event EventHandler Died;
    public HealthPointsChangedEvent healthPointsChanged;

    public float maxHealthPoints;
    public float speed;

    public float delayBetweenShots;
    public float projectileDamage;
    public float projectileSpawnDistance;

    public GameObject projectilePrefab;

    private float HealthPoints { get; set; }

    protected virtual void Start()
    {
        Assert.IsNotNull(projectilePrefab);
        HealthPoints = maxHealthPoints;
        healthPointsChanged.Invoke(HealthPoints);
        StartCoroutine(ContinuousShooting());
    }

    protected virtual void OnDeath()
    {
        // empty
    }

    public void RecieveDamage(float damageAmount)
    {
        HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);
        healthPointsChanged.Invoke(HealthPoints);
        if (HealthPoints <= 0)
        {
            OnDeath();
            Died?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
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
            if (target != null)
                SpawnProjectile(target.transform.position);
        }
    }
}

[Serializable]
public class HealthPointsChangedEvent: UnityEvent<float> { }