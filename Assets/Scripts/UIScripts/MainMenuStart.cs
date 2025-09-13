using UnityEngine;

public class MainMenuStart : MonoBehaviour
{

    void OnEnable()
    {
        PlayerController.Instance.canMove = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enabled && Input.GetKeyDown(KeyCode.Tab))
        {
            gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        PlayerController.Instance.canMove = true;
    }
}
