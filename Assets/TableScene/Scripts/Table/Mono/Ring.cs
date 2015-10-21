using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Biger and biger.
        transform.localScale *= 1.1f;
        if (IsTooBig)
            gameObject.SetActive(false);
    }

    private bool IsTooBig
    {
        get { return transform.localScale.x > 30f; }
    }
}
