using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private GameManager gameManager;

    
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
        UpdateAllInventory();
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



    void HandleInventoryAction(int slotIndex) 
    {
        if (inventory[slotIndex] != null)
        {
            itemUsingPanel.SetActive(true);
            selectedSlot = slotIndex;
        }
    }

    public void OnUseButton() 
    {
        inventory[selectedSlot].Use();
        if (inventory[selectedSlot].IsConsumable)
        {
            inventory[selectedSlot] = null;
            UpdateInventorySlot(selectedSlot);
        }
        OnCloseButton();
    }

    public void OnDropButton()
    {
        Debug.Log("Dropping " + inventory[selectedSlot].ItemName);
        inventory[selectedSlot] = null;
        UpdateInventorySlot(selectedSlot);
        OnCloseButton();
    }

    public void OnCloseButton()
    {
        itemUsingPanel.SetActive(false);
        selectedSlot = -1;
    }


    void UpdateInventorySlot(int index)
    {
        inventorySlots[index].sprite = (inventory[index] != null) ? inventory[index].ItemIcon : null;
    }

    void UpdateCrystalSlot()
    {
        // Update the UI to show the new crystal count
        crystalText.text = Crystal.ToString();
    }

    void UpdateAllInventory()
    {
        UpdateCrystalSlot();
        for (int i = 0; i < inventory.Length; i++)
        {
            UpdateInventorySlot(i);
        }
    }

    public void SaveInventory()
    {
        Debug.Log("Inventory Saved");
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                PlayerPrefs.SetString("InventorySlot" + i, inventory[i].ItemName);
            }
            else
            {
                PlayerPrefs.SetString("InventorySlot" + i, "");
            }
        }
        PlayerPrefs.SetInt("Crystal", Crystal);
        Debug.Log("Crystal123: " + Crystal);
    }

    public void LoadInventory()
    {
        Debug.Log("Inventory Loaded");
        for (int i = 0; i < inventory.Length; i++)
        {
            string itemName = PlayerPrefs.GetString("InventorySlot" + i);
            if (itemName != "")
            {
                inventory[i] = Resources.Load<Item>("Items/" + itemName);
            }
            else
            {
                inventory[i] = null;
            }
        }
        Crystal = PlayerPrefs.GetInt("Crystal");
        Debug.Log("Crystal123: " + Crystal);
        UpdateAllInventory();
    }
}
