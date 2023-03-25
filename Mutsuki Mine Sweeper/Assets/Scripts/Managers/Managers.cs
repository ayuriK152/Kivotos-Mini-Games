using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _managers;
    static Managers Manager { get { return _managers; } }

    StageManager _stageManager = new StageManager();

    public static StageManager Stage { get { return Manager._stageManager; } }
    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (_managers == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                Debug.LogError("Can't find Manager gameobject");
            }
            DontDestroyOnLoad(go);
            _managers = go.GetComponent<Managers>();
        }

        Stage.Init();
    }
}
