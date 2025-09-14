using System.Collections;
using UnityEngine;

public class VerticalShrink : MonoBehaviour
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
        Vector3 smallScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        Vector3 defaultScale = transform.localScale;

        while (true)
        {
            float waitBeforeShrink = Random.Range(2f, 4);
            yield return new WaitForSeconds(waitBeforeShrink);
            // Shrink
            yield return ScaleOverTime(transform.localScale, smallScale, waitBeforeShrink);

            float waitBeforeEnlarge = Random.Range(2f, .8f);
            yield return new WaitForSeconds(waitBeforeEnlarge);

            // Enlarge
            yield return ScaleOverTime(transform.localScale, defaultScale, waitBeforeEnlarge);
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
