using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum GameState
{
    Start,
    Pour,
    Mix,
    Blend,
    Dip,
    ChangeColor,
    Complete
}
public class GameManager : MonoBehaviour
{
    public UIManager uIManager;
    public List<GameObject> colors = new List<GameObject>();
    public static GameManager instance;
    public GameState currentState = GameState.Start;
    public GameObject cam;

    [Header("Emitters")]
    public Obi.ObiEmitter emitter;
    public Obi.ObiEmitter emitter2;
    public Obi.ObiParticleRenderer renderer1;
    public Obi.ObiParticleRenderer renderer2;
    public Color newColor;


    [Header("Bowl")]
    public Transform bowl;
    public Transform bowlDesPos;
    public Transform emptyBowl;

    [Header("Mixer Parts")]
    public Transform mixer;
    public Transform mixerPart;
    public GameObject mixerTop;
    public Transform mixerPartDesPos;

    [Header("Xbox & Stick")]
    public GameObject xbox;
    public GameObject stick;
    

    public float time;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (currentState == GameState.Blend)
        {
            emitter.enabled = true;
            emitter2.enabled = true;
        }
    }

    public IEnumerator ColorPicker()
    {
        float rendererR = renderer1.particleColor.r;
        float rendererG = renderer1.particleColor.g;
        float rendererB = renderer1.particleColor.b;

        float renderer2R = renderer2.particleColor.r;
        float renderer2G = renderer2.particleColor.g;
        float renderer2B = renderer2.particleColor.b;

        float valueColorR = (rendererR + renderer2R) / 2.5f;
        float valueColorG = (rendererG + renderer2G) / 2.5f;
        float valueColorB = (rendererB + renderer2B) / 2.5f;


        float average = Mathf.RoundToInt(100 * (valueColorR + valueColorG + valueColorB) / 3);
        newColor = new Color(valueColorR, valueColorG, valueColorB);
        yield return new WaitForSeconds(1f);
        renderer1.particleColor = Color.Lerp(renderer1.particleColor, newColor, 1f);
        renderer2.particleColor = Color.Lerp(renderer2.particleColor, newColor, 1f);
        yield return new WaitForSeconds(1f);
        renderer1.particleColor = Color.Lerp(renderer1.particleColor, newColor * 1.1f, 1f);
        renderer2.particleColor = Color.Lerp(renderer2.particleColor, newColor * 1.1f, 1f);
    }


    public IEnumerator Begin()
    {
        uIManager.SetInactive();
        bowl.DOMove(bowlDesPos.position, time);
        yield return new WaitForSeconds(1f);
        mixerPart.DOMove(mixerPartDesPos.position, time);
        currentState = GameState.Pour;
        yield return new WaitForSeconds(.5f);
        uIManager.SwipeFingerDown();
        
    }

    public IEnumerator PourTheBowl()
    {
        uIManager.SwipeFingerDownInactive();
        bowl.DOMoveX(-0.15f, 1f);
        yield return new WaitForSeconds(0.5f);
        bowl.DORotate(new Vector3(0, 0, -120), 1f);
        yield return new WaitForSeconds(0.5f);
        SoundManager.DropSound();
        yield return new WaitForSeconds(1.2f);
        bowl.DOMoveY(3f, 1f);
        mixerPart.DOLocalMove(new Vector3(0.001766419f, 0.04385072f, mixerPart.transform.localPosition.z), 1f);
        cam.transform.DOMove(new Vector3(0, 1.186f, -5f), 1f);
        cam.transform.DORotate(new Vector3(17f, 0, 0), 1f);
        uIManager.FingerTap();
        
    }

    public IEnumerator EmptyMixer()
    {
        SoundManager.MixerSound();
        uIManager.SetInactive();
        yield return new WaitForSeconds(7f);
        mixer.DOMoveX(-1.5f, 2.5f);
        mixerPart.DOMoveX(-1.5f, 2.5f);
        mixerPart.transform.gameObject.SetActive(false);
        mixerTop.SetActive(true);
        cam.transform.DOMove(new Vector3(-2.2f, cam.transform.position.y, cam.transform.position.z), 2.5f);
        yield return new WaitForSeconds(2f);
        mixer.DORotate(new Vector3(120, -90, 0), 2f);
        SoundManager.PourSound();
        yield return new WaitForSeconds(2f);
        mixer.DORotate(new Vector3(0, -90, 0), 1f);
        yield return new WaitForSeconds(.5f);
        mixer.DOMoveX(5f, 2f);
        currentState = GameState.Dip;
        uIManager.FingerTap();
    }
    public IEnumerator DestroyColors()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            colors[i].SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator DipTheObject()
    {
        uIManager.SetInactive();
        yield return new WaitForSeconds(0.5f);
        cam.transform.DOMove(new Vector3(cam.transform.position.x, cam.transform.position.y, -3f), 1f);
        cam.transform.DORotate(new Vector3(60, 0, 0), 1f);
        xbox.transform.DOMoveY(0, 1f);
        currentState = GameState.ChangeColor;
        uIManager.HoldToDip();
    }

    public void MoveObjectsDown()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.WaterShakeSound();
            uIManager.SetInactive();
            xbox.transform.DOMoveY(-1, 1f);
        }
        if(Input.GetMouseButtonUp(0))
        {
            xbox.transform.DOMoveY(0f, 1f);
            currentState = GameState.Complete;
            StartCoroutine(ShowTheObject());
        }

    }

    IEnumerator ShowTheObject()
    {
        yield return new WaitForSeconds(1f);
        stick.transform.parent = null;
        stick.transform.DOMoveY(3f,1f);
        emptyBowl.transform.DOMoveX(-4,1f);
        yield return new WaitForSeconds(1);
        xbox.transform.DORotate(new Vector3(-20,0,180),1f);
        uIManager.confetti.SetActive(true);
        SoundManager.WinSound();
    }


}
