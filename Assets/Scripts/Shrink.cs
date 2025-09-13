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

    // private IEnumerator Shrink()
    // {
    //     yield return null;
    //     float duration = .3f;
    //     float elapsedTime = 0;
    //     Vector3 startScale = transform.localScale;
    //     Vector3 endScale = new Vector3(.5f, .5f, .5f);


    //     while (elapsedTime < duration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //         transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
    //     }
    //     startScale = endScale;

    //     yield return null;
    //     StartCoroutine(Enlarge());

    // }

    // private IEnumerator Enlarge()
    // {
    //     yield return null;
    //     float duration = .3f;
    //     float elapsedTime = 0;
    //     Vector3 startScale = new Vector3(.5f, .5f, .5f);
    //     Vector3 endScale = defaultScale;

    //     while (elapsedTime < duration)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //         transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
    //     }
    //     startScale = endScale;

    // }
    private IEnumerator ShrinkAndEnlargeLoop()
{
    Vector3 smallScale = new Vector3(.5f, .5f, .5f);

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
