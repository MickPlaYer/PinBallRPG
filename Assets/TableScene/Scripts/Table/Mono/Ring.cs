using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localScale *= 1.1f;
        if (transform.localScale.x > 30f)
            gameObject.SetActive(false);
    }
}
