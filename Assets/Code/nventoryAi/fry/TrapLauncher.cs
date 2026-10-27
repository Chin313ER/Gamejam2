using UnityEngine;

public class TrapLauncher : MonoBehaviour
{
    [Header("ตั้งค่าจุดปล่อย")]
    public GameObject projectilePrefab; // ช่องใส่ Prefab ของวัตถุที่จะปล่อย
    public float fireInterval = 2f;     // ปล่อยออกมากี่วินาทีต่อ 1 ครั้ง
    public float detectRange = 10f;     // ระยะตรวจจับผู้เล่น (เข้าใกล้ในระยะนี้ถึงจะเริ่มยิง)

    private float timer;
    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (projectilePrefab == null) return;

        // เช็คว่าผู้เล่นอยู่ในระยะยิงหรือยัง
        if (playerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance > detectRange) return; // ถ้าผู้เล่นยังอยู่ไกลเกินไป จะยังไม่ยิง
        }

        // นับเวลาถอยหลังเพื่อปล่อยวัตถุ
        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            timer = 0f;
        }
    }

    // แสดงวงกลมสีแดงในหน้า Scene เพื่อดูระยะตรวจจับได้ง่ายขึ้น
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}