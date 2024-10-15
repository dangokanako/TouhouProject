using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ASC_Laser : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float colorIntensity = 4f;//HDR 
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;
    [SerializeField] private float thickness = 10;// 强度
    [Header("噪声速度，一般不用调整")]
    [SerializeField] private float noiseScale = 3;
    [Header("最大长度")]
    [SerializeField] public float maxLength;
    [Header("是否能够消弹")]
    [SerializeField] private bool canDestroyBullet;
    [Header("伤害值")]
    [SerializeField] private float damage;
    [Header("攻击间隔")]
    [SerializeField] private float attackInterval;
    private float attackIntervalCount;
    [Header("发射特效")]
    [SerializeField] private ParticleSystem specificParticleSystem;
    public Vector2 currentDirection;
    [Header("旋转速度")]
    [SerializeField] public float rotationSpeed;

    private LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        lineRenderer.material.color = color * colorIntensity;
        lineRenderer.material.SetFloat("_LaserThickness", thickness);
        lineRenderer.material.SetFloat("_LaserScale", noiseScale);


        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            Renderer r = ps.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor", color * (colorIntensity + 1));
        }

        // 获取鼠标在世界坐标中的位置
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 计算从激光发射点到鼠标位置的方向
        currentDirection = (mousePosition - (Vector2)transform.position).normalized;
    }

    public void FixedUpdate()
    {
        // 注：ismouseoverui是检测鼠标是否在UI上。由于UseChargePower在鼠标检测在UI上之后。鼠标检测在UI上就直接返回了，导致无法在Item类里销毁激光，所以新增变量放在激光里。
        // 如果鼠标没有按下，那么销毁
        if (!Input.GetMouseButton(0) || ItemControl.instance.isMouseOverUI)
        {
            StartCoroutine(DestroyGameObjectAfterParticles());
            return;
        }
    }
    IEnumerator DestroyGameObjectAfterParticles()
    {
        // 停止特定粒子系统的发射
        specificParticleSystem.Stop();

        // 将粒子系统的游戏对象从父对象中分离
        specificParticleSystem.transform.parent = null;

        // 销毁游戏对象
        Destroy(gameObject);

        // 理论上这里执行不到

        // 等待所有粒子消失
        while (specificParticleSystem.IsAlive())
        {
            yield return null;
        }
        // 销毁粒子系统的游戏对象
        Destroy(specificParticleSystem.gameObject);
    }

    public void SetLaser(Vector2 start, Vector2 end)
    {
        try
        {

            Vector2 targetDirection = end - start;
            targetDirection.Normalize();

            // 平滑地改变激光的方向
            currentDirection = Vector2.MoveTowards(currentDirection, targetDirection, Time.deltaTime * rotationSpeed);
            currentDirection.Normalize();

            // 使用新的激光方向来设置激光的起始位置和结束位置
            start = start + currentDirection * 0.3f;
            end = start + currentDirection * maxLength;

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);

            startVFX.transform.position = start;
            endVFX.transform.position = end;

            startVFX.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg);
            endVFX.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg);



            attackIntervalCount += Time.deltaTime;
            LayerMask mask = LayerMask.GetMask("EnemyBullet", "Enemy");
            RaycastHit2D hit = Physics2D.Raycast(start, currentDirection, maxLength, mask);
            if (hit.collider != null && canDestroyBullet && hit.collider.gameObject.tag == "EnemyBullet")
            {
                endVFX.transform.position = hit.point;
                lineRenderer.SetPosition(1, hit.point);

                if (attackIntervalCount >= attackInterval)
                {
                    attackIntervalCount = attackInterval;
                    var bullet = hit.collider.GetComponent<BulletBaseClass>();
                    bullet.damageAmount -= (damage * 0.5f);
                    if (bullet.damageAmount <= 0)
                        Destroy(hit.collider.gameObject);
                }
            }
            else if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
            {
                endVFX.transform.position = hit.point;
                lineRenderer.SetPosition(1, hit.point);

                if (hit.collider.tag == "Enemy" && attackIntervalCount >= attackInterval)
                {
                    attackIntervalCount = 0f;
                    hit.collider.GetComponent<EnemyControl>().TakeDamage(damage, 3);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }


}
