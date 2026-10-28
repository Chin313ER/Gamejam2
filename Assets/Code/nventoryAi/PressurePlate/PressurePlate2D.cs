using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate2D : MonoBehaviour
{
    [Header("เงื่อนไขการทำงาน (Plate Settings)")]
    [Tooltip("จำนวนผู้เล่นที่ต้องยืนบนแป้นพร้อมกัน (เช่น 1 หรือ 2 คน)")]
    [Min(1)]
    public int requiredPlayers = 1;

    [Tooltip("ติ๊กถูก = ต้องเหยียบค้าง (เดินออกกำแพงกลับมา) | ไม่ติ๊ก = เหยียบครั้งเดียวเปิดถาวร")]
    public bool isHoldMode = true;

    [Tooltip("Tag ของตัวละครที่สามารถเหยียบแป้นได้")]
    public string playerTag = "Player";

    [Header("เป้าหมาย (Target Doors / Walls)")]
    [Tooltip("ใส่ GameObject ของกำแพงหรือประตูที่ต้องการให้หายไปเมื่อเหยียบครบ")]
    public GameObject[] targetObjects;

    [Header("การแสดงผลของแป้นเหยียบ (Visual Feedback)")]
    public SpriteRenderer plateRenderer;
    public Color normalColor = Color.white;
    public Color activeColor = Color.green;

    [Header("Events เพิ่มเติม (Optional)")]
    public UnityEvent onPlateActivated;
    public UnityEvent onPlateDeactivated;

    // เก็บรายชื่อผู้เล่นที่กำลังเหยียบอยู่ ป้องกันการนับซ้ำหากตัวละครมีหลาย Collider
    private HashSet<GameObject> playersOnPlate = new HashSet<GameObject>();
    private bool isActivated = false;

    private void Awake()
    {
        // บังคับให้ Collider ของแป้นเป็น Trigger เสมอ
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        if (plateRenderer == null)
        {
            plateRenderer = GetComponent<SpriteRenderer>();
        }

        UpdateVisuals();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // เพิ่ม GameObject หลักของผู้เล่นเข้าไปในรายการ
            playersOnPlate.Add(other.gameObject);
            CheckPlateState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // ลบผู้เล่นออกจากรายการเมื่อเดินออกจากแป้น
            playersOnPlate.Remove(other.gameObject);
            CheckPlateState();
        }
    }

    private void CheckPlateState()
    {
        // กรณีเป็นโหมดเหยียบครั้งเดียว (One-Shot) และเคยทำงานไปแล้ว ไม่ต้องเช็คซ้ำ
        if (!isHoldMode && isActivated)
        {
            return;
        }

        // ตรวจสอบว่าจำนวนคนบนแป้นครบตามที่กำหนดหรือไม่
        if (playersOnPlate.Count >= requiredPlayers)
        {
            if (!isActivated)
            {
                ActivatePlate();
            }
        }
        else
        {
            // ถ้าคนไม่ครบ และเป็นโหมดเหยียบค้าง ให้คืนค่ากำแพงกลับมา
            if (isHoldMode && isActivated)
            {
                DeactivatePlate();
            }
        }
    }

    private void ActivatePlate()
    {
        isActivated = true;
        SetTargetsActive(false); // ทำให้กำแพงหรือประตูหายไป
        UpdateVisuals();
        onPlateActivated?.Invoke();
    }

    private void DeactivatePlate()
    {
        isActivated = false;
        SetTargetsActive(true); // ทำให้กำแพงหรือประตูกลับมาเมื่อไม่มีคนเหยียบ
        UpdateVisuals();
        onPlateDeactivated?.Invoke();
    }

    private void SetTargetsActive(bool state)
    {
        foreach (GameObject target in targetObjects)
        {
            if (target != null)
            {
                target.SetActive(state);
            }
        }
    }

    private void UpdateVisuals()
    {
        if (plateRenderer != null)
        {
            plateRenderer.color = isActivated ? activeColor : normalColor;
        }
    }

    // ใช้สำหรับอ่านสถานะจากสคริปต์อื่น (เช่น กรณีต้องเหยียบแยกกันหลายแป้น)
    public bool IsActivated => isActivated;
}