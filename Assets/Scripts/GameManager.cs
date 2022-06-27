using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button resetRotationBtn;

    void Update()
    {
        ListenForKeyPress();
    }

    void ListenForKeyPress()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu");
        }

        if(Input.GetKey("r"))
        {
            resetRotationBtn.onClick.Invoke();
        }
    }
}
