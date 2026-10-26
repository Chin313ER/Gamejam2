using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Data")]
    public List<ItemData> requiredItems = new List<ItemData>(); // ลากไอเทมที่ต้องเก็บมาใส่เรียงตามช่อง
    public List<ItemData> items = new List<ItemData>();         // ไอเทมที่ผู้เล่นเก็บได้แล้ว

    [Header("UI References")]
    public GameObject inventoryPanel;
    public Image[] slots;

    [Header("Slot Color Settings (ปรับสีเงาตรงนี้ได้)")]
    public Color shadowColor = new Color(0f, 0f, 0f, 0.4f); // สีเงาดำลางๆ ตอนยังไม่ได้เก็บ
    public Color normalColor = Color.white;                 // สีปกติตอนเก็บได้แล้ว

    [Header("End Game Settings")]
    public int targetItemCount = 3;
    public string winSceneName; // พิมพ์ชื่อซีนที่ต้องการเปลี่ยนไปเมื่อเก็บของครบ

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
        CheckWinCondition();

        return true;
    }

    private void CheckWinCondition()
    {
        if (items.Count >= targetItemCount)
        {
            Debug.Log("เก็บของครบแล้ว กำลังเปลี่ยนไปซีน: " + winSceneName);

            // คำสั่งเปลี่ยนซีนทันทีเมื่อของครบ
            if (!string.IsNullOrEmpty(winSceneName))
            {
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
                Debug.LogWarning("ยังไม่ได้ตั้งชื่อซีนในช่อง Win Scene Name!");
            }
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < requiredItems.Count && requiredItems[i] != null)
            {
                // แสดงรูปไอเทมประจำช่องนั้น
                slots[i].sprite = requiredItems[i].icon;

                // เช็คว่าผู้เล่นเก็บไอเทมชิ้นนี้มาหรือยัง
                if (items.Contains(requiredItems[i]))
                {
                    slots[i].color = normalColor; // เก็บแล้ว -> แสดงสีชัดปกติ
                }
                else
                {
                    slots[i].color = shadowColor; // ยังไม่เก็บ -> แสดงเป็นเงาลางๆ
                }
            }
            else
            {
                // ถ้าช่องไหนไม่ได้กำหนดไอเทมที่ต้องเก็บไว้ ให้ทำเป็นช่องว่างจางๆ
                slots[i].sprite = null;
                slots[i].color = new Color(1f, 1f, 1f, 0.1f);
            }
        }
    }
}