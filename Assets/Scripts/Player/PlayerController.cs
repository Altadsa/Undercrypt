using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRb;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Transform _groundCheck;

    IAxisInput _inputs;
    PlayerMovement _playerMovement;
    PlayerJump _playerJump;

    void Awake()
    {
        _inputs = new PlayerInput();
        _playerMovement = new PlayerMovement(_inputs, _playerRb, _playerAnimator);
        _playerJump = new PlayerJump(_playerRb, _playerAnimator, _groundCheck);
    }

    void Update()
    {
        _inputs.ReadInput();
        _playerMovement.Update();
        _playerJump.Update();

        if (Input.GetMouseButtonDown(0))
        {
            _playerAnimator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
    }

    private void LateUpdate()
    {
        _playerMovement.UpdatePlayerDirection();
    }

}