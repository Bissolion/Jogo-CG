using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Efeitos Visuais de Combate")]
    public ParticleSystem impactFlashFX;
    public ParticleSystem swordSlashArcFX;

    [Header("Configurações de Movimento")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public Collider swordCollider;

    public Light swordLight;

    [Header("Configurações de Pulo")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Rigidbody rb;
    private Animator anim;
    private float horizontalInput;

    [Header("Configurações de Combate")]
    public float comboWindow = 1.2f;
    private int comboStep = 0;
    private float lastAttackTime = 0f;

    private bool isBlocking = false;
    private bool canClick = true;

    [HideInInspector]
    public bool escudoDesativadoPelaFase1 = false;

    [HideInInspector]
    public bool emCutscene = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        if (emCutscene) return;

        Vector2 inputVector = value.Get<Vector2>();
        horizontalInput = inputVector.x;
    }

    public void OnJump(InputValue value)
    {
        if (emCutscene) return;

        if (value.isPressed && isGrounded)
        {
            anim.SetTrigger("DoJump");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        anim.SetBool("isGrounded", isGrounded);
        float currentVisualSpeed = Mathf.Abs(horizontalInput);
        if (isBlocking || comboStep > 0)
        {
            currentVisualSpeed = 0f;
        }

        float speed = Mathf.Abs(horizontalInput);
        anim.SetFloat("Speed", speed);

        if (horizontalInput != 0 && !isBlocking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }

        if (horizontalInput != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (emCutscene)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            anim.SetFloat("Speed", 0f);
            return;
        }
        if (isBlocking && !escudoDesativadoPelaFase1)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else if (comboStep > 0)
        {
            float attackSpeedModifier = 1f;

            if (comboStep == 1)
            {
                attackSpeedModifier = 0.25f;
            }
            else if (comboStep == 2)
            {
                attackSpeedModifier = 0.20f;
            }
            else if (comboStep == 3)
            {
                attackSpeedModifier = 0.20f;
            }

            float finalAttackSpeed = horizontalInput * moveSpeed * attackSpeedModifier;
            rb.linearVelocity = new Vector3(finalAttackSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(horizontalInput * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
    }

    public void OnAttack(InputValue value)
    {
        if (emCutscene) return;

        if (isBlocking) return;

        if (value.isPressed && canClick)
        {
            if (Time.time - lastAttackTime > comboWindow)
            {
                comboStep = 0;
            }

            comboStep++;

            if (comboStep > 3)
            {
                comboStep = 1;
            }

            lastAttackTime = Time.time;
            anim.SetInteger("ComboIndex", comboStep);
            canClick = false;

            CancelInvoke("EnableSword");
            CancelInvoke("AllowClick");
            CancelInvoke("DisableSword");
            CancelInvoke("ResetComboIndex");

            DisableSword();

            if (comboStep == 1)
            {
                Invoke("EnableSword", 0.1f);
                Invoke("AllowClick", 0.45f);
                Invoke("DisableSword", 0.6f);
                Invoke("ResetComboIndex", 0.6f);
            }
            else if (comboStep == 2)
            {
                Invoke("EnableSword", 0.15f);
                Invoke("AllowClick", 0.6f);
                Invoke("DisableSword", 0.7f);
                Invoke("ResetComboIndex", 0.8f);
            }
            else if (comboStep == 3)
            {
                Invoke("EnableSword", 0.2f);
                Invoke("AllowClick", 0.7f);
                Invoke("DisableSword", 0.8f);
                Invoke("ResetComboIndex", 0.85f);
            }
        }
    }

    public void OnBlock(InputValue value)
    {
        if (emCutscene) return;

        if (escudoDesativadoPelaFase1)
        {
            isBlocking = false;
            anim.SetBool("IsBlocking", false);
            return;
        }
        isBlocking = value.isPressed;
        anim.SetBool("IsBlocking", isBlocking);

        if (isBlocking)
        {
            CancelInvoke("EnableSword");
            CancelInvoke("DisableSword");
            CancelInvoke("ResetComboIndex");
            CancelInvoke("AllowClick");

            DisableSword();
            ResetComboIndex();
            canClick = true;
        }
    }

    void AllowClick()
    {
        canClick = true;
    }

    void DisableSword()
    {
        swordCollider.enabled = false;
        if (swordLight != null) swordLight.enabled = false;
    }

    void ResetComboIndex()
    {
        comboStep = 0;
        anim.SetInteger("ComboIndex", 0);
    }

    void EnableSword()
    {
        swordCollider.enabled = true;
        if (swordLight != null) swordLight.enabled = true;
    }
}