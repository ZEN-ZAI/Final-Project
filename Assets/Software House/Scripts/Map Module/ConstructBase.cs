using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructBase : MonoBehaviour
{
    public ConstructSlot constructSlot;
    public bool canPlacement;

    private MeshRenderer temp;

    public void FindGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2), transform.position.z);
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction*100 , Color.green);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            transform.parent.GetComponent<ConstructControl>().height = hit.point.y + (transform.localScale.y / 2) + 0.1f;

            Debug.DrawRay(ray.origin, new Vector3(0, -hit.distance, 0), Color.blue);

            if (hit.collider.GetComponent<ConstructSlot>().aboveConstruct == null)
            {
                canPlacement = true;
                constructSlot = hit.collider.GetComponent<ConstructSlot>();

                hit.collider.GetComponent<MeshRenderer>().material.color = Color.green;

                if (temp == null)
                {
                    temp = hit.collider.GetComponent<MeshRenderer>();
                }

                if (hit.collider.GetComponent<MeshRenderer>() != temp)
                {
                    temp.material.color = Color.clear;
                }
                temp = hit.collider.GetComponent<MeshRenderer>();

            }
            else
            {
                canPlacement = false;
                constructSlot = null;

                hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                if (temp == null)
                {
                    temp = hit.collider.GetComponent<MeshRenderer>();
                }

                if (hit.collider.GetComponent<MeshRenderer>() != temp)
                {
                    temp.material.color = Color.clear;
                }

                temp = hit.collider.GetComponent<MeshRenderer>();
            }
        }
        else
        {
            canPlacement = false;
            constructSlot = null;
        }
    }

    public void ResetHighlight()
    {
        temp.material.color = Color.clear;
    }
}