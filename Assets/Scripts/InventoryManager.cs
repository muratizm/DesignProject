using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private GameManager gameManager;
    private GameObject prefabToLoad;

    
    private static int crystal;
    public static int Crystal { get { return crystal; } private set { crystal = value; } }


    [Header("Inventory")]
    [SerializeField] private Item[] inventory = new Item[5];
    [SerializeField] private Image[] inventorySlots; // Assign in the Inspector
    [SerializeField] private GameObject itemUsingPanel; // Assign in the Inspector
    private int selectedSlot = -1;

    [Header("Crystal")]
    [SerializeField] private TextMeshProUGUI crystalText; // Assign in the Inspector


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if(gameManager.IsGamePaused){return;} // if game is paused, dont do anything

        if (selectedSlot != -1) // If an item is selected
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnCloseButton();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                OnUseButton();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                OnDropButton();
            }
        }
        else // If no item is selected
        { 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HandleInventoryAction(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HandleInventoryAction(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HandleInventoryAction(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HandleInventoryAction(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                HandleInventoryAction(4);
            }
        }
    }


    public void AddItem(Item item) 
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                UpdateInventorySlot(i);
                return;
            }
        }
    }


    public void AddCrystal() 
    {
        Crystal++;
        UpdateCrystalSlot();
        Debug.Log("we found Crystal, Count: " + Crystal);
    }



    void HandleInventoryAction(int slotIndex) // called when the player presses a number key to select an inventory slot
    {
        if (inventory[slotIndex] != null)
        {
            itemUsingPanel.SetActive(true);
            selectedSlot = slotIndex;
        }
    }

    

    public void OnUseButton() // called when the player presses the "Use" button inside of ItemUsingPanel
    {
        inventory[selectedSlot].Use();
        if (inventory[selectedSlot].IsConsumable)
        {
            inventory[selectedSlot] = null;
            UpdateInventorySlot(selectedSlot);
        }
        OnCloseButton();
    }

    public void OnDropButton() // called when the player presses the "Drop" button inside of ItemUsingPanel
    {
        StartCoroutine(DropButtonCoroutine());
    }

    
    public void OnCloseButton() // called when the player presses the "X" button inside of ItemUsingPanel
    {
        itemUsingPanel.SetActive(false);
        selectedSlot = -1;
    }

    private IEnumerator DropButtonCoroutine() // responsible for dropping the selected item
    {
        Debug.Log("Dropping " + inventory[selectedSlot].ItemName);

        yield return StartCoroutine(CreateItemDrop()); // wait for the item to be created before removing it from the inventory

        inventory[selectedSlot] = null;
        UpdateInventorySlot(selectedSlot);
        OnCloseButton();
    }

    IEnumerator CreateItemDrop()
    {
        yield return LoadPrefabOfItem(inventory[selectedSlot].itemTag);  // Wait for loading
        GameObject itemDrop = Instantiate(prefabToLoad, Player.Instance.transform.position + Player.Instance.transform.forward * 2.0f, Quaternion.identity);
        itemDrop.GetComponent<ItemObject>().Item = inventory[selectedSlot];
    }
    



    void UpdateInventorySlot(int index) // responsible for updating the specific inventory slot
    {
        inventorySlots[index].sprite = (inventory[index] != null) ? inventory[index].ItemIcon : null;
    }


    void UpdateCrystalSlot() // responsible for updating the crystal count
    {
        // Update the UI to show the new crystal count
        crystalText.text = Crystal.ToString();
    }

    void UpdateAllInventory() // responsible for updating all the inventory slots and the crystal count
    {
        UpdateCrystalSlot();
        for (int i = 0; i < inventory.Length; i++)
        {
            UpdateInventorySlot(i);
        }
    }

    public void SaveInventory() // responsible for saving the inventory data to the PlayerPrefs
    {
        Debug.Log("Inventory Saved");
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                PlayerPrefs.SetString("InventorySlot" + i, inventory[i].name); // Save the name of the scriptable object
            }
            else
            {
                PlayerPrefs.SetString("InventorySlot" + i, "");
            }
        }
        PlayerPrefs.SetInt("Crystal", Crystal);
        Debug.Log("Crystal123: " + Crystal);
    }

    public void LoadInventory() // responsible for loading the inventory data from the PlayerPrefs
    {
        Debug.Log("Inventory Loaded");
        for (int i = 0; i < inventory.Length; i++)
        {
            string itemName = PlayerPrefs.GetString("InventorySlot" + i);
            if (itemName != "")
            {
                inventory[i] = Resources.Load<Item>(Constants.Paths.RESOURCES_SCRIPTIBLEOBJECTS_ITEMS_FOLDER + itemName); // Load the scriptable object with its name
                UpdateInventorySlot(i);
            }
            else
            {
                inventory[i] = null;
            }
        }
        Crystal = PlayerPrefs.GetInt("Crystal");
        UpdateAllInventory();

    }
    IEnumerator LoadPrefabOfItem(string itemTag)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(itemTag);
        yield return handle; // Wait for loading to complete

        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Loaded: " + handle.Result.name);
            prefabToLoad = handle.Result;
        }
        else
        {
            Debug.LogError("Failed to load prefab");
        }
    }

}
