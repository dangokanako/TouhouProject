using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ParticleSystem_Script : MonoBehaviour
{
    // Start is called before the first frame update
    // ParticleSystem ps;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    void Start()
    {
        this.transform.position = PlayerHealthControl.instance.transform.position;
    }
    void OnEnable()
    {
        // ps = GetComponent<ParticleSystem>();


        // 启动时，复制整个敌人列表
        // for (int i = 0; i < EnemyCreator.instance.getAllEnemy().Count; i++)
        // {

        //     // ps.collision.AddPlane(EnemyCreator.instance.getAllEnemy()[i].transform);
        //     ps.trigger.AddCollider(
        // EnemyCreator.instance.getAllEnemy()[i].GetComponentInChildren<Collider2D>());
        // }

        // EnemyCreator.instance.addEnemyCallback_event += AddEnemyToTrigger;
    }

    // public void AddEnemyToTrigger(GameObject enemy)
    // {
    //     ps.trigger.AddCollider(enemy.GetComponentInChildren<Collider2D>());
    // }

    void OnDisable()
    {
        // EnemyCreator.instance.addEnemyCallback_event -= AddEnemyToTrigger;
    }

    void Update()
    {
    }


    void OnParticleCollision(GameObject collider)
    {
        DestoryAnimeControl.instance.CreateDestoryAnime(3, collider.transform.position);

        if (collider.tag == "Enemy")
        {
            collider.GetComponentInChildren<EnemyControl>().TakeDamage(8, 3);
        }

    }

    // private void OnParticleTrigger()
    // {
    //     int numParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    //     for (int i = 0; i < numParticles; i++)
    //     {
    //         ParticleSystem.Particle particle = enter[i];

    //         Vector2 particlePosition = particle.position + transform.position;
    //         Collider2D collider = Physics2D.OverlapPoint(particlePosition);

    //         DestoryAnimeControl.instance.CreateDestoryAnime(3, particlePosition);

    //         if (collider == null)
    //         {
    //             Debug.Log("null!");
    //         }
    //         if (collider.tag == "Enemy")
    //         {
    //             collider.GetComponentInChildren<EnemyControl>().TakeDamage(10, 3);
    //         }
    //     }
    // }

}
