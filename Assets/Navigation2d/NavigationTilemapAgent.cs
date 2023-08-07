using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NavigationTilemapAgent : MonoBehaviour
{
    public bool isNavigationEnabled = true;
    public float speed;
    public float pointReachedRadius;
    public Rigidbody2D rigidBody2D;

    private Queue<Vector2> Path { get; set; }
    private NavigationTilemapService NavigationService { get; set; }

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(rigidBody2D);
        NavigationService = FindObjectOfType<NavigationTilemapService>();
        Assert.IsNotNull(NavigationService);
        NavigationService.pathUpdated.AddListener(NavigationService_PathUpdated);
        Path = NavigationService.GetPath(transform.position);
    }

    private void OnDestroy()
    {
        NavigationService.pathUpdated.RemoveListener(NavigationService_PathUpdated);
    }

    private void NavigationService_PathUpdated()
    {
        Path = NavigationService.GetPath(transform.position);
    }

    private void FixedUpdate()
    {
        if (Path.Count == 0 || !isNavigationEnabled)
        {
            rigidBody2D.velocity = Vector3.zero;
            return;
        }

        var nextPoint = Path.Peek();
        if (Vector2.Distance(transform.position, nextPoint) <= pointReachedRadius)
        {
            Path.Dequeue();
            if (Path.Count != 0)
                nextPoint = Path.Peek();
        }

        var direction = (nextPoint - (Vector2)transform.position).normalized;
        rigidBody2D.velocity = direction * speed * Time.deltaTime;
    }
}
