using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isSecondPhase;
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
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
