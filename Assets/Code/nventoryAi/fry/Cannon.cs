using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("ตั้งค่าปืนใหญ่")]
    public GameObject bulletPrefab; // ช่องใส่ Prefab ลูกปืนใหญ่
    public Transform firePoint;     // จุดปล่อยกระสุน (ถ้าไม่ใส่ กระสุนจะออกจากกลางตัวปืน)
    public float fireRate = 2f;     // ยิงออกมากี่วินาทีต่อ 1 ลูก (ค่ายิ่งน้อยยิ่งยิงรัว)

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // เมื่อครบเวลาที่กำหนด ให้ยิงกระสุนออกไปแล้วนับเวลาใหม่วนไปเรื่อยๆ
        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null) return;

        // ถ้ามีการกำหนดจุดปากกระบอกปืน (firePoint) ให้เกิดตรงนั้น ถ้าไม่มีให้เกิดที่ตัวปืนเลย
        Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
    }
}