using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bringBackCursor : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Cursor.visible = true;
    }
}
