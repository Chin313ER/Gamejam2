using UnityEngine;
using UnityEngine.Events;

public class DoorPlateManager2D : MonoBehaviour
{
    [Header("เชื่อมแป้นเหยียบ (ใส่ 1 อัน หรือ 2 อันที่แยกกัน)")]
    [Tooltip("ลากแป้นเหยียบทั้งหมดที่ต้องใช้เปิดกำแพงชุดนี้มาใส่")]
    public PlateSwitch2D[] connectedPlates;

    [Header("เงื่อนไขการทำงาน")]
    [Tooltip("ติ๊กถูก = ต้องเหยียบค้างไว้ (ถ้าคนใดคนหนึ่งเดินออกจากแป้น กำแพงจะกลับมา) \nไม่ติ๊ก = เมื่อเหยียบครบทุกแป้นพร้อมกันครั้งเดียว กำแพงจะหายไปถาวร")]
    public bool isHoldMode = true;

    [Header("กำแพงหรือประตูที่ต้องการควบคุม")]
    [Tooltip("ลาก GameObject ของกำแพงหรือประตูมาใส่ที่นี่")]
    public GameObject[] targetWalls;

    [Header("Events เสริม (สำหรับเล่นเสียงหรือ Animation)")]
    public UnityEvent onDoorOpened;
    public UnityEvent onDoorClosed;

    private bool isWallHidden = false;

    private void OnEnable()
    {
        // ผูก Event กับแป้นเหยียบทุกอันที่ลากมาใส่
        foreach (PlateSwitch2D plate in connectedPlates)
        {
            if (plate != null)
            {
                plate.OnPlateStateChanged += CheckAllPlates;
            }
        }
    }

    private void OnDisable()
    {
        foreach (PlateSwitch2D plate in connectedPlates)
        {
            if (plate != null)
            {
                plate.OnPlateStateChanged -= CheckAllPlates;
            }
        }
    }

    private void CheckAllPlates()
    {
        // ถ้าเป็นโหมดเหยียบครั้งเดียวเปิดถาวร และเปิดไปแล้ว ไม่ต้องเช็คซ้ำ
        if (!isHoldMode && isWallHidden)
        {
            return;
        }

        // เช็คว่าแป้นเหยียบทุกอันในรายการถูกเหยียบครบทั้งหมดหรือไม่
        bool allPressed = true;
        foreach (PlateSwitch2D plate in connectedPlates)
        {
            if (plate != null && !plate.IsPressed)
            {
                allPressed = false;
                break;
            }
        }

        if (allPressed && !isWallHidden)
        {
            // เหยียบครบทุกแป้น -> ซ่อนกำแพง
            isWallHidden = true;
            SetWallsActive(false);
            onDoorOpened?.Invoke();
        }
        else if (!allPressed && isHoldMode && isWallHidden)
        {
            // มีคนเดินออกจากแป้นใดแป้นหนึ่ง ในโหมดเหยียบค้าง -> นำกำแพงกลับมา
            isWallHidden = false;
            SetWallsActive(true);
            onDoorClosed?.Invoke();
        }
    }

    private void SetWallsActive(bool state)
    {
        foreach (GameObject wall in targetWalls)
        {
            if (wall != null)
            {
                wall.SetActive(state);
            }
        }
    }
}