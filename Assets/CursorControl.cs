using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public static void EnableCursor()
    {
        Cursor.visible = true;
    }

    private void Start()
    {
        Cursor.visible = false;
    }
}
