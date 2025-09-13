using UnityEngine;
using UnityEngine.EventSystems;

public class KerimProfile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string url = "https://www.linkedin.com/in/azizkerimkocak/";
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

