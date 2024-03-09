using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private GameManager gameManager;

    [SerializeField]
    private Item[] inventory = new Item[5];
    [SerializeField] private Image[] inventorySlots; // Assign in the Inspector
    [SerializeField] private GameObject itemUsingPanel; // Assign in the Inspector
    private int selectedSlot = -1;


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
        UpdateInventorySlots();
    }

    void Update()
    {
        if(gameManager.IsGamePaused){return;} //if game is paused, dont do anything

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

    void UpdateInventorySlots()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            UpdateInventorySlot(i);
        }
    }
}
