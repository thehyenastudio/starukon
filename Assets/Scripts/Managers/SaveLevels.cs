using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLevels : MonoBehaviour
{
    public static SaveLevels Instance;
    public int level = 0;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
