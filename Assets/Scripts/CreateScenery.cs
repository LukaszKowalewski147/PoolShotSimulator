using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScenery : MonoBehaviour
{
    public Sprite[] scenery;

    // Start is called before the first frame update
    void Start()
    {
        int sceneryIndex = Settings.instance.GetScenery();
        GetComponent<SpriteRenderer>().sprite = scenery[sceneryIndex];
    }
}
