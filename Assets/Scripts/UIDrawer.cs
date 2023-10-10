using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDrawer : MonoBehaviour
{
    [SerializeField]
    private Image energy, energyBackground;
    [SerializeField]
    private GameObject loseTitle, winTitle, buttons;

    private void Awake()
    {
        buttons.SetActive(false);
        winTitle.SetActive(false);
        loseTitle.SetActive(false);
    }

    public void WinRender()
    {
        winTitle.SetActive(true);
        ButtonRender();
    }

    public void LoseRender()
    {
        loseTitle.SetActive(true);
        ButtonRender();
    }

    private void ButtonRender()
    {
        buttons.gameObject.SetActive(true);
    }

    public void UpdateHealth(float currentHealth)
    {
        StartCoroutine(Damage(currentHealth));
    }

    private IEnumerator Damage(float currentHealth)
    {
        energy.fillAmount = currentHealth;
        float fillTime = energyBackground.fillAmount - energy.fillAmount;
        while (energyBackground.fillAmount > energy.fillAmount)
        {
            energyBackground.fillAmount -= fillTime * Time.deltaTime;
            yield return null;
        }
        energyBackground.fillAmount = energy.fillAmount;
    }
}
