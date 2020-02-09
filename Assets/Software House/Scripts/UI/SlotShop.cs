using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotShop : MonoBehaviour
{
    public Construct construct;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            MapManager.instance.Test_BuyConstruct(construct);
            MapManager.instance.MapEditorMoveMode();
        });

    }

    public void Set(Construct construct)
    {
        this.construct = construct;
        GetComponent<Image>().sprite = construct.icon;
    }
}