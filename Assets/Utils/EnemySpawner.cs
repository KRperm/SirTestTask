using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{
    public int enemyGroundAmount;
    public int enemyAirAmount;
    public int filterWeight;

    public NavigationTilemapService navigationService;
    public GameObject enemyGroundPrefab;
    public GameObject enemyAirPrefab;

    private void Start()
    {
        Assert.IsNotNull(navigationService);
        Assert.IsNotNull(enemyGroundPrefab);
        Assert.IsNotNull(enemyAirPrefab);

        var emptyPositions = navigationService.GetPositionsWithWeight(filterWeight);
        SpawnEnemies(enemyGroundPrefab, enemyGroundAmount, emptyPositions);
        SpawnEnemies(enemyAirPrefab, enemyAirAmount, emptyPositions);
    }

    private void SpawnEnemies(GameObject enemyPrefab, int amount, List<Vector3> emptyPositions)
    {
        for (var i = 0; i < amount; i++)
        {
            if (emptyPositions.Count == 0)
                break;
            var index = Random.Range(0, emptyPositions.Count);
            Instantiate(enemyPrefab, emptyPositions[index], Quaternion.identity);
            emptyPositions.RemoveAt(index);
        }
    }
}
