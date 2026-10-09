using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

//Installing Character Controller Dependency
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Character Movement vars
    //Takes the keyboard input as V2
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    //Serialize shows it in unity inspector even though it's private
    //but doesn't let other scripts access it
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    //Sword animation
    private Animator animator;

    //Character hurt flash vars
    private MeshRenderer _characterColor;
   
    public Color _flashColor = Color.red;
    private float _flashDuration = 0.1f;
    private Color _originalColor;
    public float _damage;
    public healthBar _health;
    //Iframes
    //private bool isInvincible=false;

    //Game Over
    [SerializeField] private MainMenuManager gameOverScreen;
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        _characterColor = GetComponentInChildren<MeshRenderer>();
        _originalColor = _characterColor.material.color;
        Debug.Log(_originalColor);

    }
    //Called when the "Player" GameObject that has this script is initialized
    private void Awake()
    {
        //Used to reference the CharacterController class
        _characterController = GetComponent<CharacterController>();

        //Locks cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //if input.x & y magnitude = 0 (no input), don't rotate
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(y: _direction.x, x: _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref _currentVelocity,smoothTime);
        transform.rotation = Quaternion.Euler(x:0.0f,y:angle,z:0.0f);
        //Every frame, moves the character a certain direction*speed*time-since-last frame
        _characterController.Move(_direction*speed*Time.deltaTime);

        if (_health.health <= 0)
        {
            gameOverScreen.Show(gameOverPanel);
        }
    }
    //Context receives the info from Player Input component on when one of it's mapped actions are triggered
    public void Move(InputAction.CallbackContext context)
    {
        //Reads the input
        _input = context.ReadValue<Vector2>();
        //Turns input from V2 to V3
        _direction = new Vector3(_input.x,y: 0.0f,_input.y);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(message: "Player Attacks!");
            animator.SetTrigger("Attack");
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(message: "Player Hit!");
            Flash();
            _health.TakeDamage(_damage);
        }
    }

    private IEnumerator DoFlash()
    {
        _characterColor.material.color = _flashColor;
        yield return new WaitForSeconds(_flashDuration);
        _characterColor.material.color = _originalColor;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(DoFlash());
    }
}
