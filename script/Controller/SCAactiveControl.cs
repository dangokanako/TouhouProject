using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SCAactiveControl : MonoBehaviour
{
    // 主动武器控制器
    static public SCAactiveControl instance;

    // 我草这都是啥简写鬼看得懂啊
    public ASC_IndiscriminateSword ASC_IDIS;
    public ASC_SpaceGunBullet ASC_SGB;
    public GameObject ASC_YQZG;
    public EnemyDamagerClass ASC_IF;
    public GameObject ASC_MXMZ;
    public EnemyDamagerClass ASC_TFS;

    // LilyBullet_new
    public EnemyDamagerClass ASC_LilyBullet_new;
    public EnemyDamagerClass ASC_Sharenwanou;
    public EnemyDamagerClass ASC_MarisaBullet;
    public EnemyDamagerClass starBullet;
    public EnemyDamagerClass starBullet_WithOutInitialVelocity;

    public EnemyDamagerClass ASC_Yinyangyu;
    public EnemyDamagerClass ASC_iceBullet;
    public EnemyDamagerClass ASC_LongKnives;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = MainPlayer.instance.transform.position;
    }

    // 割草镰刀
    public bool ASC_Sickle()
    {
        return true;
    }
    // 空白符卡发射子弹
    public bool ASC_NullBullet()
    {
        if (!PlayerHealthControl.instance.UseSP(0.14f, true, true))
            return false;

        // 播放音效
        SFXManger.instance.PlaySFX(4);
        // 播放动画        
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);

        ASC_SpaceGunBullet ASC_SGB_temp = Instantiate(ASC_SGB, transform.position, Quaternion.identity);
        // 朝向鼠标方向的速度
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        direction = new Vector2(direction.x, direction.y);
        direction.Normalize();
        // 方向给正负十度的偏差
        ASC_SGB_temp.rb.AddForce(direction * CalAddForceSCA(100f, 0f));

        return true;
    }
    // 起点发射子弹
    public bool ASC_SpaceGunBullet()
    {
        if (!PlayerHealthControl.instance.UseSP(0.3f, true, true))
            return false;

        // 播放音效
        SFXManger.instance.PlaySFX(4);
        // 播放动画        
        // TODO 使用鼠标物品时，这个东西不对
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);


        StartCoroutine(ASC_SpaceGunBullet_Content());

        return true;
    }
    IEnumerator ASC_SpaceGunBullet_Content()
    {
        ASC_SpaceGunBullet ASC_SGB_temp = Instantiate(ASC_SGB, transform.position, Quaternion.identity);
        // 朝向鼠标方向的速度
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;

        direction = new Vector2(direction.x, direction.y);
        direction.Normalize();

        ASC_SGB_temp.rb.AddForce(direction * CalAddForceSCA(110f, 0f));
        var direction2 = Quaternion.Euler(0, 0, -15) * direction; // 旋转-15度
        var direction3 = Quaternion.Euler(0, 0, 15) * direction; // 旋转15度


        ASC_SGB_temp = Instantiate(ASC_SGB, transform.position, Quaternion.identity);
        ASC_SGB_temp.rb.AddForce(direction2 * CalAddForceSCA(110f, 0f));

        ASC_SGB_temp = Instantiate(ASC_SGB, transform.position, Quaternion.identity);
        ASC_SGB_temp.rb.AddForce(direction3 * CalAddForceSCA(110f, 0f));

        yield return new WaitForSeconds(0.15f);
    }

    // “第一步”发射子弹
    public bool ASC_TheFristStepBullet()
    {
        if (!PlayerHealthControl.instance.UseSP(0.38f, true, true))
            return false;

        // 播放音效
        SFXManger.instance.PlaySFX(4);
        CameraControl.instance.DoShake(0.1f, 0.1f);
        // 播放动画        
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);


        var ASC_SGB_temp = Instantiate(ASC_TFS, transform.position, Quaternion.identity);
        // 朝向鼠标方向的速度
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;


        direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.2f, 0.2f), direction.y + UnityEngine.Random.Range(-0.2f, 0.2f));
        direction.Normalize();
        var direction2 = Quaternion.Euler(0, 0, -15) * direction; // 旋转-15度
        var direction3 = Quaternion.Euler(0, 0, 15) * direction; // 旋转15度
        // var direction4 = Quaternion.Euler(0, 0, -30) * direction; // 旋转-15度
        // var direction5 = Quaternion.Euler(0, 0, 30) * direction; // 旋转15度

        ASC_SGB_temp.rb.AddForce(direction * CalAddForceSCA(120f, 0f));
        ASC_SGB_temp = Instantiate(ASC_TFS, transform.position, Quaternion.identity);
        ASC_SGB_temp.rb.AddForce(direction2 * CalAddForceSCA(120f, 0f));

        ASC_SGB_temp = Instantiate(ASC_TFS, transform.position, Quaternion.identity);
        ASC_SGB_temp.rb.AddForce(direction3 * CalAddForceSCA(120f, 0f));
        // ASC_SGB_temp = Instantiate(ASC_TFS, transform.position, Quaternion.identity);
        // ASC_SGB_temp.rb.AddForce(direction4 * CalAddForceSCA(120f, 0f));
        // ASC_SGB_temp = Instantiate(ASC_TFS, transform.position, Quaternion.identity);
        // ASC_SGB_temp.rb.AddForce(direction5 * CalAddForceSCA(120f, 0f));

        return true;
    }
    // 追梦者
    public bool ASC_DreamChaser()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(0.45f))
            return false;

        // 播放音效
        SFXManger.instance.PlaySFX(13);
        CameraControl.instance.DoShake(0.1f, 0.1f);

        for (int i = 0; i < 12; i++)
        {
            // 50%概率，期望5
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                var ASC_SGB_temp = Instantiate(starBullet_WithOutInitialVelocity, transform.position, Quaternion.identity);
                // 朝向鼠标方向的速度
                Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
                direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.3f, 0.2f), direction.y + UnityEngine.Random.Range(-0.3f, 0.3f));
                // 随机颜色
                string[] bulletPath = new string[] { "bullet/marisa/blue", "bullet/marisa/blue2", "bullet/marisa/green", "bullet/marisa/greenblue", "bullet/marisa/yellow", "bullet/marisa/yellow2", "bullet/marisa/red", "bullet/marisa/red2" };
                ASC_SGB_temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(bulletPath[UnityEngine.Random.Range(0, bulletPath.Length)]);

                direction.Normalize();
                // 方向给正负十度的偏差
                ASC_SGB_temp.rb.AddForce(direction * CalAddForceSCA(130f, 0.3f));
            }
        }


        return true;
    }



    // 操弄玩偶
    public bool SCActive_ASC_IndiscriminateSword()
    {
        // 扣取SP费用中
        if (!PlayerHealthControl.instance.UseSP(1.0f))
            return false;
        // 播放音效
        SFXManger.instance.PlaySFX(4);

        // UIControl.instance.UI_SCActive(2);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);
        Instantiate(ASC_IDIS, transform.position, Quaternion.identity);

        return true;
    }
    // 梦想妙珠
    public bool SCActive_ASC_Mengxiangmiaozhu()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.0f))
            return false;


        // 播放音效
        SFXManger.instance.PlaySFX(14, 5f);



        var temp = Instantiate(ASC_MXMZ, transform.position, Quaternion.identity);
        temp.SetActive(true);
        StartCoroutine(DelayDestroy(temp, 10f));
        return true;
    }
    // 梦想封印
    internal bool SCActive_ASC_Mengxiangfengyin()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(PlayerHealthControl.instance.maxSP * 0.3f))
            return false;

        // 播放音效
        SFXManger.instance.PlaySFX(14, 5f);
        CameraControl.instance.DoShake(0.2f, 0.2f);


        var temp = Instantiate(ASC_MXMZ, transform.position, Quaternion.identity);

        // 修改受力
        // 获取 ParticleSystem 组件
        ParticleSystem particleSystem = temp.GetComponent<ParticleSystem>();

        // 获取 forceOverLifetime 模块
        ParticleSystem.ForceOverLifetimeModule forceOverLifetime = particleSystem.forceOverLifetime;

        // 修改 x 轴方向的力

        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        direction.Normalize();

        forceOverLifetime.x = direction.x * 3;
        forceOverLifetime.y = direction.y * 3;

        // 增多发射数量
        ParticleSystem.EmissionModule emission = particleSystem.emission;
        emission.rateOverTime = 7;

        temp.SetActive(true);
        StartCoroutine(DelayDestroy(temp, 10f));

        return true;
    }

    // 冰瀑冰符「Icicle Fall」
    public bool SCActive_ASC_IcicleFall()
    {
        // 屏幕震动
        CameraControl.instance.DoShake(1.0f, 0.05f);
        // 音效
        SFXManger.instance.PlaySFX(5);


        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(0.5f))
            return false;

        // 向鼠标方向批量随机生成若干实体
        for (int i = 0; i < 18; i++)
        {

            var temp = Instantiate(ASC_IF, transform.position, Quaternion.identity);
            // 第一步，将实体朝向鼠标方向
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
            direction.Normalize();
            direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.3f, 0.3f), direction.y + UnityEngine.Random.Range(-0.3f, 0.3f));

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
            temp.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);

            // 第二步，加速

            temp.rb.velocity = direction * 5f * UnityEngine.Random.Range(0.8f, 1.2f);
        }

        return true;
    }

    // 夜雀
    public bool SCActive_ASC_Yequezhige()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.0f))
            return false;

        var temp = Instantiate(ASC_YQZG, transform.position, Quaternion.identity, MainPlayer.instance.transform);

        StartCoroutine(DelayDestroy(temp, 5f));


        return true;
    }
    // 境界线
    public bool SCActive_ASC_Jingjiexian()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.8f))
            return false;
        CameraControl.instance.DoShake(0.2f, 0.2f);
        var temp = Instantiate(ASC_YQZG, transform.position, Quaternion.identity, MainPlayer.instance.transform);


        // 播放音效
        SFXManger.instance.PlaySFX(14, 5f);


        StartCoroutine(DelayDestroy(temp, 5f));
        StartCoroutine(SCActive_ASC_Jingjiexian_Content());


        return true;
    }
    private IEnumerator SCActive_ASC_Jingjiexian_Content()
    {
        // 在身边创建360度的ASC_LilyBullet_new弹幕
        for (int i = 0; i < 1080; i += 10)
        {
            var temp2 = Instantiate(ASC_LilyBullet_new, transform.position, Quaternion.identity);
            temp2.transform.rotation = Quaternion.Euler(0f, 0f, i);
            temp2.lifeTime = 2f;
            temp2.damageAmount = 3;
            // 给子弹一个向前的速度
            temp2.rb.velocity = temp2.transform.up * 2f;


            var temp = Instantiate(ASC_LilyBullet_new, transform.position, Quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0f, 0f, -i);
            temp.lifeTime = 2f;
            temp.damageAmount = 3;
            // 给子弹一个向前的速度
            temp.rb.velocity = temp.transform.up * 2f;

            // 等待0.5秒
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator DelayDestroy(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    // 钻石风暴
    internal bool SCActive_ASC_Zuanshifengbao()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(2.5f))
            return false;

        StartCoroutine(SCActive_ASC_Zuanshifengbao_Content());

        return true;
    }
    private IEnumerator SCActive_ASC_Zuanshifengbao_Content()
    {
        // CameraControl.instance.StartCoroutine(CameraControl.instance.Shake(3.8f, 0.01f));
        CameraControl.instance.DoShake(7.5f, 0.15f);

        // 向鼠标方向批量随机生成若干实体
        for (int i = 0; i < 140; i++)
        {
            var temp = Instantiate(ASC_IF, transform.position, Quaternion.identity);
            // 第一步，将实体朝向鼠标方向
            Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
            direction.Normalize();
            direction = new Vector2(direction.x + UnityEngine.Random.Range(-0.3f, 0.3f), direction.y + UnityEngine.Random.Range(-0.3f, 0.3f));

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 计算旋转角度
            temp.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);

            // 第二步，加速

            temp.rb.velocity = direction * 4f * UnityEngine.Random.Range(0.8f, 1.2f) * (2f - (float)i / 100f);

            if (i % 2 == 0)
                yield return new WaitForSeconds(0.05f);
        }
    }



    // 楼观剑 仮
    internal bool SCActive_ASC_Louguanjian()
    {
        // 计算武器通用CD
        if (MainPlayer.instance.WeaponCDCount > 0)
            return false;

        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(0.4f))
            return false;

        // 武器CD
        MainPlayer.instance.WeaponCDCount = 0.4f;

        // 播放角色物品动画
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);

        // 播放音效
        SFXManger.instance.PlaySFX(9);


        // 在鼠标位置播放动画
        // 鼠标位置只能在自身的范围内，如果在范围外，那么距离将会被限制在范围内
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        if (direction.magnitude > 1.2)
        {
            direction.Normalize();
            direction *= 1.2f;
        }

        // 根据鼠标位置旋转播放动画
        Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);

        // 将direction转换回世界坐标
        DestoryAnimeControl.instance.CreateDestoryAnime(4, (Vector2)MainPlayer.instance.transform.position + direction, quaternion);

        return true;
    }

    // YOUMU_BLADE1 仮 人界剑「悟入幻想」
    internal bool SCActive_ASC_Wuruhuanxiang()
    {
        // 计算武器通用CD
        if (MainPlayer.instance.WeaponCDCount > 0)
            return false;

        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(0.65f))
            return false;

        // 武器CD
        MainPlayer.instance.WeaponCDCount = 1.1f;

        // 播放角色物品动画
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage, 1.2f);

        // 播放音效
        SFXManger.instance.PlaySFX(10);

        // 在鼠标位置播放动画
        // 鼠标位置只能在自身的范围内，如果在范围外，那么距离将会被限制在范围内
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        if (direction.magnitude > 1.6)
        {
            direction.Normalize();
            direction *= 1.6f;
        }

        // 根据鼠标位置旋转播放动画
        Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);

        // 将direction转换回世界坐标
        DestoryAnimeControl.instance.CreateDestoryAnime(5, (Vector2)MainPlayer.instance.transform.position + direction, quaternion);

        return true;
    }
    // YOUMU_BLADE1 真 人界剑「悟入幻想」
    internal bool SCActive_ASC_Wuruhuanxiang_Shin()
    {
        CameraControl.instance.DoShake(0.2f, 0.2f);


        // 计算武器通用CD
        if (MainPlayer.instance.WeaponCDCount > 0)
            return false;

        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(0.8f))
            return false;

        // 武器CD
        MainPlayer.instance.WeaponCDCount = 1.2f;

        // 播放角色物品动画
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage, 1.2f);

        // 播放音效
        SFXManger.instance.PlaySFX(10);

        // 在鼠标位置播放动画
        // 鼠标位置只能在自身的范围内，如果在范围外，那么距离将会被限制在范围内
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        if (direction.magnitude > 2.1)
        {
            direction.Normalize();
            direction *= 2.1f;
        }

        // 根据鼠标位置旋转播放动画
        Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);

        // 将direction转换回世界坐标
        DestoryAnimeControl.instance.CreateDestoryAnime(8, (Vector2)MainPlayer.instance.transform.position + direction, quaternion);

        return true;
    }

    // 长刀
    // internal bool SCActive_ASC_LongKnives()
    // {
    //     // 计算武器通用CD
    //     if (MainPlayer.instance.WeaponCDCount > 0)
    //         return false;

    //     // 扣取SP费用
    //     if (!PlayerHealthControl.instance.UseSP(0.1f))
    //         return false;

    //     // 武器CD
    //     MainPlayer.instance.WeaponCDCount = 1.2f;

    //     // 播放音效
    //     SFXManger.instance.PlaySFX(10);

    //     // 在鼠标位置播放动画
    //     // 鼠标位置只能在自身的范围内，如果在范围外，那么距离将会被限制在范围内
    //     Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
    //     // 如果鼠标在MainPlayer的左边，那么旋转图片180度


    //     if (direction.magnitude > 0.5f)
    //     {
    //         direction.Normalize();
    //         direction *= 0.5f;
    //     }

    //     var temp2 = Instantiate(ASC_LongKnives, (Vector2)MainPlayer.instance.transform.position + direction, Quaternion.identity, MainPlayer.instance.transform);

    //     if (direction.x < 0)
    //     {
    //         temp2.transform.rotation = Quaternion.Euler(0, 180, 0);
    //         temp2.transform.DORotate(new Vector3(0, 180, -90), 0.8f);
    //     }
    //     else
    //     {
    //         temp2.transform.DORotate(new Vector3(0, 0, -90), 0.8f);
    //     }


    //     return true;
    // }
    // 杀人玩偶
    internal bool SCActive_ASC_Sharenwanou()
    {
        // 扣取SP费用中
        if (!PlayerHealthControl.instance.UseSP(2.0f))
            return false;

        // 播放音效
        SFXManger.instance.PlaySFX(4);

        StartCoroutine(SCActive_ASC_ASC_Sharenwanou_Content());

        return true;
    }
    private IEnumerator SCActive_ASC_ASC_Sharenwanou_Content()
    {
        CameraControl.instance.DoShake(1.0f, 0.05f);


        // 在身边创建360度的ASC_Sharenwanou弹幕
        for (int i = 0; i < 1080; i += 10)
        {

            var temp2 = Instantiate(ASC_Sharenwanou, transform.position, Quaternion.identity);
            temp2.transform.rotation = Quaternion.Euler(0f, 0f, i + UnityEngine.Random.Range(1, 10));
            temp2.lifeTime = 1.3f;
            temp2.damageAmount = 3;
            // 给子弹一个向前的速度
            temp2.rb.velocity = temp2.transform.up * 2f;
            temp2.moveSpeedAdd = 0.005f;



            var temp = Instantiate(ASC_Sharenwanou, transform.position, Quaternion.identity);
            temp.transform.rotation = Quaternion.Euler(0f, 0f, i - 10);
            temp.lifeTime = 1.0f;
            temp.damageAmount = 3;
            // 给子弹一个向前的速度
            temp.rb.velocity = temp.transform.up * 2f;
            temp.moveSpeedAdd = 0.015f;


            SFXManger.instance.PlaySFX(29);

            // 等待0.05秒
            yield return new WaitForSeconds(0.05f);
        }
    }


    // 魔符「Stardust Reverie」 


    // 阴阳玉系列
    public bool SCActive_ASC_Yinyangyu_1()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.0f))
            return false;

        // 播放动画        
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);

        CameraControl.instance.DoShake(0.1f, 0.1f);

        // 播放音效
        SFXManger.instance.PlaySFX(20);

        // 生成一个子弹
        var bulletObj = Instantiate(ASC_Yinyangyu, MainPlayer.instance.transform.position, Quaternion.identity);

        // // 将子弹的方向设置为鼠标的方向
        // 获取鼠标的位置
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        direction.Normalize();

        // 设置速度
        bulletObj.rb.mass = 40f;
        bulletObj.rb.AddForce(direction * bulletObj.rb.mass * CalAddForceSCA(90f, 0.2f));
        bulletObj.lifeTime = 10f;
        bulletObj.damageAmount = 6;
        bulletObj.damageType = 1;

        return true;
    }

    public bool SCActive_ASC_Yinyangyu_2()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.5f))
            return false;

        // 播放动画        
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);

        CameraControl.instance.DoShake(0.3f, 0.3f);

        // 播放音效
        SFXManger.instance.PlaySFX(20);

        // 生成一个子弹
        var bulletObj = Instantiate(ASC_Yinyangyu, MainPlayer.instance.transform.position, Quaternion.identity);

        // // 将子弹的方向设置为鼠标的方向
        // 获取鼠标的位置
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        direction.Normalize();

        // 设置速度
        bulletObj.rb.mass = 70f;
        bulletObj.rb.AddForce(direction * bulletObj.rb.mass * CalAddForceSCA(120f, 0.2f));
        bulletObj.lifeTime = 10f;
        bulletObj.damageAmount = 9;
        bulletObj.damageType = 1;

        return true;
    }
    public bool SCActive_ASC_Yinyangyu_3()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.3f))
            return false;

        // 播放动画        
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);

        CameraControl.instance.DoShake(0.2f, 0.2f);

        // 播放音效
        SFXManger.instance.PlaySFX(20);

        // 生成一个子弹
        var bulletObj = Instantiate(ASC_Yinyangyu, MainPlayer.instance.transform.position, Quaternion.identity);

        // // 将子弹的方向设置为鼠标的方向
        // 获取鼠标的位置
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)MainPlayer.instance.transform.position;
        direction.Normalize();

        // 设置速度
        bulletObj.rb.mass = 90f;
        bulletObj.rb.AddForce(direction * bulletObj.rb.mass * CalAddForceSCA(140f, 0.2f));
        bulletObj.lifeTime = 10f;
        bulletObj.damageAmount = 12;
        bulletObj.damageType = 1;

        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="baseFroce">基础力，具体要看本体重量</param>
    /// <param name="FroceRange">浮动力度，0.2f即可</param>
    /// <returns></returns>
    private float CalAddForceSCA(float baseFroce, float FroceRange)
    {
        return baseFroce * UnityEngine.Random.Range(1f - FroceRange, 1f + FroceRange) * (1f + PlayerHealthControl.instance.BulletSpeedRate);
    }
    // 魔理沙的信物
    public void SCActive_MarisaMono()
    {
        if (GlobalControl.instance.teammate == 1)
            BubbleTalkControl.instance.ShowBubbleTalk(new BubbleTalkData("marisa_small_10", "援护攻击~！"));

        StartCoroutine(SCActive_MarisaMono_Content());
    }
    private IEnumerator SCActive_MarisaMono_Content()
    {
        for (int i = 0; i < (GlobalControl.instance.teammate == 1 ? 20 : 10); i++)
        {
            SFXManger.instance.PlaySFX(26, default, false);
            // 生成一个子弹 随机方向
            var bulletObj = Instantiate(starBullet, PlayerHealthControl.instance.transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));
            bulletObj.initialVelocity *= UnityEngine.Random.Range(0.7f, 1.3f);
            // 随机颜色
            string[] bulletPath = new string[] { "bullet/marisa/blue", "bullet/marisa/blue2", "bullet/marisa/green", "bullet/marisa/greenblue", "bullet/marisa/yellow", "bullet/marisa/yellow2", "bullet/marisa/red", "bullet/marisa/red2" };
            bulletObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(bulletPath[UnityEngine.Random.Range(0, bulletPath.Length)]);
            // 随机大小
            float randomSize = UnityEngine.Random.Range(0.5f, 2f);
            bulletObj.transform.localScale = new Vector3(randomSize, randomSize, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }
    // 琪露诺的信物
    public void SCActive_CrinoMono()
    {
        StartCoroutine(SCActive_CrinoMono_Content());
    }
    private IEnumerator SCActive_CrinoMono_Content()
    {
        for (int i = 0; i < 4; i++)
        {
            SFXManger.instance.PlaySFX(26);
            for (int j = 0; j < 3; j++)
            {
                var bulletObj = Instantiate(ASC_iceBullet, PlayerHealthControl.instance.transform.position, Quaternion.Euler(0f, 0f, j * 120 + i * 60));
            }
            yield return new WaitForSeconds(0.4f);
        }
    }
    // 137
    public bool SCActive_ASC_137()
    {
        // 扣取SP费用
        if (!PlayerHealthControl.instance.UseSP(1.5f))
            return false;

        // 播放动画        
        MainPlayer.instance.PlayWeaponAnime(ItemControl.instance.GetChooseItem().itemImage);

        CameraControl.instance.DoShake(0.1f, 0.1f);

        // 播放音效
        //  SFXManger.instance.PlaySFX(20);

        StartCoroutine(SCActive_ASC_137_Content());
        return true;
    }
    private IEnumerator SCActive_ASC_137_Content()
    {
        // 在身边创建360度的ASC_LilyBullet_new弹幕
        for (int i = 0; i < 1080; i += 10)
        {
            var temp2 = Instantiate(ASC_LilyBullet_new, transform.position, Quaternion.Euler(0f, 0f, 90));
            temp2.transform.rotation = Quaternion.Euler(0f, 0f, i);
            temp2.lifeTime = 5f;
            temp2.damageAmount = 3;
            temp2.moveSpeedAdd = -0.12f;
            temp2.initialVelocity = 6f;
            temp2.hasInitialVelocity = true;


            var temp = Instantiate(ASC_LilyBullet_new, transform.position, Quaternion.Euler(0f, 0f, 90));
            temp.transform.rotation = Quaternion.Euler(0f, 0f, i + 90);
            temp.lifeTime = 5f;
            temp.damageAmount = 3;
            temp.moveSpeedAdd = -0.12f;
            temp.initialVelocity = 6f;
            temp.hasInitialVelocity = true;

            var temp3 = Instantiate(ASC_LilyBullet_new, transform.position, Quaternion.Euler(0f, 0f, 90));
            temp3.transform.rotation = Quaternion.Euler(0f, 0f, i + 180);
            temp3.lifeTime = 5f;
            temp3.damageAmount = 3;
            temp3.moveSpeedAdd = -0.12f;
            temp3.initialVelocity = 6f;
            temp3.hasInitialVelocity = true;

            var temp4 = Instantiate(ASC_LilyBullet_new, transform.position, Quaternion.Euler(0f, 0f, 90));
            temp4.transform.rotation = Quaternion.Euler(0f, 0f, i + 270);
            temp4.lifeTime = 5f;
            temp4.damageAmount = 3;
            temp4.moveSpeedAdd = -0.12f;
            temp4.initialVelocity = 6f;
            temp4.hasInitialVelocity = true;

            // 等待0.5秒
            yield return new WaitForSeconds(0.05f);
        }
    }

    private Vector2 GetDirectionToMouseAfterNormalize()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = MainPlayer.instance.transform.position;
        return (mouseWorldPosition - playerPosition).normalized;
    }
}
