using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [Header("Animator")]
    public Animator fingerAnim;

    [Header("UI Objects")]
    public GameObject finger;
    public GameObject fingerClick;
    public GameObject tapToStart;
    public GameObject holdToDip;
    public GameObject confetti;
    
    

    public void SetInactive()
    {
        finger.SetActive(false);
        fingerClick.SetActive(false);
        tapToStart.SetActive(false);
        holdToDip.SetActive(false);
    }

    public void SwipeFingerDown()
    {
        finger.SetActive(true);
        fingerAnim.SetBool("canSwipe",true);
    }

    public void SwipeFingerDownInactive()
    {
        finger.SetActive(false);
    }

    public void FingerTap()
    {
        finger.SetActive(true);
        fingerClick.SetActive(true);
    }
    public void HoldToDip()
    {
        finger.SetActive(true);
        fingerClick.SetActive(true);
        holdToDip.SetActive(true);
    }
}
