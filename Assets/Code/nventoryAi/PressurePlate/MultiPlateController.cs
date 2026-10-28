using UnityEngine;

public class MultiPlateController : MonoBehaviour
{
    [Tooltip("ลากแป้นเหยียบทั้งหมดที่ต้องเหยียบพร้อมกันมาใส่ที่นี่")]
    public PressurePlate2D[] requiredPlates;

    [Tooltip("กำแพงหรือประตูที่จะหายไปเมื่อทุกแป้นถูกเหยียบครบ")]
    public GameObject[] targetWalls;

    [Tooltip("ติ๊กถูก = ต้องเหยียบค้างทุกแป้น | ไม่ติ๊ก = เหยียบพร้อมกันครั้งเดียวเปิดถาวร")]
    public bool isHoldMode = true;

    private bool isUnlocked = false;

    private void Update()
    {
        if (!isHoldMode && isUnlocked) return;

        bool allActive = true;
        foreach (PressurePlate2D plate in requiredPlates)
        {
            if (plate != null && !plate.IsActivated)
            {
                allActive = false;
                break;
            }
        }

        if (allActive && !isUnlocked)
        {
            isUnlocked = true;
            SetWallsActive(false); // ซ่อนกำแพง
        }
        else if (!allActive && isHoldMode && isUnlocked)
        {
            isUnlocked = false;
            SetWallsActive(true); // นำกำแพงกลับมา
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