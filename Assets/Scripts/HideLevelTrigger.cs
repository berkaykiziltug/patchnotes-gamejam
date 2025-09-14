using UnityEngine;

public class HideLevelTrigger : MonoBehaviour
{

    private bool canHideLevel;
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private GameObject secondLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canHideLevel = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController player) && canHideLevel)
        {
            canHideLevel = false;
            firstLevel.gameObject.SetActive(false);
            secondLevel.gameObject.SetActive(true);

        }
    }
}
