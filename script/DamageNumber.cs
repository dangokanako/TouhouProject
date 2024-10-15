using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageNumber : MonoBehaviour
{

    [SerializeField] public TMP_Text damageText;
    public float lifetime = 1.5f;
    private float lifeCounter;
    private float RandomX;
    // Start is called before the first frame update
    void Start()
    {
        // RandomX = Random.Range(-1f, 1f);
        lifeCounter = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;
        }
        else
        {
            DamageNumberControl.instance.PlaceInPool(this);
            //Destroy(gameObject);
        }

        // transform.position += Vector3.up * Time.deltaTime;
        // transform.position += Vector3.right * Time.deltaTime * RandomX;
    }

    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;
        damageText.text = damageDisplay.ToString();

        // 设置初始透明度为1
        Color32 newColor = damageText.color;
        newColor.a = 255;
        damageText.color = newColor;

        // 渐变透明
        damageText.DOFade(0.3f, 1.5f);

        var endpostion = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(0.2f, 0.5f), 0f);
        transform.DOJump(endpostion, Random.Range(0.5f, 1f), 1, 1.5f);
    }
    public void SetupCritical(int damageDisplay)
    {
        lifeCounter = lifetime + 0.5f;
        damageText.text = damageDisplay.ToString() + "!";
        damageText.fontSize = 50;

        // 设置初始透明度为1
        Color32 newColor = damageText.color;
        newColor.a = 255;
        damageText.color = newColor;

        // 渐变透明
        damageText.DOFade(0.3f, 2f);

        var endpostion = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(0.2f, 0.5f), 0f);
        transform.DOJump(endpostion, Random.Range(0.5f, 1f), 1, 2f);

    }
    public void SetText(string text)
    {
        lifeCounter = lifetime;
        damageText.text = text;

        // 设置初始透明度为1
        Color32 newColor = damageText.color;
        newColor.a = 255;
        damageText.color = newColor;

        // 渐变透明
        damageText.DOFade(0.3f, 2f);

        var endpostion = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(0.2f, 0.5f), 0f);
        transform.DOJump(endpostion, Random.Range(0.5f, 1f), 1, 2f);
    }

}
