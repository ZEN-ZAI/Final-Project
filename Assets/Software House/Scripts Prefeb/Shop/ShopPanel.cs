using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
	#region Singleton
	public static ShopPanel instance;

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

    public Button moveButton;
    public Button rotateButton;
    public Button DeleteButton;

    public Button groundButton;
    public Button wallButton;
    public Button furnitureButton;
    public Button relaxButton;
    public Button workButton;

    public Transform groundContent;
    public Transform wallContent;
	public Transform furnitureContent;
	public Transform relaxContent;
    public Transform workContent;

    public Transform groundPanel;
    public Transform wallPanel;
    public Transform furniturePanel;
    public Transform relaxPanel;
    public Transform workPanel;

    public GameObject slotShop;

    // Start is called before the first frame update
    void Start()
    {
        RefreshSlotShop();

        CloseAllPanel();
        groundPanel.gameObject.SetActive(true);

        groundButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            groundPanel.gameObject.SetActive(true);
        });

        wallButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            wallPanel.gameObject.SetActive(true);
        });

        furnitureButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            furniturePanel.gameObject.SetActive(true);
        });

        relaxButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            relaxPanel.gameObject.SetActive(true);
        });

        workButton.onClick.AddListener(() =>
        {
            CloseAllPanel();
            workPanel.gameObject.SetActive(true);
        });

        moveButton.onClick.AddListener(() =>
        {
            MapManager.instance.MapEditorMoveMode();
        });

        rotateButton.onClick.AddListener(() =>
        {
            MapManager.instance.MapEditorRotateMode();
        });

        DeleteButton.onClick.AddListener(() =>
        {
            MapManager.instance.MapEditorDeleteMode();
        });
    }

    private void CloseAllPanel()
    {
        groundPanel.gameObject.SetActive(false);
        wallPanel.gameObject.SetActive(false);
        furniturePanel.gameObject.SetActive(false);
        relaxPanel.gameObject.SetActive(false);
        workPanel.gameObject.SetActive(false);
    }

    public void RefreshSlotShop()
    {
        RemoveAllSlotShop(furnitureContent);
        RemoveAllSlotShop(groundContent);
        RemoveAllSlotShop(relaxContent);
        RemoveAllSlotShop(wallContent);
        RemoveAllSlotShop(workContent);

        AddSlotShop(ConstructAsset.instance.GetFurnitureAsset(), furnitureContent);
        AddSlotShop(ConstructAsset.instance.GetGroundAsset(), groundContent);
        AddSlotShop(ConstructAsset.instance.GetRelaxAsset(), relaxContent);
        AddSlotShop(ConstructAsset.instance.GetWallAsset(), wallContent);
        AddSlotShop(ConstructAsset.instance.GetWorkAsset(), workContent);
    }

    private void AddSlotShop(List<Construct> constructs,Transform content)
    {
        for (int i = 0; i < constructs.Count; i++)
        {
            Construct construct = constructs[i];
            SlotShop slotShop_temp = Instantiate(slotShop, content).GetComponent<SlotShop>();

            slotShop_temp.Set(construct);
        }
    }

    private void RemoveAllSlotShop(Transform content)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
}
