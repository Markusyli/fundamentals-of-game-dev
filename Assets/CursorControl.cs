using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public static void EnableCursor()
    {
        Cursor.visible = true;
    }
    public static void DisableCursor()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        Cursor.visible = false;
    }
}
