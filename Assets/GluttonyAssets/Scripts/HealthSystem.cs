using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject heart6;
    public GameObject heart7;
    public GameObject heart8;

    public GameObject heartsContainer;

    public bool isDead;

    public int healthNum;

    public GameObject SceneManagerGO;

    public AudioSource gluttonyMunchSound;
    public AudioSource monsterGrowl;

    //gluttony bullet stuff

    private int amtOfDntsHit = 0;



    // Start is called before the first frame update
    void Start()
    {

        healthNum = 8;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        heart4.gameObject.SetActive(true);
        heart5.gameObject.SetActive(true);
        heart6.gameObject.SetActive(true);
        heart7.gameObject.SetActive(true);
        heart8.gameObject.SetActive(true);

        isDead = false;
    
    }

    // Update is called once per frame
    void Update()
    {
    	Scene currentScene = SceneManager.GetActiveScene ();
    	string sceneName = currentScene.name;
    	
    	if (sceneName == "Gluttony1" || sceneName == "WrathIsland" || sceneName == "GreedDungeon") 
         {
            heartsContainer.SetActive(true);
         }
         if (sceneName == "GameOverGluttony" || sceneName == "GameOverWrath") 
         {
            heartsContainer.SetActive(false);
         }

        if (healthNum > 8)
        {
            healthNum = 8;
        }
        else if (healthNum == 8)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
            heart5.gameObject.SetActive(true);
            heart6.gameObject.SetActive(true);
        	heart7.gameObject.SetActive(true);
        	heart8.gameObject.SetActive(true);
        }         
        else if (healthNum == 7)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
            heart5.gameObject.SetActive(true);
            heart6.gameObject.SetActive(true);
        	heart7.gameObject.SetActive(true);
        	heart8.gameObject.SetActive(false);
        }        
        else if (healthNum == 6)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
            heart5.gameObject.SetActive(true);
            heart6.gameObject.SetActive(true);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
        }

        else if (healthNum == 5)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
            heart5.gameObject.SetActive(true);
            heart6.gameObject.SetActive(false);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
        }
        else if (healthNum == 4)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
            heart5.gameObject.SetActive(false);
            heart6.gameObject.SetActive(false);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
        }
        else if (healthNum == 3)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(false);
            heart5.gameObject.SetActive(false);
            heart6.gameObject.SetActive(false);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
        }
        else if (healthNum == 2)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
            heart5.gameObject.SetActive(false);
            heart6.gameObject.SetActive(false);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
        }
        else if (healthNum == 1)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
            heart5.gameObject.SetActive(false);
            heart6.gameObject.SetActive(false);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
        }
        else if (healthNum <= 0)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
            heart5.gameObject.SetActive(false);
            heart6.gameObject.SetActive(false);
        	heart7.gameObject.SetActive(false);
        	heart8.gameObject.SetActive(false);
            isDead = true;
        }


        if (isDead == true)
        {
        	Debug.Log("dead");
            if (sceneName == "Gluttony1") 
            {
                isDead=false;
                healthNum = 8;
                AltSceneManager altSceneMan = SceneManagerGO.GetComponent<AltSceneManager>();
                altSceneMan.SelectScene("GluttonyGameOver");
            }

            Debug.Log("dead");
            if (sceneName == "WrathIsland")
            {
                isDead = false;
                healthNum = 8;
                AltSceneManager altSceneMan = SceneManagerGO.GetComponent<AltSceneManager>();
                altSceneMan.SelectScene("WrathGameOver");
                }
            }
    }
    
    // RIGID BODY NOT KINEMATIC

    private void OnCollisionEnter(Collision collision)
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;

        if (collision.collider.tag == "Damager")
        {
        	Debug.Log("hit");
            healthNum -= 1;
            Destroy(collision.collider.gameObject);
            if (sceneName == "Gluttony1") 
            {
                gluttonyMunchSound.Play();
            }            
        }
        //if i wanna add a way to add health:

    }

    //BOX COLLIDER WITH IS TRIGGER CHECKED

    private void OnTriggerEnter(Collider col)
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;

        if (col.gameObject.tag == "Damager")
        {
            Debug.Log("hit");
            healthNum -= 2;
            if (sceneName == "Gluttony1") 
            {
                gluttonyMunchSound.Play();
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "Bullets")
        {
        	//play audio
        	if (sceneName == "Gluttony1") 
            {
                gluttonyMunchSound.Play();
                amtOfDntsHit +=1;
            }            

        	if (amtOfDntsHit <= 3)
        	{
        		return;
        	} else {
        		healthNum -= 1;
        		amtOfDntsHit = 0;
        	}
        }
        if (col.gameObject.tag == "WrathFin")
        {
            AltSceneManager altSceneMan = SceneManagerGO.GetComponent<AltSceneManager>();
            altSceneMan.SelectScene("GluttonyOP");
            healthNum = 8;
        }
        if (col.gameObject.tag == "GluttonyFin")
        {
            AltSceneManager altSceneMan = SceneManagerGO.GetComponent<AltSceneManager>();
            altSceneMan.SelectScene("GreedOP");
            healthNum = 8;
        }
    }
}