using UnityEngine;
using UnityEngine.UI;

public class EnergyInit : MonoBehaviour
{
    [SerializeField]
    private Material _playerMaterial;

    private void Awake()
    {
        GetComponent<Image>().color = _playerMaterial.color;
        Destroy(this);
    }
}
