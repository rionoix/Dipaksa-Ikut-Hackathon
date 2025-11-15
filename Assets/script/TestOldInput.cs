using UnityEngine;

public class TestOldInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Debug.Log("OLD INPUT: E ditekan (Input.GetKeyDown)");
    }
}
