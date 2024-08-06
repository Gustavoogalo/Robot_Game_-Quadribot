using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 _Player_Input;

    [Header("Player Components")]
    private Animator _Player_Animator;
    private Rigidbody _Player_Rigidbody;

    public float _Player_Speed = 5;
    public bool _Player_Isgrounded;
    public bool wallAhead;
    public bool _Item_In = false; // New boolean to track if player has an item
    public GameObject currentItem; // Reference to the current item


    [SerializeField] private LayerMask layerMask;

    void Start()
    {
        _Item_In = false;
        _Player_Animator = GetComponentInChildren<Animator>();
        _Player_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Look();
        TakeInput();
        VerifyWall();

    }

    private void FixedUpdate()
    {
        Walking();
    }

    #region Moviment
    void TakeInput() //pega os inputs de movimentaçcao da Unity, a base para os movimentos
    {
        _Player_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look() //aqui configuramos para onde o Player Olha ao se movimentar
    {
        if (_Player_Input != Vector3.zero)
        {
            var relative = (transform.position + _Player_Input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    void Walking() //usado para verificar se estamos nos movendo e caso sim, nos movemos
    {
        if (wallAhead == false && _Player_Input != Vector3.zero)
        {
            _Player_Animator = GetComponentInChildren<Animator>();
            _Player_Animator.SetBool("IsMoving", true);
            _Player_Rigidbody.MovePosition(transform.position + (transform.forward * _Player_Input.magnitude) * _Player_Speed * Time.deltaTime);
        }
        else
        {
            _Player_Animator = GetComponentInChildren<Animator>();
            _Player_Animator.SetBool("IsMoving", false);
        }
    }

    void VerifyWall()
    {


        if (Physics.Raycast(transform.position, transform.forward, 1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);

            wallAhead = true;
        }
        else
        {
            wallAhead = false;
        }




    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _Player_Isgrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _Player_Isgrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery") && _Item_In)
        {
            Debug.Log("Player collided with Battery.");
            BatteryScript batteryScript = other.GetComponent<BatteryScript>();
            if (batteryScript != null)
            {
                batteryScript.IncrementBattery();
                Debug.Log("Battery points incremented.");
            }
            else
            {
                Debug.LogWarning("BatteryScript not found on Battery.");
            }
            if (!currentItem.CompareTag("item_03"))
            {
                Destroy(currentItem); // Destroy the collected item
                Debug.Log("Item destroyed.");

            }

            _Item_In = false; // Reset the item status
            currentItem = null; // Clear the reference to the current item
        }


    }
    #endregion
}
