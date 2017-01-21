using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator anim;
    public Rigidbody rbody;

    public float linearSpeed = 106.0f;
    public float angularSpeed = 41.0f;

    private float inputH;
    private float inputV;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hit = anim.GetCurrentAnimatorStateInfo (0).IsName ("DAMAGED01");

        if (Input.GetKeyDown("1"))
        {
            anim.Play("WALK00_F", -1, 0f);
        }
        if (Input.GetKeyDown("2"))
        {
            anim.Play("WALK00_B", -1, 0f);
        }
        if (Input.GetKeyDown("3"))
        {
            anim.Play("WALK00_L", -1, 0f);
        }
        if (Input.GetKeyDown("4"))
        {
            anim.Play("WALK00_R", -1, 0f);
        }
        if (!hit && Input.GetMouseButtonDown(0))
        {
            anim.Play("DAMAGED01", -1, 0f);
            rbody.velocity = new Vector3(0f, 0f, 0f);
            hit = true;
        }

        if (!hit)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                anim.SetBool("jump", true);
            }
            else
            {
                anim.SetBool("jump", false);
            }

            bool run = Input.GetKey (KeyCode.LeftShift);

            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxis("Vertical");

            anim.SetFloat("inputH", inputH);
            anim.SetFloat("inputV", inputV);
            anim.SetBool ("run", run);

            float angularMove = inputH * angularSpeed * Time.deltaTime;
            float linearMove = inputV * linearSpeed * Time.deltaTime;

            if(run)
            {
                // Run!
                linearMove *= 3f;
            }

            if(linearMove <= 0f)
            {
                angularMove *= 2.2f;
            }

            Vector3 moveDirection = new Vector3(0f, 0f, linearMove);
            moveDirection = transform.TransformDirection (moveDirection);
            rbody.velocity = moveDirection;
            rbody.angularVelocity = new Vector3(0f, angularMove, 0f);
        }
    }
}