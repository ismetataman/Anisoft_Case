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
    Dip
}
public class GameManager : MonoBehaviour
{
    public List<GameObject> colors = new List<GameObject>();
    public static GameManager instance;
    public GameState currentState = GameState.Start;
    public GameObject cam;

    [Header("Emitters")]
    public Obi.ObiEmitter emitter;
    public Obi.ObiEmitter emitter2;
    public Obi.ObiParticleRenderer renderer1;
    public Obi.ObiParticleRenderer renderer2;


    [Header("Bowl")]
    public Transform bowl;
    public Transform bowlDesPos;

    [Header("Mixer Parts")]
    public Transform mixer;
    public Transform mixerPart;
    public GameObject mixerTop;
    public Transform mixerPartDesPos;

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
        Color newColor = new Color(valueColorR, valueColorG, valueColorB);
        yield return new WaitForSeconds(1f);
        renderer1.particleColor = Color.Lerp(renderer1.particleColor, newColor / 1.2f, 1f);
        renderer2.particleColor = Color.Lerp(renderer2.particleColor, newColor / 1.2f, 1f);
        yield return new WaitForSeconds(1f);
        renderer1.particleColor = Color.Lerp(renderer1.particleColor, newColor, 1f);
        renderer2.particleColor = Color.Lerp(renderer2.particleColor, newColor, 1f);
    }


    public IEnumerator Begin()
    {
        bowl.DOMove(bowlDesPos.position, time);
        yield return new WaitForSeconds(1f);
        mixerPart.DOMove(mixerPartDesPos.position, time);
    }

    public IEnumerator PourTheBowl()
    {
        bowl.DOMoveX(-0.15f, 1f);
        yield return new WaitForSeconds(0.5f);
        bowl.DORotate(new Vector3(0, 0, -120), 1f);
        yield return new WaitForSeconds(1.2f);
        bowl.DOMoveY(3f, 1f);
        mixerPart.DOLocalMove(new Vector3(0.001766419f, 0.04385072f, mixerPart.transform.localPosition.z), 1f);
        cam.transform.DOMove(new Vector3(0, 1.186f, -5f), 1f);
        cam.transform.DORotate(new Vector3(17f, 0, 0), 1f);
    }

    public IEnumerator EmptyMixer()
    {
        yield return new WaitForSeconds(7f);
        mixer.DOMoveX(-1.5f, 2.5f);
        mixerPart.DOMoveX(-1.5f, 2.5f);
        mixerPart.transform.gameObject.SetActive(false);
        mixerTop.SetActive(true);
        cam.transform.DOMove(new Vector3(-2.2f, cam.transform.position.y, cam.transform.position.z), 2.5f);
        yield return new WaitForSeconds(2f);
        mixer.DORotate(new Vector3(120, -90, 0), 2f);
        yield return new WaitForSeconds(2f);
        mixer.DORotate(new Vector3(0, -90, 0), 1f);
        yield return new WaitForSeconds(.5f);
        mixer.DOMoveX(5f,2f);
        currentState = GameState.Dip;
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
        yield return new WaitForSeconds(0.5f);
        cam.transform.DOMove(new Vector3(cam.transform.position.x,cam.transform.position.y,-3f),1f);
        cam.transform.DORotate(new Vector3(60,0,0),1f);

    }

}
