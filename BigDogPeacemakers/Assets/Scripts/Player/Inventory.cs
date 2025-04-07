using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public InventoryData inventoryData;
    UIDocument uIDocument;
    VisualElement root;

    void Start()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
    }

    void Update()
    {
        
    }

    public void SetInventory(InventoryData data)
    {
        this.inventoryData = data;
    }
}
