using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControl : MonoBehaviour
{
    public static EnemyBulletControl instance;
    private List<BulletBaseClass> bulletPool;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // 从对象池中获取一个对象
    public BulletBaseClass GetBullet()
    {
        foreach (BulletBaseClass bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        return null;
        // 如果所有对象都在使用中，创建一个新的对象
        //BulletBaseClass newBullet = Instantiate(bulletPrefab);
        //bulletPool.Add(newBullet);
        //return newBullet;
    }

    // 从对象池中获取一个对象
    public BulletBaseClass GetBullet(BulletBaseClass bulletPrefab)
    {
        foreach (BulletBaseClass bullet in bulletPool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(true);
                bullet.CopyFrom(bulletPrefab);
                return bullet;
            }
        }

        // 如果所有对象都在使用中，可以选择创建一个新的对象，或者返回null
        BulletBaseClass newBullet = Instantiate(bulletPrefab);
        newBullet.transform.parent = this.transform;  // 将新创建的对象的父对象设置为对象池管理对象
        bulletPool.Add(newBullet);
        return newBullet;
    }

    // 从对象池中获取一个对象并初始化它
    public BulletBaseClass GetBulletAndInitialize(BulletBaseClass bulletPrefab, Vector3 position, Quaternion rotation)
    {
        BulletBaseClass bullet = GetBullet(bulletPrefab);
        if (bullet != null)
        {
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
        }
        return bullet;
    }
    private void Start()
    {
        bulletPool = new List<BulletBaseClass>();
    }



    // 将对象返回到对象池
    public void ReturnBullet(BulletBaseClass bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
