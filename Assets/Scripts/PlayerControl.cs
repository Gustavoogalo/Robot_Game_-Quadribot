using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 _Player_Input;

    [Header("Player Components")]
    private Animator _Player_Animator;
    private Rigidbody _Player_Rigidbody;

    public float _Player_Speed = 5;
    public bool _Player_Isgrounded;
    public bool _Item_In = false; // New boolean to track if player has an item
    public GameObject currentItem; // Reference to the current item

    void Start()
    {
        _Player_Animator = GetComponent<Animator>();
        _Player_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Look();
        TakeInput();
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
        if (_Player_Input != Vector3.zero)
        {
            _Player_Animator.SetBool("IsMoving", true);
            _Player_Rigidbody.MovePosition(transform.position + (transform.forward * _Player_Input.magnitude) * _Player_Speed * Time.deltaTime);
        }
        else
        {
            _Player_Animator.SetBool("IsMoving", false);
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

            Destroy(currentItem); // Destroy the collected item
            Debug.Log("Item destroyed.");

            _Item_In = false; // Reset the item status
            currentItem = null; // Clear the reference to the current item
        }
    }
    #endregion
}
