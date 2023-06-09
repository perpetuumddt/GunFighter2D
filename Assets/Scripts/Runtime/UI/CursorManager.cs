using UnityEngine;

namespace Gunfighter.Runtime.UI
{
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
}
