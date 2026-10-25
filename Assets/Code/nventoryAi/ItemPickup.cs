using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public KeyCode pickupKey = KeyCode.E; // ปุ่มที่ใช้กดเก็บ (ค่าเริ่มต้นคือปุ่ม E)
    public GameObject pickupPrompt;       // (ไม่บังคับ) สำหรับใส่รูปปุ่มหรือข้อความแจ้งเตือนบนหัวไอเทม

    private bool isPlayerInRange = false;

    private void Start()
    {
        // ซ่อนป้ายบอกปุ่มกดไว้ก่อนตอนเริ่มเกม
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        // ถ้าผู้เล่นอยู่ในระยะของไอเทม และกดปุ่ม E
        if (isPlayerInRange && Input.GetKeyDown(pickupKey))
        {
            bool wasPickedUp = InventoryManager.Instance.AddItem(itemData);
            if (wasPickedUp)
            {
                Destroy(gameObject); // ลบไอเทมออกจากฉากเมื่อเก็บสำเร็จ
            }
        }
    }

    // เมื่อผู้เล่นเดินเข้ามาในระยะชนของไอเทม
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (pickupPrompt != null)
            {
                pickupPrompt.SetActive(true); // แสดงป้ายบอกปุ่มกด
            }
        }
    }

    // เมื่อผู้เล่นเดินออกจากระยะของไอเทม
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (pickupPrompt != null)
            {
                pickupPrompt.SetActive(false); // ซ่อนป้ายบอกปุ่มกด
            }
        }
    }
}