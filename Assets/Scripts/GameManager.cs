using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isSecondPhase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSecondPhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
