using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityManager : MonoBehaviour
{
    // ตัวแปร static เพื่อให้กับดักเรียกหาได้ทันทีแม้ไม่ได้อยู่ในตัว Player
    public static SanityManager instance;

    [Header("UI หลอดพลัง (เลือกใส่ตามที่ใช้จริง)")]
    public Slider sanitySlider; // ใส่ถ้าใช้ UI Slider
    public Image sanityImage;   // ใส่ถ้าใช้ UI Image (แบบ Filled)

    [Header("ตั้งค่าพลังสติ (Sanity)")]
    public float sanity = 100f;
    public float maxSanity = 100f;
    public float recoverSpeed = 10f;
    public bool shadowActive = false;

    [Header("ตั้งค่าการรับดาเมจจากกับดัก")]
    public float damageCooldown = 1f;
    private float lastDamageTime = -99f;

    [Header("ตั้งค่าเมื่อพลังหมด")]
    public bool resetSceneOnDeath = true; // ติ๊กถูก = รีเซ็ตซีนเดิม, เอาติ๊กออก = ไปหน้า GameOver

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // ถ้าลืมลากใส่ใน Inspector แต่แปะสคริปต์นี้ไว้ที่ตัว Slider หรือ Image โดยตรง ให้ดึงค่าเองอัตโนมัติ
        if (sanitySlider == null) sanitySlider = GetComponent<Slider>();
        if (sanityImage == null) sanityImage = GetComponent<Image>();

        if (sanitySlider != null)
        {
            sanitySlider.maxValue = maxSanity;
        }

        UpdateSanityUI();
    }

    void Update()
    {
        if (!shadowActive && sanity < maxSanity)
        {
            sanity += recoverSpeed * Time.deltaTime;
            sanity = Mathf.Clamp(sanity, 0f, maxSanity);
        }

        UpdateSanityUI();

        // เช็คเมื่อค่า Sanity หมด
        if (sanity <= 0f)
        {
            if (resetSceneOnDeath)
            {
                // รีเซ็ตซีนปัจจุบัน
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                // หรือโหลดไปหน้า GameOver
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    // ฟังก์ชันอัปเดตหลอด UI ทั้งแบบ Slider และ Image
    void UpdateSanityUI()
    {
        if (sanitySlider != null)
        {
            sanitySlider.value = sanity;
        }

        if (sanityImage != null)
        {
            sanityImage.fillAmount = sanity / maxSanity;
        }
    }

    // ฟังก์ชันสำหรับให้สคริปต์ Trap เรียกใช้เพื่อลดค่า Sanity
    public bool TakeDamage(float damageAmount)
    {
        if (Time.time < lastDamageTime + damageCooldown)
        {
            return false;
        }

        lastDamageTime = Time.time;
        sanity -= damageAmount;
        sanity = Mathf.Clamp(sanity, 0f, maxSanity);

        UpdateSanityUI();
        Debug.Log("โดนกับดัก! Sanity เหลือ: " + sanity);
        if (sanity <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        return true;
    }

    public void UseShadow()
    {
        sanity -= 20f;
        sanity = Mathf.Clamp(sanity, 0f, maxSanity);
        shadowActive = true;
        UpdateSanityUI();
    }

    public void ReturnShadow()
    {
        shadowActive = false;
    }

    // ฟังก์ชันเสริมเผื่อนำไปผูกกับปุ่มกดรีเซ็ตด่าน (UI Button)
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}