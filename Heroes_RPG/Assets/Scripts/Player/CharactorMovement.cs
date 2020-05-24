using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorMovement : MonoBehaviour
{
    private MovementMotor motor;

    public float move_magnitude = 0.05f;
    public float speed = 0.7f;
    public float speed_Move_WhileAttack = 0.1f;
    public float speed_Attack = 1.5f;
    public float turnSpeed = 10f;
    public float speed_jump = 20f;

    private float speed_Move_multiper = 1f;

    private Vector3 direction;

    private Animator anim;
    private Camera mainCamera;

    private string PARAMETER_STATE = "State";
    private string PARAMETER_ATTACK_TYPE = "Attack_Type";
    private string PARAMETER_ATTACK_INDEX = "Attck_Index";

    public AttackAnimation[] attack_Animation;
    public string[] combo_AttackList;
    public int combo_type;

    private int attack_Index = 0;
    private string[] combo_List;
    private int attack_State;
    private float attack_State_TimeTemp;

    private bool isAttacking;

    private GameObject atkPoint;

    public GameObject fireTornado;

    private void Awake()
    {
        motor = GetComponent<MovementMotor>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        anim.applyRootMotion  = false;
        mainCamera = Camera.main;
        atkPoint = GameObject.Find("Player Attack Point");
        atkPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleAttackAnimation();
      

        if(MouseLock.Mouselocked)
        {
            if(Input.GetButtonDown ("Fire1"))
            {
                Attack();
            }
            if (Input.GetButtonDown ("Fire2"))
            {
                Attack();
                StartCoroutine(SpecialAttack());
            }
        }
        MovementAndJumping();
    }

    private Vector3 MoveDirection
    {
        get { return direction; }

        set
        {
            direction = value * speed_Move_multiper;

            if (direction.magnitude > 0.1f)
            {
                var newRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
            }

            direction *= speed * (Vector3.Dot(transform.forward, direction) + 1f) * 5f;
            motor.Move(direction);

            AnimationMove(motor.charController.velocity.magnitude * 0.1f);
        }
    }

    void Moving(Vector3 dir,float mult)
    {

      //  speed_Move_multiper = 1 * mult;
       // MoveDirection = dir;
       if(isAttacking)
        {
            speed_Move_multiper = speed_Move_WhileAttack * mult;
        }
        else
        {
            speed_Move_multiper = 1 * mult;
        }
        MoveDirection = dir;
    }

    void Jump()
    {
        motor.Jump(speed_jump);
    }

    void AnimationMove(float magnitude)
    {
        if(magnitude > move_magnitude)
        {
            float speed_Animation = magnitude * 2f;

            if(speed_Animation < 1f)
            {
                speed_Animation = 1f;
            }
            if(anim.GetInteger(PARAMETER_STATE) != 2)
            {
                anim.SetInteger(PARAMETER_STATE, 1);
            }
        }
        else
        {
            if (anim.GetInteger(PARAMETER_STATE) != 2)
            {
                anim.SetInteger(PARAMETER_STATE, 0);
            }
        }
    }
    void MovementAndJumping()
    {
        Vector3 moveInput = Vector3.zero;
        Vector3 forward = Quaternion.AngleAxis(-90, Vector3.up) * mainCamera.transform.right;

        moveInput += forward * Input.GetAxis("Vertical");
        moveInput += mainCamera.transform.right * Input.GetAxis("Horizontal");

        moveInput.Normalize();
        Moving(moveInput.normalized, 1f);

        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    void ResetCombo()
    {
        attack_Index = 0;
        attack_State = 0;
        isAttacking = false;
    }

    void FightAnimation()
    {
        if(combo_List != null && attack_Index >= combo_List.Length)
        {
            ResetCombo();
        }

        if(combo_List != null && combo_List.Length > 0)
        {
            int motionIndex = int.Parse(combo_List[attack_Index]);

            if(motionIndex < attack_Animation.Length)
            {
                anim.SetInteger(PARAMETER_STATE, 2);
                anim.SetInteger(PARAMETER_ATTACK_TYPE, combo_type);
                anim.SetInteger(PARAMETER_ATTACK_INDEX, attack_Index);
            }
        }
    }

    void HandleAttackAnimation()
    {
        if(Time.time > attack_State_TimeTemp + 0.5f)
        {
            attack_State = 0;
        }

        combo_List = combo_AttackList[combo_type].Split("," [0]);

        if(anim.GetInteger(PARAMETER_STATE) == 2)
        {
            anim.speed = speed_Attack;

            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsTag("Attack"))
            {
                int motionIndex = int.Parse(combo_List[attack_Index]);

                if(stateInfo.normalizedTime > 0.9f)
                {
                    anim.SetInteger(PARAMETER_STATE, 0);
                    isAttacking = false;

                    attack_Index++;

                    if(attack_State > 1)
                    {
                        FightAnimation();
                    }
                    else
                    {
                        if(attack_Index > combo_List.Length)
                        {
                            ResetCombo();
                        }
                    }
                }
            }
        }
    }

    void Attack()
    {
        if(attack_State < 1 || (Time.time > attack_State_TimeTemp + 0.2f && Time.time < attack_State_TimeTemp + 1f))
        {
            attack_State++;
            attack_State_TimeTemp = Time.time;
        }
        FightAnimation();
    }

    void Attack_Began()
    {
        atkPoint.SetActive(true);
    }
    void Attack_End()
    {
        atkPoint.SetActive(false);
    }

    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(fireTornado, transform.position + transform.forward * 2.5f, Quaternion.identity);
    }
}
