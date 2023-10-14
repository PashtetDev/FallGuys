using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private PAMControl control;
    private string sceneName;

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Menu")
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartGame(float waitTime)
    {
        StartCoroutine(WaitLoadScene(waitTime, "Game"));
    }

    public void ExitToMenu(float waitTime)
    {
        StartCoroutine(WaitLoadScene(waitTime, "Menu"));
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator WaitLoadScene(float loadTime, string scene)
    {
        if (control != null)
            control.Play();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(scene);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (sceneName)
            {
                case "Menu":
                    Exit();
                    break;
                //case "Game":
                //    StartCoroutine(WaitLoadScene(0.5f, "Menu"));
                //    break;
                default:
                    break;
            }
        }

    }
}
