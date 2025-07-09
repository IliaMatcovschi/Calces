using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public float ground_drag;
    [SerializeField] float rotation_speed;

    public float jump_force;
    public float jump_cooldown;
    [SerializeField] float air_multiplier;
    [SerializeField] float air_drag;
    [SerializeField] float velLimit;
    bool ready_to_jump;

    [SerializeField] float player_height;
    [SerializeField] LayerMask what_is_ground;
    [SerializeField] bool grounded;


    [SerializeField] Transform Player_body;
    [SerializeField] Transform Player_orientation;

    [SerializeField] Transform SpawnPoint;

    public float horizontal_input;
    public float vertical_input;

    public Vector3 Move_direction;

    Rigidbody Player_rb;

    [SerializeField] AudioSource Steps;
    [SerializeField] AudioSource JumpSound;
    [SerializeField] AudioSource LandingSound;

    [SerializeField] ÑharacterStats Stats;

    private void Start()
    {
        Cursor.visible = false;
        Player_rb = GetComponent<Rigidbody>();
        Player_rb.freezeRotation = true;
        grounded = true;
        ready_to_jump = true;
    }

    private void Update()
    {
        if (Stats.hp <= 0)
        {
            transform.position = SpawnPoint.position;
            Stats.hp = Stats.maxHP;
        }

        grounded = Physics.Raycast(Player_body.position, Vector3.down, player_height * 0.7f, what_is_ground);

        MyInput();

        if (grounded)
            Player_rb.drag = ground_drag;
        else
            Player_rb.drag = air_drag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && ready_to_jump && grounded)
        {
            ready_to_jump = false;

            Jump();

            Invoke(nameof(ResetJump), jump_cooldown);
        }
    }

    void MovePlayer()
    {
            Move_direction = Player_orientation.forward * vertical_input + Player_orientation.right * horizontal_input;

            if (Move_direction != Vector3.zero && grounded)
            {
                Steps.enabled = true;
            }
            else
            {
                Steps.enabled = false;
            }

            if (grounded && Player_rb.velocity.magnitude < velLimit)
            {
                Player_rb.AddForce(Move_direction.normalized * movespeed, ForceMode.Force);
            }
            else if (Player_rb.velocity.magnitude < 7)
                Player_rb.AddForce(Move_direction.normalized * movespeed, ForceMode.Force);

    }

    void Jump()
    {
        JumpSound.Play();

        Player_rb.velocity = new Vector3(Player_rb.velocity.x, 0f, Player_rb.velocity.z);

        Player_rb.AddForce(transform.up * jump_force, ForceMode.Impulse);
    }

    void ResetJump()
    {
        ready_to_jump = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            LandingSound.Play();
    }
}
