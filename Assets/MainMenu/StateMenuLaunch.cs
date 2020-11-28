using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMenuLaunch : MonoBehaviour
{
    public static StateMenuLaunch instance;
    public bool IsFirstLaunch = true; 
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
        DontDestroyOnLoad(this);
    }

    
        
    

}
