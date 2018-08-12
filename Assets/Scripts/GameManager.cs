using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    PlaneController plane;
    [Inject]
    TrailStorageController trail;
    [Inject]
    TimerController timer;
    [Inject]
    ScoreController score;
    [Inject]
    SkyboxController sky;

    [Inject]
    readonly SignalBus _signalBus;

    Camera cam;
    Color globalColor;

    public void Lose()
    {
        trail.Reset();
        plane.Reset();
        timer.Reset();
        score.Reset();
        globalColor = Color.black;
        ChangeColor();
    }

    public void NextLevel()
    {
        if (globalColor == Color.black)
            globalColor = Color.white;
        else
            globalColor = Color.black;

        ChangeColor();
        sky.NextLevel();
        plane.NextLevel();
        timer.NextLevel();
        plane.Reset();
    }

    public void ChangeColor()
    {
        cam.backgroundColor = globalColor;
        _signalBus.Fire(new SignalChangeColors() { color = globalColor });
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        cam = Camera.main;
        globalColor = Color.white;
        ChangeColor();
    }
}
