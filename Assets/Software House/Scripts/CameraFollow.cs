using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private Vector3 startingPosition;

    public Transform followTarget;
    public float smoothing = 1f;
    public Vector3 offset;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<StateController>() != null)
            {
                print("Follow: " + hit.transform.name);
                followTarget = hit.transform;
                AIControllerUI.instance.Set( hit.collider.GetComponent<StateController>());
            }
        }
        //else if (Input.GetMouseButtonDown(0) && followTarget != null && !AIControllerUI.instance.panel.gameObject.activeSelf)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<StateController>() != null)
        //    {
        //        print("Follow: "+ hit.transform.name);
        //        followTarget = hit.transform;
        //        AIControllerUI.instance.Set(hit.collider.GetComponent<StateController>());
        //    }
        //    else
        //    {
        //        followTarget = null;
        //        print("Follow mode: diable.");
        //    }
        //}

        if (AIControllerUI.instance.panel.gameObject.activeSelf)
        {
            Vector3 targetCamPos = followTarget.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
