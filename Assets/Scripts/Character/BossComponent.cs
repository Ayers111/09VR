using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100f;
    public float currentHealth;
    public float defensePower = 5f;
    public float attackPower = 10f;
    public float CharacterDamage = 50f;
    public GameObject win;

    public Image healthBarImage; // Ѫ��ͼƬ

    void Start()
    {
        currentHealth = maxHealth;
        //healthBarImage = GetComponentInChildren<Image>();
    }

    public void TakeDamage(float damage)
    {
        float actuallDamage = damage - defensePower;
        if(actuallDamage <= 0)
        { 
            actuallDamage = 0; 
        }
        currentHealth -= actuallDamage;
        UpdateHealthBar();
        if(currentHealth <= 0)
        {
            win.SetActive(true);
        }

        Debug.Log("������" + defensePower);
        Debug.Log("��ʵ����˺���" + actuallDamage);
        Debug.Log("��ǰѪ����" + currentHealth);
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Character>() != null)
        {
            Debug.Log("�����˺���"+ CharacterDamage);
            TakeDamage(CharacterDamage);


        }

    }
}

