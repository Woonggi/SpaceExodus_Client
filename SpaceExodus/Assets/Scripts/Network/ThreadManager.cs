using System;
using System.Collections.Generic;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    private static readonly List<Action> mainThread = new List<Action>();
    private static readonly List<Action> copiedMainThread = new List<Action>();
    private static bool actionToExecuteOnMainThread = false;

    // Update is called once per frame
    void Update()
    {
        UpdateMain();
    }
    public static void ExecuteOnMainThread(Action action)
    {
        if (action == null)
        {
            Debug.Log("No action to execute on main thread!");
            return;
        }
        lock (mainThread)
        {
            mainThread.Add(action);
            actionToExecuteOnMainThread = true;
        }
    }

    public static void UpdateMain()
    {
        if (actionToExecuteOnMainThread == true)
        {
            copiedMainThread.Clear();
            lock (mainThread)
            {
                copiedMainThread.AddRange(mainThread);
                mainThread.Clear();
                actionToExecuteOnMainThread = false;
            }

            for (int i = 0; i < copiedMainThread.Count; ++i)
            {
                copiedMainThread[i](); 
            }
        }
    }
}
