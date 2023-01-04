using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum GameState
{
    Start,
    Pour,
    Mix
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentState = GameState.Start;
    public GameObject cam;
   

    [Header("Bowl")]
    public Transform bowl;
    public Transform bowlDesPos;

    [Header("Mixer Parts")]
    public Transform mixerPart;
    public Transform mixerPartDesPos;

    public float time;

    private void Awake()
    {
        instance = this;
    }


    public IEnumerator Begin()
    {
        bowl.DOMove(bowlDesPos.position, time);
        yield return new WaitForSeconds(1f);
        mixerPart.DOMove(mixerPartDesPos.position, time);
    }

    public IEnumerator PourTheBowl()
    {
        bowl.DOMoveX(-0.15f,1f);
        yield return new WaitForSeconds(0.5f);
        bowl.DORotate(new Vector3(0,0,-120),1f);
        yield return new WaitForSeconds(1f);
        bowl.DOMoveY(3f,1f);
        cam.transform.DOMove(new Vector3(0,1.186f,-5f),1f);
        cam.transform.DORotate(new Vector3(17f,0,0),1f);
    }
}
