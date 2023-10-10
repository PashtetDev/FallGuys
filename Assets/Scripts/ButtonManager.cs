using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private PAMControl control;

    public void StartGame()
    {
        StartCoroutine(WaitLoadScene(1.5f, "Game"));
    }
    public void RestartGame()
    {
        StartCoroutine(WaitLoadScene(0.5f, "Game"));
    }

    private IEnumerator WaitLoadScene(float loadTime, string scene)
    {
        if (control != null)
            control.Play();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(scene);
    }

    public void OpenMenu()
    {
        StartCoroutine(WaitLoadScene(0.5f, "Menu"));
    }

}
