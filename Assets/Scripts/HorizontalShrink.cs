using System.Collections;
using UnityEngine;

public class HorizontalShrink : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         StartCoroutine(ShrinkAndEnlargeLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
       private IEnumerator ShrinkAndEnlargeLoop()
{
        Vector3 smallScale = new Vector3(transform.localScale.x, transform.localScale.y,.2f);
        Vector3 defaultScale = transform.localScale;

    while (true)
        {
            // Shrink
            yield return ScaleOverTime(transform.localScale, smallScale, 0.3f);

            // Enlarge
            yield return ScaleOverTime(transform.localScale, defaultScale, 0.3f);
        }
}

private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
{
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
        yield return null;
    }
}
}
