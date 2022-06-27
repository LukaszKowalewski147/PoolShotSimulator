using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings instance;

    private float volume = 1;
    private int scenery = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public void SetScenery(int scenery)
    {
        this.scenery = scenery;
    }

    [HideInInspector]
    public float GetVolume()
    {
        return volume;
    }

    [HideInInspector]
    public int GetScenery()
    {
        return scenery;
    }
}
