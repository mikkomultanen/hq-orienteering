using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(AudioSource)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float force = 300;
    public float jumpInterval = 0.01f;
    public AudioClip[] eatSfx;
    public float alcoholAttenuation = 0.9f;
    public float maxAlcoholLevel = 10;
    public float caffeineAttenuation = 0.8f;
    public float maxCaffeineLevel = 10;
    private bool facingRight = true;
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource source;

    private CinemachineImpulseSource impulseSource;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private bool canJump = true;
    private float alcoholLevel = 0;
    private float caffeineLevel = 0;
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int RunningSpeed = Animator.StringToHash("RunningSpeed");

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }


    private void Update()
    {
        var facedDirectiong = facingRight;
        if (canJump)
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            var dir = new Vector2(x, y) + Random.insideUnitCircle * Mathf.Clamp01(alcoholLevel / maxAlcoholLevel);
            if (dir.sqrMagnitude > 0.1)
            {
                StartCoroutine(Jump(dir));
            }
        }

        var velocity = rb.velocity;
        facingRight = velocity.x >= 0.1;
        if (facedDirectiong != facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
        }


        if (Mathf.Abs(velocity.x) >= 0.15 || Mathf.Abs(velocity.y) >= 0.15)
        {
            anim.SetBool(Running, true);
            anim.SetFloat(RunningSpeed, velocity.magnitude * 0.15f);
        }
        else
        {
            anim.SetBool(Running, false);
        }

        alcoholLevel = alcoholLevel * Mathf.Pow(alcoholAttenuation, Time.deltaTime);
        caffeineLevel = caffeineLevel * Mathf.Pow(caffeineAttenuation, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var food = collision.gameObject.GetComponent<Food>();
        if (food != null)
        {
            source.PlayOneShot(food.eatSfx);
        }
    }

    private IEnumerator Jump(Vector2 dir)
    {
        canJump = false;
        var r = Random.onUnitSphere;
        dir.Normalize();
        var alcoholMultiplier = Mathf.Pow(Mathf.Clamp01(alcoholLevel / maxAlcoholLevel), 2);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = alcoholMultiplier * 10f;
        var caffeineMultiplier = Mathf.Pow(Mathf.Clamp01(caffeineLevel / maxCaffeineLevel), 0.5f);
        impulseSource.m_ImpulseDefinition.m_AmplitudeGain = caffeineMultiplier * 0.1f;
        impulseSource.GenerateImpulse(transform.position);
        Debug.Log("Jump: " + alcoholMultiplier + " " + caffeineMultiplier);
        dir = Vector2.Lerp(dir, r, alcoholMultiplier) * (1 + alcoholMultiplier + caffeineMultiplier);
        rb.AddForce(dir * force);
        yield return new WaitForSeconds(jumpInterval);
        canJump = true;
    }

    public void AddAlcohol(float amount)
    {
        alcoholLevel += amount;
    }

    public void AddCaffeine(float amount)
    {
        caffeineLevel += amount;
    }
}