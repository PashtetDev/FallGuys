using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.localEulerAngles += Vector3.up * 100 * Time.deltaTime;
    }
}
