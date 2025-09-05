using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    private static InputController instance;

    [SerializeField] private PlayerInput _playerInput;

    private InputAction move;
    [SerializeField] private float _moveSpeed = 8f;
    private Vector2 moveDirection;
    private bool isMoving = false;

    private InputAction shoot;
    [SerializeField] private float _shootInterval;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    private bool isShooting = false;
    private bool alreadyShooting = false;

    private InputAction reset;
    private InputAction quit;

    private Rigidbody2D playerRB2D;

    public static InputController Instance { get => instance; set => instance = value; }

    private void Start()
    {
        instance = this;

        _playerInput.currentActionMap.Enable();
        move = _playerInput.currentActionMap.FindAction("Move");
        shoot = _playerInput.currentActionMap.FindAction("Shoot");
        reset = _playerInput.currentActionMap.FindAction("Reset");
        quit = _playerInput.currentActionMap.FindAction("Quit");

        move.started += Move_started;
        move.canceled += Move_canceled;
        shoot.started += Shoot_started;
        shoot.canceled += Shoot_canceled;
        reset.performed += Reset_performed;
        quit.performed += Quit_performed;

        playerRB2D = GameController.Instance.PlayerObject.GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        shoot.started -= Shoot_started;
        shoot.canceled -= Shoot_canceled;
        reset.performed -= Reset_performed;
        quit.performed -= Quit_performed;
    }
    private void FixedUpdate()
    {
        moveDirection = move.ReadValue<Vector2>();
        if (isMoving)
            playerRB2D.linearVelocity = moveDirection * _moveSpeed;
        else
            playerRB2D.linearVelocity = Vector2.zero;
    }
    private void Move_started(InputAction.CallbackContext obj)
    {
        isMoving = true;
    }
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }
    private void Shoot_started(InputAction.CallbackContext obj)
    {
        isShooting = true;
        playerRB2D.GetComponent<Animator>().SetBool("isShooting", true);
        if (!alreadyShooting)
            StartCoroutine(Shooting());
    }
    IEnumerator Shooting()
    {
        alreadyShooting = true;
        while (isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(_shootInterval);
        }
        alreadyShooting = false;
    }
    private void Shoot()
    {
        if (playerRB2D.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            playerRB2D.GetComponent<Animator>().Play("GrabGun");
    }
    public void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(_projectilePrefab, new Vector2(playerRB2D.transform.position.x + 0.8f, playerRB2D.transform.position.y), Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().AddForce(playerRB2D.transform.right * _projectileSpeed, ForceMode2D.Impulse);
    }
    private void Shoot_canceled(InputAction.CallbackContext obj)
    {
        isShooting = false;
        playerRB2D.GetComponent<Animator>().SetBool("isShooting", false);
    }
    private void Reset_performed(InputAction.CallbackContext obj)
    {
        GameController.Instance.ResetScene();
    }

    private void Quit_performed(InputAction.CallbackContext obj)
    {
        GameController.Instance.QuitGame();
    }

}
