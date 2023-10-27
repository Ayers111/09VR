using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
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

    public GameObject originalPosition; // ԭʼλ��

    public float randomRange = 0.5f; // ���λ�õķ�Χ 
    public float randomYRangea = 2f; // ���Yλ�õķ�Χa
    public float randomYRangeb = 3f; // ���Yλ�õķ�Χb

    public Vector3 targetPosition; // Ŀ��λ��  
    public float moveSpeed = 1.0f; // �ƶ��ٶ�  

    private Vector3 initialPosition; // ��ʼλ��  
    private float elapsedTime; // �Ѿ���ȥ��ʱ�� 

    public bool flashSkillTime = false; // �Ƿ�����˸���ܸ�ֵ

    void Start()
    {
        currentHealth = maxHealth;
        //healthBarImage = GetComponentInChildren<Image>();

        initialPosition = transform.position; // ��¼��ʼλ��  
        StartCoroutine(MoveObjectCoroutine()); // ��Start�����п�ʼЭ�� 
    }

    void Update()
    {

        //float step = moveSpeed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition1.transform.position, step);
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

    /// <summary>
    /// �ƶ�
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveObjectCoroutine()
    {
        while (flashSkillTime == true) // ����һ������ѭ����ʹЭ��һֱ����  
        {
            // ��ȡ��ɫ��ǰλ��  
            Vector3 bossPosition = transform.position;

            // �������ƫ����  
            float randomX = UnityEngine.Random.Range(-randomRange, randomRange);
            float randomY = UnityEngine.Random.Range(randomYRangea, randomYRangeb);
            float randomZ = UnityEngine.Random.Range(-randomRange, randomRange);

            // �����µ����λ��  
            Vector3 randomPosition = new Vector3(bossPosition.x + randomX, bossPosition.y + randomY, bossPosition.z + randomZ);

            targetPosition = randomPosition;

            // ����Ϸ�����ƶ���ָ��λ��  
            transform.position = targetPosition;
            // ��¼��ȥ��ʱ��  
            elapsedTime += Time.deltaTime;
            yield return new WaitForSeconds(1); // ��Э�̵ȴ�1���ӣ�Ȼ�����¿�ʼ��һ��ѭ�����ƶ����ȴ���
        }
    }
}

