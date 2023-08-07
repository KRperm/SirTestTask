using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class NavigationTilemapService : MonoBehaviour
{
    public UnityEvent pathUpdated;

    public GameObject goal;
    public Tilemap tilemap;
    public int maxWaves;

    private Dictionary<Vector3Int, int> TileWeightTable { get; } = new Dictionary<Vector3Int, int>();
    private Vector3Int GoalCellPosition { get; set; }

    private void Awake()
    {
        Assert.IsNotNull(goal);
        Assert.IsNotNull(tilemap);
        GoalCellPosition = tilemap.WorldToCell(goal.transform.position);
        RunPathfinding(GoalCellPosition);
    }

    private void Update()
    {
        if (goal == null)
            return;

        var currentGoalPosition = tilemap.WorldToCell(goal.transform.position);
        if (currentGoalPosition != GoalCellPosition)
        {
            GoalCellPosition = currentGoalPosition;
            RunPathfinding(GoalCellPosition);
        }
    }

    public List<Vector3> GetPositionsWithWeight(int filterWeight)
    {
        return TileWeightTable
            .Where(x => x.Value >= filterWeight)
            .Select(x => tilemap.CellToWorld(x.Key) + tilemap.cellSize / 2)
            .ToList();
    }

    public Queue<Vector2> GetPath(Vector2 startingPosition)
    {
        var path = new Queue<Vector2>();
        var currentTilePosition = tilemap.WorldToCell(startingPosition);
        if (GetTileWeight(currentTilePosition) == int.MaxValue)
            return path;

        for(var i = 0; i < maxWaves; i++)
        {
            var nextTilePosition = GetNeighboringTileWithLeastWeight(currentTilePosition);
            var nextPosition = tilemap.CellToWorld(nextTilePosition) + tilemap.cellSize / 2;
            path.Enqueue(nextPosition);
            currentTilePosition = nextTilePosition;
            if (GetTileWeight(currentTilePosition) == 0)
                break;
        }

        return path;
    }

    private Vector3Int GetNeighboringTileWithLeastWeight(Vector3Int tilePosition)
    {
        var minWeightTilePosition = MinTileWeight(tilePosition + Vector3Int.up, tilePosition + Vector3Int.left);
        minWeightTilePosition = MinTileWeight(tilePosition + Vector3Int.right, minWeightTilePosition);
        minWeightTilePosition = MinTileWeight(tilePosition + Vector3Int.down, minWeightTilePosition);
        return minWeightTilePosition;
    }

    private Vector3Int MinTileWeight(Vector3Int firstTilePosition, Vector3Int secondTilePosition)
    {
        var firstTileWeight = GetTileWeight(firstTilePosition);
        var secondTileWeight = GetTileWeight(secondTilePosition);
        return firstTileWeight < secondTileWeight ? firstTilePosition : secondTilePosition;
    }

    private int GetTileWeight(Vector3Int tilePosition)
    {
        if (tilemap.HasTile(tilePosition) || !TileWeightTable.TryGetValue(tilePosition, out var weight))
            return int.MaxValue;
        return weight;
    }


    private void RunPathfinding(Vector3Int startingPoint)
    {
        TileWeightTable.Clear();
        var currentWeight = 0;
        TileWeightTable.Add(startingPoint, currentWeight);

        for (var i = 0; i < maxWaves; i++)
        {
            var beforeWaveTileCount = TileWeightTable.Count;
            var nextWave = TileWeightTable.Where(x => x.Value == currentWeight).Select(x => x.Key).ToArray();
            currentWeight++;
            SetTileWeightWave(nextWave, currentWeight);
            if (beforeWaveTileCount == TileWeightTable.Count)
                break;
        }
        pathUpdated.Invoke();
    }

    private void SetTileWeightWave(Vector3Int[] cellPositions, int weight)
    {
        foreach (var waveTilePosition in cellPositions)
        {
            TrySetTileWeight(waveTilePosition + Vector3Int.up, weight);
            TrySetTileWeight(waveTilePosition + Vector3Int.left, weight);
            TrySetTileWeight(waveTilePosition + Vector3Int.right, weight);
            TrySetTileWeight(waveTilePosition + Vector3Int.down, weight);
        }
    }

    private void TrySetTileWeight(Vector3Int newTilePosition, int currentWeight)
    {
        if (!tilemap.HasTile(newTilePosition) && !TileWeightTable.ContainsKey(newTilePosition))
            TileWeightTable.Add(newTilePosition, currentWeight);
    }
}
