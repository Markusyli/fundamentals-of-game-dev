using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject player => GameObject.FindGameObjectsWithTag("Player").First();
}
