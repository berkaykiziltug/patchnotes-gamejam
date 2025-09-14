using System.Collections;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    private Vector3 defaultScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultScale = transform.localScale;
        // StartCoroutine(Shrink());
        StartCoroutine(ShrinkAndEnlargeLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator ShrinkAndEnlargeLoop()
{
    Vector3 smallScale = new Vector3(.5f, .5f, .5f);

    while (true)
    {
         
            float waitBeforeShrink = Random.Range(10f, 15f); 
            yield return new WaitForSeconds(waitBeforeShrink);

            // Shrink
            float shrinkDuration = Random.Range(0.2f, 1f); // random shrink speed
            yield return ScaleOverTime(transform.localScale, smallScale, shrinkDuration);
            
            float waitBeforeEnlarge = Random.Range(2f, 10f);
            yield return new WaitForSeconds(waitBeforeEnlarge);

            // Enlarge
            float enlargeDuration = Random.Range(0.2f, 1f); //Random enlarge speed.
            yield return ScaleOverTime(transform.localScale, defaultScale, enlargeDuration);
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
        transform.localScale = endScale;
}
}
