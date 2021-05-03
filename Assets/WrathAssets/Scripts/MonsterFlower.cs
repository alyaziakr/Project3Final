using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlower : MonoBehaviour
{
    public GameObject flower;
    public GameObject wrath;
    public AudioSource magicWand;
    // Start is called before the first frame update
    void Start()
    {
        flower.SetActive(false);
        wrath.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On collision enter");
        //magicWand.PlayOneShot(impact, 0.7F);

         wrath.SetActive(false);
        flower.SetActive(true);
    }
}