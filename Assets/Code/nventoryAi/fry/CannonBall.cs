using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [Header("ตั้งค่าลูกปืนใหญ่")]
    public float speed = 7f;                        // ความเร็วของลูกปืน
    public Vector2 direction = new Vector2(-1, 0);  // ทิศทาง (-1,0 = ไปทางซ้าย | 1,0 = ไปทางขวา | 0,-1 = พุ่งลง)
    public float sanityDamage = 20f;                // จำนวน Sanity ที่ลดเมื่อโดน
    public float lifeTime = 5f;                     // เวลาทำลายตัวเองเมื่อลอยพ้นฉาก (วินาที)

    void Start()
    {
        // ลบลูกปืนทิ้งอัตโนมัติเมื่อครบกำหนดเวลา เพื่อไม่ให้ค้างในฉากเยอะเกินไป
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // เคลื่อนที่พุ่งตรงไปตามทิศทางที่ตั้งไว้
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ถ้าชนผู้เล่น ให้ลดค่า Sanity แล้วทำลายลูกปืนทิ้ง
        if (other.CompareTag("Player"))
        {
            if (SanityManager.instance != null)
            {
                SanityManager.instance.TakeDamage(sanityDamage);
            }

            Destroy(gameObject);
        }
    }
}