
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool canOpenMenu;
    public static bool isSecondPhase;
    public static bool isThirdPhase;
    [SerializeField] private GameObject playerUIGO;
    [SerializeField] private GameObject menuGO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        isSecondPhase = false;
        isThirdPhase = false;
        canOpenMenu = true;

        playerUIGO.SetActive(false);
        menuGO.SetActive(true);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
