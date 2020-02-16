using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    public bool active;
    [SerializeField] private GameObject construct;

    public void SetActive(bool state)
    {
        active = state;

        if (state == false)
        {
            CancelPlacement();
        }
    }

    [SerializeField] private bool moveMode;
    [SerializeField] private bool rotateMode;
    [SerializeField] private bool deleteMode;

    void LateUpdate()
    {

        if (active)
        {
            if (moveMode)
            {
                MoveConstruct();
            }
            else if (rotateMode)
            {
                RotateConstruct();
            }
            else if (deleteMode)
            {
                DeleteConstruct();
            }
        }
    }

    public void MoveMode()
    {
        moveMode = true;
        rotateMode = false;
        deleteMode = false;
    }

    public void RotateMode()
    {
        moveMode = false;
        rotateMode = true;
        deleteMode = false;
    }

    public void DeleteMode()
    {
        moveMode = false;
        rotateMode = false;
        deleteMode = true;
    }

    public void SetSelectConstruct(GameObject construct)
    {
        this.construct = construct;
    }

    public GameObject GetSelectConstruct()
    {
        return this.construct;
    }

    private void RotateConstruct()
    {
        if (construct == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<ConstructControl>() != null
                    && hit.collider.GetComponent<ConstructSlot>().construct.constructType != ConstructType.Ground)
                {
                    construct = hit.collider.gameObject;

                    hit.collider.GetComponent<ConstructControl>().ResetUnderConstructSlot();

                    construct.GetComponent<ConstructControl>().Rotate();
                    construct.GetComponent<ConstructControl>().FindGround();

                    if (construct.GetComponent<ConstructControl>().canPlacement)
                    {
                        construct.GetComponent<ConstructSlot>().UpdateRotation();
                        Placement();
                    }
                    else if (!construct.GetComponent<ConstructControl>().canPlacement)
                    {
                        construct.GetComponent<ConstructControl>().ReverseRotate();
                        construct.GetComponent<ConstructSlot>().UpdateRotation();

                        construct = null;
                        MessageSystem.instance.UpdateMessage("Can't Rotate Object");
                        construct.GetComponent<ConstructControl>().ResetHighlight();
                    }
                }
            }
        }
    }

    private void DeleteConstruct()
    {
        if (construct == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<ConstructControl>() != null
                    && hit.collider.GetComponent<ConstructSlot>().construct.constructType != ConstructType.Ground)
                {
                    hit.collider.GetComponent<ConstructControl>().ResetUnderConstructSlot();
                    construct = hit.collider.gameObject;
                    Destroy(construct);
                    construct = null;
                    MapManager.instance.UpdateMapLayer();
                }
            }
        }
    }

    private void MoveConstruct()
    {
        if (construct != null)
        {

            if (construct.GetComponent<ConstructSlot>().construct.constructType != ConstructType.Ground)
            {
                construct.GetComponent<ConstructControl>().FindGround();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<ConstructSlot>() != null)
                {
                    if (hit.collider.GetComponent<ConstructSlot>().aboveConstruct == null &&
                        hit.collider.GetComponent<ConstructSlot>().construct.constructType == ConstructType.Ground)
                    {
                        ConstructSlot ConstructSlot = hit.collider.GetComponent<ConstructSlot>();
                        construct.GetComponent<ConstructControl>().Move(new Vector2(ConstructSlot.transform.position.x, ConstructSlot.transform.position.z));
                    }


                    if (Input.GetMouseButtonDown(0))
                    {
                        if (construct.GetComponent<ConstructControl>().canPlacement)
                        {
                            construct.GetComponent<ConstructSlot>().UpdateInfomation();
                            Placement();
                        }
                        else
                        {
                            Debug.LogWarning("Can't Placement");
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        CancelPlacement();
                    }

                    MapManager.instance.UpdateMapLayer();
                }

            }
            else if (construct.GetComponent<ConstructSlot>().construct.constructType == ConstructType.Ground)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<ConstructSlot>() != null && (
                    hit.collider.GetComponent<ConstructSlot>().construct.constructType == ConstructType.Ground))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.GetComponent<ConstructSlot>().constructData.constructID = construct.GetComponent<ConstructSlot>().constructData.constructID;
                        hit.collider.GetComponent<MeshRenderer>().material = construct.GetComponent<MeshRenderer>().material;
                        Destroy(construct.gameObject);
                        construct = null;
                    }

                    MapManager.instance.UpdateMapLayer();
                }
            }
        }
        else if (construct == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<ConstructControl>() != null
                    && hit.collider.GetComponent<ConstructSlot>().construct.constructType != ConstructType.Ground)
                {
                    hit.collider.GetComponent<ConstructControl>().ResetUnderConstructSlot();
                    construct = hit.collider.gameObject;
                }
            }
        }
    }

    public void Placement()
    {
        construct.GetComponent<ConstructControl>().Placement();
        construct = null;
    }

    public void CancelPlacement()
    {
        Debug.Log("Cancel Placement");
        Destroy(construct);
        construct = null;
    }

}
