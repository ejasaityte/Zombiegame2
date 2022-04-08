using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashScreenCursor : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Cursor.visible = false;
    }
}
