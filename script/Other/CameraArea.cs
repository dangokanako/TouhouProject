using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
    public CameraBounds cameraBounds;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("MainCamera"))
        {
            if (CameraFather.instance.cameraBounds == null)
            {
                CameraFather.instance.SetCameraBounds(cameraBounds);
            }


            if (CameraFather.instance.cameraBounds.minX == this.cameraBounds.minX)
                return;

            Debug.Log("加载相机碰撞地图:" + cameraBounds.remark);
            CameraFather.instance.SetCameraBounds(cameraBounds);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            if (CameraFather.instance.cameraBounds == null)
            {
                CameraFather.instance.SetCameraBounds(cameraBounds);
            }


            if (CameraFather.instance.cameraBounds.minX == this.cameraBounds.minX)
                return;


            Debug.Log("加载相机碰撞地图:" + cameraBounds.remark);
            CameraFather.instance.SetCameraBounds(cameraBounds);
        }
    }
}
