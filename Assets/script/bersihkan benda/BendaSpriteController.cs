using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pasang pada setiap GameObject "benda".
/// Isi spriteAwal dan (opsional) spriteAkhir di Inspector.
/// Script ini:
/// - otomatis menambahkan / mengambil SpriteRenderer,
/// - memastikan spriteAwal tampil saat mulai,
/// - mendaftar ke daftar global agar tombol bisa mempengaruhi semua benda.
/// </summary>
[DisallowMultipleComponent]
public class BendaSpriteController : MonoBehaviour
{
    [Header("Sprite Settings")]
    public Sprite spriteAwal;   // wajib diisi (agar ada gambar awal)
    public Sprite spriteAkhir;  // optional (kalau diisi → ubah, kalau kosong → hide)

    // Global registry untuk semua instance
    internal static HashSet<BendaSpriteController> allControllers = new HashSet<BendaSpriteController>();

    private SpriteRenderer sr;

    void Awake()
    {
        // Ambil SpriteRenderer, kalau belum ada → tambahkan otomatis
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = gameObject.AddComponent<SpriteRenderer>();
        }

        // Set sprite awal saat awake (langsung muncul di layar)
        if (spriteAwal != null)
        {
            sr.sprite = spriteAwal;
            sr.enabled = true;
        }
        else
        {
            // Kalau tidak ada spriteAwal, set null (tidak tampil)
            sr.sprite = null;
            sr.enabled = false;
        }
    }

    void OnEnable()
    {
        allControllers.Add(this);
    }

    void OnDisable()
    {
        allControllers.Remove(this);
    }

    void OnDestroy()
    {
        allControllers.Remove(this);
    }

    /// <summary>
    /// Fungsi per-instance: ubah menjadi spriteAkhir (jika ada) atau sembunyikan.
    /// </summary>
    public void ChangeOrHide()
    {
        if (sr == null) // safety
        {
            sr = GetComponent<SpriteRenderer>();
            if (sr == null) sr = gameObject.AddComponent<SpriteRenderer>();
        }

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

    /// <summary>
    /// Static helper: jalankan ChangeOrHide() ke semua controller terdaftar.
    /// </summary>
    public static void ChangeOrHideAllRegistered()
    {
        // Make a copy to avoid modification-during-iteration issues
        var copy = new BendaSpriteController[allControllers.Count];
        allControllers.CopyTo(copy);

        foreach (var c in copy)
        {
            if (c != null)
                c.ChangeOrHide();
        }
    }
}
