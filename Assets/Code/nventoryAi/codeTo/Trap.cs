using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("อ้างอิงสคริปต์ (ลาก Object ที่มี SanityManager มาใส่ได้เลย)")]
    public SanityManager sanityManager;

    [Header("ตั้งค่ากับดัก")]
    public float sanityDamage = 25f;

    void Start()
    {
        // ถ้าไม่ได้ลากใส่ในช่อง Inspector ให้ดึงจาก instance หรือค้นหาในฉากอัตโนมัติ
        if (sanityManager == null)
        {
            sanityManager = SanityManager.instance;
        }

        if (sanityManager == null)
        {
            sanityManager = Object.FindAnyObjectByType<SanityManager>();
        }
    }

    // กรณีที่ 1: กับดักติ๊กถูกที่ช่อง Is Trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyTrapDamage();
        }
    }

    // กรณีที่ 2: กับดักไม่ได้ติ๊กช่อง Is Trigger (เป็นวัตถุแข็งเดินชน)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyTrapDamage();
        }
    }

    private void ApplyTrapDamage()
    {
        if (sanityManager == null)
        {
            sanityManager = SanityManager.instance;
        }

        if (sanityManager != null)
        {
            sanityManager.TakeDamage(sanityDamage);
        }
        else
        {
            Debug.LogWarning("หา SanityManager ในฉากไม่เจอ! กรุณาลากใส่ในช่อง Sanity Manager ของกับดัก");
        }
    }
}