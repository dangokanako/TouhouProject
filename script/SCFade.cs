using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SCFade : MonoBehaviour
{

    Graphic image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Graphic>();
        StartCoroutine(DestorySeft());
        image.CrossFadeAlpha(0f, 2f, true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up;

    }

    public IEnumerator DestorySeft()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
