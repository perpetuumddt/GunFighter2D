using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    public Texture2D texture;
    public CursorMode cursorMode = CursorMode.Auto;
    void Start()
    {
        Cursor.SetCursor(texture, new Vector2(32/2,32/2), cursorMode);
    }


}
