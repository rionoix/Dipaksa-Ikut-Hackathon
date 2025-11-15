using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public Sprite speakerSprite;
    [TextArea(3, 10)]
    public string text;
    public bool isPlayer; // jika true dialog muncul di kanan
}
