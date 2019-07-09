using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    private Vector2 hotspot = Vector2.zero;

    public static bool visible { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
       // Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
}
