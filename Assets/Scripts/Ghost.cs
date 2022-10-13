using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGhostState
{
    Visible,
    Showing,
    Blinking,
    Hiding,
    Hiden
}

public class Ghost : MonoBehaviour
{
    [Header("Movment")]
    [SerializeField] private float speed;

    [Space(1)]
    [Header("Visible")]
    [SerializeField] private float visibleTransparency = 0.8f;
    [SerializeField] private float showingSpeed;

    [Space(1)]
    [Header("Hiden")]
    [SerializeField] private float hidenTransparency = 0.1f;
    [SerializeField] private float hidingSpeed;

    [Space(1)]
    [Header("Blink")]
    [SerializeField] private ValueAnimation blinkAnimation;
    [SerializeField] private float maxBlinkTransperency = 0.3f;
    [SerializeField] private float blinkChance = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float transparency;
    private Transform player;
    private EGhostState state;

    private void Awake()
    {
        Entities.AddEnemy(this);
    }

    private void OnDestroy()
    {
        Entities.RemoveEnemy(this);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        transparency = hidenTransparency;
        state = EGhostState.Hiden;
    }

    private void Update()
    {
        MoveToPlayer();

        switch (state)
        {
            case EGhostState.Hiden:
                if (Random.Range(0.0f, 1.0f) <= blinkChance)
                {
                    StartBlinking();
                }
                break;

            case EGhostState.Showing:
                Showing();
                break;

            case EGhostState.Hiding:
                Hiding();
                break;

            case EGhostState.Blinking:
                Blinking();
                break;

            case EGhostState.Visible:
                Hide();
                break;
        }
        UpdateTransparency();
    }

    public void Show()
    {
        state = EGhostState.Showing;
    }

    public void Hide()
    {
        state = EGhostState.Hiding;
    }

    private void MoveToPlayer()
    {
        var direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Hiding()
    {
        if(transparency > hidenTransparency)
        {
            transparency = Mathf.Clamp(transparency - hidingSpeed * Time.deltaTime, hidenTransparency, 255);
        }
        else
        {
            state = EGhostState.Hiden;
        }
    }

    private void Showing()
    {
        if (transparency < visibleTransparency)
        {
            transparency = Mathf.Clamp(transparency + showingSpeed * Time.deltaTime, 0, visibleTransparency);
        }
        else
        {
            state = EGhostState.Visible;
        }
    }

    private void Blinking()
    {
        if (!blinkAnimation.IsEnded)
        {
            blinkAnimation.Update();
            transparency = blinkAnimation.Value * (maxBlinkTransperency - hidenTransparency) + hidenTransparency;
        }
        else
        {
            state = EGhostState.Hiden;
        }
    }

    private void StartBlinking()
    {
        state = EGhostState.Blinking;
        blinkAnimation.Start();
    }

    private void UpdateTransparency()
    {
        Color c = spriteRenderer.color;
        c.a = transparency;
        spriteRenderer.color = c;
    }
}
