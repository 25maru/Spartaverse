using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float flapForce = 6f;
    [SerializeField] private float velocityReductionFactor = 0.1f;
    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private bool godMode = false;

    private FlappyGameManager gameManager;
    private float smoothRotation = 0f;
    private bool isFlap = false;
    private bool isDead = false;
    private bool isStart = false;
    private float deathCooldown = 0f;

    private void Start()
    {
        gameManager = FlappyGameManager.Instance; 

        if (anim == null) Debug.LogError("애니메이터 컴포넌트를 찾을 수 없습니다.");
        if (rb == null) Debug.LogError("리지드바디 컴포넌트를 찾을 수 없습니다.");

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (!isStart)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                isStart = true;

                gameManager.UIManager.StartText.gameObject.SetActive(false);

                rb.velocity = new Vector2(0f, flapForce / 5f);
            }

            return;
        }

        if (isDead)
        {
            // if (deathCooldown <= 0)
            // {
            //     if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            //     {
            //         gameManager.Restart();
            //     }
            // }
            // else
            // {
            //     deathCooldown -= Time.deltaTime;
            // }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = rb.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            float dynamicFlapForce = flapForce;

            if (rb.velocity.y > 0)
            {
                dynamicFlapForce *= Mathf.Exp(-velocityReductionFactor * rb.velocity.y);
            }
            velocity.y += dynamicFlapForce;
            // velocity.y += flapForce;

            isFlap = false;
        }

        rb.velocity = velocity;

        float targetAngle = Mathf.Clamp(rb.velocity.y * 10f, -70, 70);
        smoothRotation = Mathf.Lerp(smoothRotation, targetAngle, Time.fixedDeltaTime * 3f);

        transform.rotation = Quaternion.Euler(0, 0, smoothRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode) return;
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;

        anim.SetInteger("IsDie", 1);
        gameManager.GameOver();
    }
}
