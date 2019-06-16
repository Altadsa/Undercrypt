using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRb;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Transform _groundCheck;
    [SerializeField] PlayerSword _playerSword;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpPower;

    IAxisInput _inputs;
    PlayerMovement _playerMovement;
    PlayerJump _playerJump;
    PlayerCombat _playerCombat;

    void Awake()
    {
        _inputs = new PlayerInput();
        _playerMovement = new PlayerMovement(_inputs, _playerRb, _playerAnimator, _movementSpeed);
        _playerJump = new PlayerJump(_playerRb, _playerAnimator, _groundCheck, _jumpPower);
        _playerCombat = new PlayerCombat(_playerSword, _playerAnimator);
    }

    void Update()
    {
        _inputs.ReadInput();
        _playerMovement.Update();
        _playerJump.Update();
        _playerCombat.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetComponentInParent<PlayerInventory>().UseActiveItem();
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

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var collisionContact in collision.contacts)
        {
            Debug.DrawRay(collisionContact.point, collisionContact.normal * 10f, Color.blue, 10);
        }
    }

}