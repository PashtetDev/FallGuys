using System.Collections.Generic;
using UnityEngine;

public class PlayerAcces : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> hats;
    [SerializeField]
    private List<GameObject> acces;
    [SerializeField]
    private List<GameObject> boots;
    [SerializeField]
    private Material playerMaterial;

    public void Change(GameObject selectedHat, GameObject selectedAcs, Color selectedColor)
    {
        foreach (var item in hats)
        {
            if (item != selectedHat)
                item.gameObject.SetActive(false);
            else
                item.gameObject.SetActive(true);
        }
        foreach (var item in acces)
        {
            if (item != selectedAcs)
                item.gameObject.SetActive(false);
            else
                item.gameObject.SetActive(true);
        }
        playerMaterial.color = selectedColor;
    }
}
