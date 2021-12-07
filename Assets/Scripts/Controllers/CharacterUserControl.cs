using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(CharacterCombat))]
public class CharacterUserControl : MonoBehaviour
{
    public Camera m_Camera;

    private Character m_Character; // A reference to the ThirdPersonCharacter on the object
    private CharacterCombat m_CharacterCombat;
    private CharacterStats characterStats;
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private bool m_Attack;
    private GameObject[] players;

    private void Awake()
    {
        SetCamera();

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<Character>();
        m_CharacterCombat = GetComponent<CharacterCombat>();
        characterStats = GetComponent<CharacterStats>();

        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        SetCamera();
        transform.position = GameObject.FindWithTag("LevelStartingPoint").transform.position;

        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 1)
        {
            Destroy(players[1]);
        }
    }

    private void SetCamera()
    {
        m_Cam = CameraManager.instance.mainCamera.transform;
    }

    private void Update()
    {
        if (!m_Jump)
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

        if (!m_Attack)
            m_Attack = Input.GetMouseButtonDown(0);
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (characterStats.isDead) return;

        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);

        if (m_Attack && !crouch)
            m_CharacterCombat.Attack();

        m_Jump = false;
        m_Attack = false;
    }
}
