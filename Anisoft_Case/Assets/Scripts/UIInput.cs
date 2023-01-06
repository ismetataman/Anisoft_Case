using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInput : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public UIManager uIManager;
    private void Update()
    {
        if (GameManager.instance.currentState == GameState.ChangeColor)
        {
            GameManager.instance.MoveObjectsDown();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.delta.y < 0 && GameManager.instance.currentState == GameState.Pour)
        {
            GameManager.instance.currentState = GameState.Mix;
            StartCoroutine(GameManager.instance.PourTheBowl());
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.instance.currentState == GameState.Start)
        {
            StartCoroutine(GameManager.instance.Begin());
        }
        else if (GameManager.instance.currentState == GameState.Mix)
        {
            GameManager.instance.currentState = GameState.Blend;
            StartCoroutine(GameManager.instance.ColorPicker());
            StartCoroutine(GameManager.instance.DestroyColors());
            StartCoroutine(GameManager.instance.EmptyMixer());

        }
        else if (GameManager.instance.currentState == GameState.Dip)
        {
            StartCoroutine(GameManager.instance.DipTheObject());
        }

    }


}
