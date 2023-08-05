using UnityEngine;

public class Odometer : MonoBehaviour
{
    public float TraveledDistance { get; private set; }
    private Vector3 LastPosition { get; set; }

    public void ResetDistance()
    {
        TraveledDistance = 0;
    }

    private void Update()
    {
        var frameDistance = Vector3.Distance(transform.position, LastPosition);
        TraveledDistance += frameDistance;
        LastPosition = transform.position;
    }
}
