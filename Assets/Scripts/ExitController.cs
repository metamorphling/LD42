using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ExitController : MonoBehaviour
{
    [Inject]
    GameManager gm;
    [Inject]
    readonly SignalBus _signalBus;
    Image img;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gm.NextLevel();
        }
    }

    private void Start()
    {
        _signalBus.Subscribe<SignalChangeColors>(OnChangeColor);
        img = GetComponent<Image>();
    }

    void OnChangeColor(SignalChangeColors args)
    {
        Color global = args.color;
        Color toSet;
        
        if (global == Color.black)
            toSet = Color.white;
        else
            toSet = Color.black;
        img.color = toSet;
    }
}
