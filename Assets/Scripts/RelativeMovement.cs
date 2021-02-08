using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private CharacterController _characterController;
    private float _vertSpeed;
    private ControllerColliderHit _contact;

    private Animator _animator;

    public float RotationSpeed = 15f;
    public float MoveSpeed = 6f;
    public float Gravity = -9.81f;
    public float JumpSpeed = 15f;
    public float TerminalVelocity = -10;
    public float MinFall = -1.5f;

    public float PushForce = 3.0f;

    private void Start()
    {
        _vertSpeed = MinFall;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        var hitGround = false;
        RaycastHit hit;

        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (_characterController.height + _characterController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        var movement = Vector3.zero;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            movement.x = horizontalInput * MoveSpeed;
            movement.z = verticalInput * MoveSpeed;
            movement = Vector3.ClampMagnitude(movement, MoveSpeed);

            var tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, RotationSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = JumpSpeed;
            }
            else
            {
                _vertSpeed = MinFall;
                _animator.SetBool("Jumping", false);
            }
        }
        else
        {
            _vertSpeed += Gravity * Time.deltaTime * 5;
            if (_vertSpeed < TerminalVelocity)
            {
                _vertSpeed = TerminalVelocity;
            }

            if (_contact != null)
            {
                _animator.SetBool("Jumping", true);
            }

            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                { 
                    movement = _contact.normal * MoveSpeed; 
                } 
                else 
                { 
                    movement += _contact.normal * MoveSpeed; 
                }
            }
        }

        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _characterController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) 
    { 
        _contact = hit;

        var rigidBody = hit.gameObject.GetComponent<Rigidbody>();
        if (rigidBody != null && !rigidBody.isKinematic)
        {
            rigidBody.velocity = hit.moveDirection * PushForce;
        }
    }
}
