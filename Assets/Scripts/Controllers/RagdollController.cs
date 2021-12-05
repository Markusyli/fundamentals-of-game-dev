using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public GameObject mainModel;
    public GameObject ragdollModel;

    private Collider mainCollider;
    private Collider[] ragdollColliders;

    private void Start()
    {
        mainCollider = GetComponent<Collider>();
        mainModel.SetActive(true);
        ragdollModel.SetActive(false);
        ragdollColliders = ragdollModel.GetComponentsInChildren<Collider>(true);
    }

    public void DoRagdoll()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Animator animator = GetComponent<Animator>();
        UnityEngine.AI.NavMeshAgent navMeshAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

        if (navMeshAgent)
            navMeshAgent.enabled = false;

        ragdollModel.transform.position = mainModel.transform.position;
        ragdollModel.transform.rotation = mainModel.transform.rotation;

        CopyTransformsRecursive(mainModel, ragdollModel);

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        animator.enabled = false;
        animator.avatar = null;

        mainCollider.enabled = false;

        mainModel.SetActive(false);
        ragdollModel.SetActive(true);
    }

    private void CopyTransformsRecursive(GameObject source, GameObject destination)
    {
        for (int i = 0; i < source.transform.childCount; ++i)
        {
            var currentSourceTransform = source.transform.GetChild(i);

            for (int j = 0; j < destination.transform.childCount; ++j)
            {
                var currentDestTransform = destination.transform.GetChild(j);
                if (currentDestTransform.name == currentSourceTransform.name)
                {
                    currentDestTransform.position = currentSourceTransform.position;
                    currentDestTransform.rotation = currentSourceTransform.rotation;

                    if (currentDestTransform.childCount > 0)
                    {
                        CopyTransformsRecursive(currentSourceTransform.gameObject, currentDestTransform.gameObject);
                    }
                }
            }
        }
    }
}
