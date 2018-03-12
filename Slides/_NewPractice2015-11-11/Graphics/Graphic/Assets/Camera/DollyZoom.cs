using UnityEngine;
using System.Collections;

public class DollyZoom : MonoBehaviour {
    public Transform target;
    public Camera camera;

    private float initHeightAtDist;
    private bool dzEnabled;

    // Calculate the frustum height at a given distance from the camera.
    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance.
    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    // Start the dolly zoom effect.
    void StartDZ()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        dzEnabled = true;
    }

    // Turn dolly zoom off.
    void StopDZ()
    {
        dzEnabled = false;
    }

    void Start()
    {
        StartDZ();
    }

    void Update()
    {

        if (dzEnabled)
        {
            // Measure the new distance and readjust the FOV accordingly.
            var currDistance = Vector3.Distance(transform.position, target.position);
            camera.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
        }

        // Simple control to allow the camera to be moved in and out using the up/down arrows.
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * 5f);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                GameObject.Destroy(hit.transform.gameObject);
                return;
                Renderer rend =  hit.transform.gameObject.GetComponent<Renderer>();
                if (rend)
                {
                    rend.material.color = new Color(255, 0, 0);
                }
            }
        }
    }

}
