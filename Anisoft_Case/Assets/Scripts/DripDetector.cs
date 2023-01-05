using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripDetector : MonoBehaviour
{
    public Material xboxMat;
    private void Start()
    {
        xboxMat.color = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Drip":
                other.GetComponent<Renderer>().material.color = GameManager.instance.newColor * 1.5f;
                break;
            case "Xbox":
                xboxMat.color = GameManager.instance.newColor * 1.5f;
                break;
        }
    }
}
