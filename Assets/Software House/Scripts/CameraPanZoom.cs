using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanZoom : MonoBehaviour
{
    #region Singleton
    public static CameraPanZoom instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    Vector3 touchStart;
	public float zoomOutMin = 1;
	public float zoomOutMax = 8;
    public float zoomPower = 5;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    public float minZ;
    public float maxZ;

    [SerializeField] private bool active = true;

	// Update is called once per frame
	void Update()
	{
        if (active)
        {
            if (Input.GetMouseButtonDown(0) && AIControllerUI.instance.active == false)
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.1f);
            }
            else if (Input.GetMouseButton(0) && AIControllerUI.instance.active == false)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Camera.main.transform.position += direction;

                Vector3 temp = Camera.main.transform.position + direction;

                float x = Mathf.Clamp(temp.x, minX, maxX);
                float y = Mathf.Clamp(temp.y, minY, maxY);
                float z = Mathf.Clamp(temp.z, minZ, maxZ);

                Camera.main.transform.position = new Vector3(x,y,z);

            }

            zoom(Input.GetAxis("Mouse ScrollWheel")* zoomPower);
        }
	}

    public void zoom(float increment)
	{
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
	}

    public void SetActive(bool state)
    {
        active = state;
    }
}