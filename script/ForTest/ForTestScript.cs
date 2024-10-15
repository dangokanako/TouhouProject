using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthControl.instance.OnPeace = false;
        GlobalControl.instance.isBattle = true;
        GlobalControl.instance.isCloseCreateEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
