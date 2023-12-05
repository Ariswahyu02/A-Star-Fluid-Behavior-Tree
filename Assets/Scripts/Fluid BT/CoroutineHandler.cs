using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    public static CoroutineHandler instance;
    private void Awake()
    {
        instance = this;
    }

    public static Coroutine StartCoroutineVariable(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }
    public static void StopCoroutineVariable(IEnumerator coroutine)
    {
        instance.StopCoroutine(coroutine);
    }

    public static Coroutine StartCoroutineString(string coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }

    public static void StopCoroutineString(string coroutine)
    {
        instance.StopCoroutine(coroutine);
    }
}
