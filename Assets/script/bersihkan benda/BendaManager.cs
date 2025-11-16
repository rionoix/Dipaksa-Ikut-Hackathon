using UnityEngine;

public class BendaManager : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;         // Drag Player di inspector
    public float jarakMaksimum = 5f; // Radius interaksi

    public void ChangeNearest()
    {
        BendaSpriteController[] allBenda = FindObjectsOfType<BendaSpriteController>();
        BendaSpriteController nearest = null;
        float nearestDist = Mathf.Infinity;

        foreach (var benda in allBenda)
        {
            float dist = Vector2.Distance(player.position, benda.transform.position);
            if (dist <= jarakMaksimum && dist < nearestDist)
            {
                nearestDist = dist;
                nearest = benda;
            }
        }

        if (nearest != null)
        {
            nearest.ChangeOrHide();
        }
        else
        {
            Debug.Log("Tidak ada benda di dekat player saat ini.");
        }
    }
}
