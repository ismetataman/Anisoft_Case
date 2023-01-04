using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInput : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.delta.y < 0 && GameManager.instance.currentState == GameState.Pour)
        {
            GameManager.instance.currentState = GameState.Mix;
            StartCoroutine(GameManager.instance.PourTheBowl());
            Debug.Log(eventData.delta.y);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.instance.currentState == GameState.Start)
        {
            GameManager.instance.currentState = GameState.Pour;
            StartCoroutine(GameManager.instance.Begin());
        }
        else if(GameManager.instance.currentState == GameState.Mix)
        {
            Debug.Log("Mixing");
        }

    }
}
