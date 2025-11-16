using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pasang script ini pada 1 GameObject (mis. "GameManager" atau empty GameObject).
/// Sambungkan fungsi ChangeOrHideAll() ke Button UI OnClick.
/// </summary>
public class BendaManager : MonoBehaviour
{
    // Fungsi ini akan memanggil perubahan pada semua benda yang terdaftar.
    public void ChangeOrHideAll()
    {
        // Jika ada controller yang sudah terdaftar, pakai itu.
        if (BendaSpriteController.allControllers != null && BendaSpriteController.allControllers.Count > 0)
        {
            BendaSpriteController.ChangeOrHideAllRegistered();
            return;
        }

        // Fallback: cari semua BendaSpriteController di scene (termasuk inactive jika Unity mendukung overload)
#if UNITY_2020_1_OR_NEWER
        var found = FindObjectsOfType<BendaSpriteController>(true);
#else
        var found = FindObjectsOfType<BendaSpriteController>();
#endif
        foreach (var c in found)
        {
            if (c != null) c.ChangeOrHide();
        }
    }

    // Optional: method untuk tombol yang hanya mengubah satu tipe (tidak dipakai default)
    public void ChangeOrHideAllButOnlyIfHasNewSprite()
    {
        // contoh variasi jika mau logika berbeda
        ChangeOrHideAll();
    }
}
