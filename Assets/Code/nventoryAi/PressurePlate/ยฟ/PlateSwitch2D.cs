using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlateSwitch2D : MonoBehaviour
{
    [Header("การตั้งค่าแป้นเหยียบเดี่ยว")]
    [Tooltip("Tag ของตัวละครผู้เล่น")]
    public string playerTag = "Player";

    [Tooltip("ติ๊กถูก = เหยียบแป้นนี้ครั้งเดียวแล้วแป้นค้างให้เลย (ไม่ต้องยืนเฝ้าแป้นนี้)")]
    public bool lockIndividualPlate = false;

    [Header("สีของแป้นเหยียบ (Visual)")]
    public SpriteRenderer plateRenderer;
    public Color normalColor = Color.white;
    public Color pressedColor = Color.green;

    // ส่งสัญญาณบอกตัวควบคุมกำแพงเมื่อมีคนเหยียบหรือเดินออก
    public event Action OnPlateStateChanged;

    private HashSet<GameObject> playersOnPlate = new HashSet<GameObject>();
    private bool isPressed = false;

    // ให้สคริปต์จัดการกำแพงดึงค่าไปเช็คว่าแป้นนี้ถูกเหยียบอยู่หรือไม่
    public bool IsPressed => isPressed;

    private void Awake()
    {
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
            playersOnPlate.Add(other.gameObject);
            EvaluateState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playersOnPlate.Remove(other.gameObject);
            EvaluateState();
        }
    }

    private void EvaluateState()
    {
        // ถ้าตั้งให้แป้นนี้ล็อกตัวเองเมื่อเหยียบไปแล้ว ไม่ต้องยกเลิกสถานะ
        if (lockIndividualPlate && isPressed)
        {
            return;
        }

        bool shouldBePressed = playersOnPlate.Count > 0;

        if (shouldBePressed != isPressed)
        {
            isPressed = shouldBePressed;
            UpdateVisuals();
            OnPlateStateChanged?.Invoke(); // แจ้งเตือนไปยัง DoorPlateManager2D
        }
    }

    private void UpdateVisuals()
    {
        if (plateRenderer != null)
        {
            plateRenderer.color = isPressed ? pressedColor : normalColor;
        }
    }
}