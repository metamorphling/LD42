using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkyboxController : MonoBehaviour
{
    public List<Image> images;
    public Texture blackSky, whiteSky;
    public Transform matchZ;
    List<SkyboxSettings> settings;

    bool skyIsBlack = true;
    float directionModifier = 0.3f;
    float scrollModifier = -0.3f;
    [Inject]
    readonly SignalBus _signalBus;

    private void Start()
    {
        _signalBus.Subscribe<SignalChangeColors>(OnChangeColor);
        settings = new List<SkyboxSettings>();
        settings.Add(new SkyboxSettings());
        settings.Add(new SkyboxSettings());
        settings.Add(new SkyboxSettings());
        settings.Add(new SkyboxSettings());
        settings[(int)Shaders.Up].scroll = new Vector2(1.5f, 1.5f);
        settings[(int)Shaders.Left].scroll = new Vector2(1.5f, 1.5f);
        settings[(int)Shaders.Right].scroll = new Vector2(1.5f, 1.5f);
        settings[(int)Shaders.Down].scroll = new Vector2(1.5f, 1.5f);
        settings[(int)Shaders.Up].direction = new Vector2(0f, -0.3f);
        settings[(int)Shaders.Left].direction = new Vector2(0.3f, 0f);
        settings[(int)Shaders.Right].direction = new Vector2(-0.3f, 0);
        settings[(int)Shaders.Down].direction = new Vector2(0, 0.3f);
        SetDefaultSettings();
    }

    public void Reset()
    {
        SetDefaultSettings();
    }

    private void OnValidate()
    {
        images.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            images.Add(transform.GetChild(i).GetComponent<Image>());
        }
    }

    void OnChangeColor(SignalChangeColors args)
    {
        Texture toSet;
        if (args.color == Color.black)
            toSet = blackSky;
        else
            toSet = whiteSky;

        for (int i = 0; i < images.Count; i++)
            images[i].material.SetTexture("_MainTex", toSet);
    }

    public void NextLevel()
    {
        for (int i = 0; i < images.Count; i++)
        {
            var scroll = images[i].material.GetVector("Vector2_63B1780A");
            var direction = images[i].material.GetVector("Vector2_7F7308D7");

            if (scroll.x != 0)
                if ((i == (int)Shaders.Left) || (i == (int)Shaders.Right))
                    scroll.x += Mathf.Sign(scroll.x) * scrollModifier;
            if (scroll.y != 0)
                if ((i == (int)Shaders.Up) || (i == (int)Shaders.Down))
                    scroll.y += Mathf.Sign(scroll.y) * scrollModifier;
            if (direction.x != 0)
                direction.x += Mathf.Sign(direction.x) * directionModifier;
            if (direction.y != 0)
                direction.y += Mathf.Sign(direction.y) * directionModifier;

            images[i].material.SetVector("Vector2_63B1780A", scroll);
            images[i].material.SetVector("Vector2_7F7308D7", direction);
        }
    }

    void SetDefaultSettings()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].material.SetVector("Vector2_63B1780A", settings[i].scroll);
            images[i].material.SetVector("Vector2_7F7308D7", settings[i].direction);
        }
    }
}

class SkyboxSettings
{
    public Vector2 scroll;
    public Vector2 direction;
}

enum Shaders
{
    Up,
    Left,
    Right,
    Down
}