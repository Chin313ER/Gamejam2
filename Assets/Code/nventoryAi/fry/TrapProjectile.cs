using UnityEngine;

public class TrapProjectile : MonoBehaviour
{
    [Header("ตั้งค่าการพุ่งและดาเมจ")]
    public float speed = 8f;            // ความเร็วในการพุ่ง
    public float sanityDamage = 20f;    // ดาเมจที่ลดค่า Sanity
    public float lifeTime = 4f;         // เวลาทำลายตัวเองอัตโนมัติ (วินาที)
    public bool aimAtPlayer = true;     // ติ๊กถูก = เล็งพุ่งไปหาผู้เล่น, ไม่ติ๊ก = พุ่งตรงไปข้างหน้า

    private Vector2 moveDirection;

    void Start()
    {
        // ลบวัตถุทิ้งเมื่อครบเวลา เพื่อไม่ให้ค้างในฉากเยอะเกินไป
        Destroy(gameObject, lifeTime);

        if (aimAtPlayer)
        {
            // ค้นหาตำแหน่งของ Player แล้วคำนวณทิศทางพุ่งเข้าหา
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                moveDirection = (player.transform.position - transform.position).normalized;
            }
            else
            {
                moveDirection = Vector2.left; // ถ้าหา Player ไม่เจอ ให้พุ่งไปทางซ้ายเป็นค่าเริ่มต้น
            }
        }
        else
        {
            // พุ่งตรงไปตามทิศทางของจุดปล่อย
            moveDirection = transform.right;
        }
    }

    void Update()
    {
        // สั่งให้วัตถุเคลื่อนที่พุ่งไปตามทิศทางที่คำนวณไว้
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // เช็คว่าชนผู้เล่นหรือไม่
        if (other.CompareTag("Player"))
        {
            if (SanityManager.instance != null)
            {
                SanityManager.instance.TakeDamage(sanityDamage);
            }

            // ชนผู้เล่นเสร็จแล้วให้ทำลายวัตถุนี้ทิ้งทันที
            Destroy(gameObject);
        }
    }
}