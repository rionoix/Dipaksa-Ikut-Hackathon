using UnityEngine;

[DisallowMultipleComponent]
public class BendaSpriteController : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite spriteAwal;   
    public Sprite spriteAkhir;  

    private SpriteRenderer sr;

    void Awake()
    {
        // Ambil atau buat SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = gameObject.AddComponent<SpriteRenderer>();

        // Tampilkan sprite awal
        if (spriteAwal != null)
        {
            sr.sprite = spriteAwal;
            sr.enabled = true;
        }
        else
        {
            sr.enabled = false;
        }
    }

    public void ChangeOrHide()
    {
        if (spriteAkhir != null)
        {
            sr.sprite = spriteAkhir;
            sr.enabled = true;
        }
        else
        {
            sr.sprite = null;
            sr.enabled = false;
        }
    }
}
