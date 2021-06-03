using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{

    public string[] TaskString;
    private Animator CanvasAnim;
    [Space]
    public Animator gems;
    public GameObject[] gem;
    public GameObject ParticleCrashGem;
    public Transform tranfGem;
    [Space]
    public int HPbat = 90;
    public Text enemyText;
    public Slider hpSlider;
    public GameObject Enemy;
    public Animator EnemyAnim;

    int indexGem = 0;
    int CurrentUron;
  
    void Start()
    {
       
        hpSlider.maxValue = 90;
        hpSlider.value = 90;
        enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();
        PlayerPrefs.SetInt("Number", 0);
        CanvasAnim = GetComponent<Animator>();
        StartCoroutine(StartFight());
   
    }
    public IEnumerator StartFight()
    {
        yield return new WaitForSeconds(17.5f);
       CanvasAnim.SetTrigger("StartFight");
    }
    public void ClickOnGems()
    {

        if (indexGem == 0)
        {
            gems.SetTrigger("Pick");
            StartCoroutine(WaitToStart());
        }
    }
    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(WaitForChangeScale());
    }
    IEnumerator WaitForChangeScale()
    {
        if (indexGem <= 3)
        {
            float i = gem[indexGem].transform.localScale.x;
            for (float q = i; q < i * 2; q += .1f)
            {
                gem[indexGem].transform.localScale = new Vector3(q, q, q);
                yield return new WaitForSeconds(.05f);
            }
            yield return new WaitForSeconds(1);
            CanvasAnim.SetTrigger("NewTask");
            indexGem++;
        }
        else
        {
            hpSlider.value -= CurrentUron;
            if(hpSlider.value<= 0)
            {
                hpSlider.value = 0;
                EnemyAnim.SetTrigger("die");
            }
        }
    }
    public void CrashGem(int Plus)
    {
        CurrentUron += Plus;
        PlayerPrefs.SetInt("Number", PlayerPrefs.GetInt("Number") +1);
        StartCoroutine(CrashGemCoroutine());
    }
    IEnumerator CrashGemCoroutine()
    {
        yield return new WaitForSeconds(2);
        CanvasAnim.SetTrigger("NewTask");
        yield return new WaitForSeconds(3);
      
        tranfGem.position = gem[indexGem - 1].transform.position;
        gem[indexGem - 1].SetActive(false);
        Instantiate(ParticleCrashGem, tranfGem.transform);

        StartCoroutine(WaitForChangeScale());
       
    }

    void Update()
    {
        HPbat = (int)hpSlider.value;
        enemyText.text = hpSlider.value.ToString() + "/" + hpSlider.maxValue.ToString();
    }
}
