using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // allows so that

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // do not destroy game object when another scene is loaded
            // will maintain game manager as you pass through levels
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; 
        }
        
    }

}
