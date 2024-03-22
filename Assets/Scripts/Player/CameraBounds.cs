using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        CalculateCameraBounds();
    }

    void CalculateCameraBounds()
    {
        // Calculate viewport bounds
        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Extract minimum and maximum X and Y coordinates
        minX = lowerLeft.x;
        maxX = upperRight.x;
        minY = lowerLeft.y;
        maxY = upperRight.y;

        Debug.Log("Camera Bounds: " + minX + ", " + maxX + ", " + minY + ", " + maxY);
    }
}
