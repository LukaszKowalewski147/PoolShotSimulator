using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class MainMenu : MonoBehaviour
{
    public Slider progressBar;
    public GameObject optionsMenu;
    public GameObject mainMenu;

    void Start()
    {
        optionsMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void LoadSimulator()
    {
        Invoke("InvokedLoading", 0.5f);
    }

    void InvokedLoading()
    { 
        StartCoroutine(LoadSimulationAsync());
    }

    IEnumerator LoadSimulationAsync()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync("Simulator");
        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            Debug.Log(progress);
            progressBar.value = progress;
            yield return null;
        }
    }

    public void EnterOptionsMenu()
    {
        Invoke("ShowOptions", 0.2f);
    }

    void ShowOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ExitSimulator()
    {
        Invoke("ExitApplication", 0.3f);
    }

    void ExitApplication()
    {
        Application.Quit();
    }
}
