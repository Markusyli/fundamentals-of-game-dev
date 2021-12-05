using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterStats))]
public class CharacterAIControl : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Character character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for
    public Transform lookPoint;
    public float lookRadius = 10f;

    private bool isActive = true;
    private PlayerManager playerManager;
    private CharacterCombat characterCombat;
    private CharacterStats characterStats;

    private Character playerCharacter;
    private CharacterStats playerStats;

    private void Awake()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();
        characterCombat = GetComponent<CharacterCombat>();
        characterStats = GetComponent<CharacterStats>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        playerManager = PlayerManager.instance;
        GameObject player = playerManager.player;

        playerCharacter = player.GetComponent<Character>();
        playerStats = player.GetComponent<CharacterStats>();

        // Default the lookpoint to root transform
        if (lookPoint == null)
            lookPoint = transform;

        if (target == null)
            target = player.transform;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (characterStats.isDead)
            return;

        if (target != null)
        {
            float lookDistance = Vector3.Distance(lookPoint.position, target.position) / playerCharacter.Visibility();
            float physicalDistance = Vector3.Distance(transform.position, target.position);

            if (lookDistance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (physicalDistance <= agent.stoppingDistance && !playerStats.isDead)
                {
                    characterCombat.Attack();

                    FaceTarget();
                }
            }
        }

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }

    private void OnDrawGizmosSelected()
    {
        if (lookPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lookPoint.position, lookRadius);
    }
}
