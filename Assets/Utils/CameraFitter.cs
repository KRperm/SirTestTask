using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class CameraFitter : MonoBehaviour
{
    public Camera targetCamera;
    public Renderer objectToFit;

    public void Start()
    {
        Assert.IsNotNull(targetCamera);
        Assert.IsNotNull(objectToFit);
        targetCamera.transform.position = objectToFit.bounds.center - Vector3.forward * 10;        
    }

    private void Update()
    {
        var objectAspect = objectToFit.bounds.extents.y / objectToFit.bounds.extents.x;
        var screenAspect = (float)Screen.height / Screen.width;
        
        targetCamera.orthographicSize = objectAspect > screenAspect 
            ? objectToFit.bounds.extents.y 
            : objectToFit.bounds.extents.x * screenAspect;
    }
}
