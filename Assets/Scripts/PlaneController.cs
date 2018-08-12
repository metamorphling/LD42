using System.Collections;
using UnityEngine;
using Zenject;

public class PlaneController : MonoBehaviour
{
    [Inject]
    readonly SignalBus _signalBus;
    float force;
    float directForce;
    public float verticalSpeed = 0f;
    public float horizontalSpeed = 0f;
    public float tilt = 45f;
    public Material planeMaterial;
    public TrailRenderer trailRenderer;
    public TrailManager trailManager;
    public BoxCollider boxCollider;
    public float jumpStrength = 3f;
    public float jumpSpeed = 1f;

    float jump = 0;
    float timeSpent;

    bool moveBlock = false;
    bool readyToJump = true;
    bool isJumping = false;
    bool isPlaying = false;
    float defaultVerticalSpeed = 0f;

    void Start()
    {
        planeMaterial.SetColor("Color_C1DC765B", Color.white * 10f);
        force = 0;
        directForce = 0;
        defaultVerticalSpeed = verticalSpeed;
        _signalBus.Subscribe<SignalChangeColors>(OnChangeColor);
        _signalBus.Subscribe<SignalGameState>(OnChangeGameState);
    }

    void OnChangeGameState(SignalGameState args)
    {
        verticalSpeed = defaultVerticalSpeed;
        if (args.isPlaying == false)
            Reset(false);
        else
            Reset();
        isPlaying = args.isPlaying;
    }

    void Update()
    {
        if (isPlaying == false)
            return;

        force += Input.GetAxis("Horizontal") * horizontalSpeed;
        directForce += Input.GetAxis("Vertical") * verticalSpeed;

        if (readyToJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                readyToJump = false;
                isJumping = true;
                timeSpent = 0f;
            }
        }
        timeSpent += Time.deltaTime * jumpSpeed;
        if (isJumping == true)
        {
            var distance = Mathf.Clamp(Mathf.Sin(timeSpent), 0, 1);
            if (distance <= 0)
            {
                jump = 0;
                readyToJump = true;
                isJumping = false;
            }
            else
                jump = distance * jumpStrength;
        }
    }

    private void FixedUpdate()
    {
        if (moveBlock == false)
            transform.position = new Vector3(0f + ((10f - jump) * Mathf.Sin(Mathf.Deg2Rad * force)),
                                     3f + ((5f - jump) * Mathf.Cos(Mathf.Deg2Rad * force)),
                                     directForce);
    }

    float MCos(float value)
    {
        return Mathf.Cos(Mathf.Deg2Rad * value);
    }

    float MSin(float value)
    {
        return Mathf.Sin(Mathf.Deg2Rad * value);
    }

    public void Reset(bool reenable = true)
    {
        trailRenderer.emitting = false;
        trailManager.enabled = false;
        boxCollider.enabled = false;
        moveBlock = true;
        force = 0;
        directForce = 0;
        if (reenable == true)
        {
            moveBlock = false;
            StartCoroutine(trailON());
        }
    }

    IEnumerator trailON()
    {
        yield return new WaitForSeconds(3);
        trailRenderer.emitting = true;
        trailManager.enabled = true;
        boxCollider.enabled = true;
    }

    private void OnValidate()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    void OnChangeColor(SignalChangeColors args)
    {
        Color toSet;

        if (args.color == Color.black)
            toSet = Color.white;
        else
            toSet = Color.black;

        planeMaterial.SetColor("Color_C1DC765B", toSet * 10f);
    }

    public void NextLevel()
    {
        verticalSpeed += 0.01f;
        _signalBus.Fire(new SignalIncreaseScore() { score = 100 });
    }
}
