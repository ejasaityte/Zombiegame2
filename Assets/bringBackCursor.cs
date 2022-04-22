using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bringBackCursor : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        //brings back cursor after splash screen
        Cursor.visible = true;
    }
}
