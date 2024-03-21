using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [field: SerializeField] public GameObject PlayerRef {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManager();
        }
    }

    public void InitializeManager()
    {
        if (PlayerRef == null)
        {
            PlayerRef = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {

    }

    public void SetPlayerObject(GameObject newPlayerObject)
    {
        if (PlayerRef != null)
        {
            Destroy(PlayerRef);
        }

        PlayerRef = newPlayerObject;
    }
}
