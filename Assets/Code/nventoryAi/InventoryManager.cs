using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Data")]
    public List<ItemData> items = new List<ItemData>();

    [Header("UI References")]
    public GameObject inventoryPanel; // ลากตัว "inventory" มาใส่
    public Image[] slots;             // ลาก Image, Image (1), Image (2), Image (3) มาใส่

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public bool AddItem(ItemData newItem)
    {
        if (items.Count >= slots.Length)
        {
            Debug.Log("กระเป๋าเต็มแล้ว!");
            return false;
        }

        items.Add(newItem);
        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                // ถ้ามีไอเทมในช่องนี้ ให้แสดงรูปไอเทม
                slots[i].sprite = items[i].icon;
                slots[i].color = Color.white; // ทำให้มองเห็นรูปชัดเจน
            }
            else
            {
                // ถ้าช่องว่าง ให้เคลียร์รูปออก
                slots[i].sprite = null;
                slots[i].color = new Color(1, 1, 1, 0.2f); // ทำให้ช่องดูจางลงเวลาไม่มีของ
            }
        }
    }
}