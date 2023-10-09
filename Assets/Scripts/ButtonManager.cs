using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private PAMControl control;

    public void Play()
    {
        StartCoroutine(WaitLoadGame());
    }

    private IEnumerator WaitLoadGame()
    {
        control.Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Game");
    }

}
