using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    
    [SerializeField] private float speedForSwipe = 2f;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject fakeBar;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private ParticleSystem[] sparkleParticles;
    [SerializeField] private ParticleSystem[] collectParticles;
    [SerializeField] private TMP_Text gemText;
    [SerializeField] private Ui uiScript;
    [SerializeField] private float scale=0.01f;
    
    
    private Touch touch;
    private float properDistanceFromPlayerForBar;
    private int gemCount = 0;
        
    
    public float speed=5f;
    public bool canMove=true;

   

    private void Start()
    {
        properDistanceFromPlayerForBar = bar.transform.localPosition.z;
    }

    private void Update()
    {
        if (canMove)
        {
            TouchController();
            CollectibleHandler();
            FakeBarScaleHandler();
        }
        
    }


    private void TouchController()
    {
        
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            transform.position = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + speed * Time.deltaTime);
            
            
            if (touch.phase == TouchPhase.Moved)
            {
                
                
                transform.position =
                    new Vector3(transform.position.x + touch.deltaPosition.x * speedForSwipe * Time.deltaTime,
                        transform.position.y,
                        transform.position.z);
                
                PositionChecker();
                
            }
            
        }
        
    }
    
    private void PositionChecker()
    {
        var pos = transform.position;
        pos.x =  Mathf.Clamp(transform.position.x, -4.2f, 4.2f);
        transform.position = pos;
    }

    private void BarCollectHandler()
    {
        bar.transform.localScale += new Vector3(0f, scale, 0f);
    }

    private void BarSlidingHandler()
    {
         
        foreach (ParticleSystem spark in sparkleParticles)
        {
            spark.Play();
        }
        ScaleDecreaser();
        speed = 25f;
    }

    public void ScaleDecreaser()
    {
        if (bar.transform.localScale.y <= 1.5)
        {
            return;
        }
        bar.transform.localScale -= new Vector3(0f, (scale), 0f);
    }

    private void AfterBarSliding()
    {
        foreach (ParticleSystem spark in sparkleParticles)
        {
            spark.Stop();
        }
 
        speed = 13f;
    }
    

    private void CollectibleHandler()
    {
        gemText.text = gemCount.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("barrel"))
        {
            BarSlidingHandler();
        }
        else
        {
            AfterBarSliding();
        }
        
        if (other.gameObject.CompareTag("Finish"))
        {
            //Here the  next game button would be called,
            //For this test case project, I just called the
            //retry button the restart the level again.
            FailDutiesHandler();
        }
        
    }

    private void FakeBarScaleHandler()
    {
        fakeBar.transform.localScale = new Vector3(bar.transform.localScale.x, bar.transform.localScale.y,
            bar.transform.localScale.z);
        

    }
    
    private void OnCollisionExit(Collision other)
    {
        AfterBarSliding();
         
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            BarCollectHandler();    
            other.gameObject.GetComponent<Barrel>().CollectHandler();
            
        }
        
        else if (other.gameObject.CompareTag("deathZone"))
        {
            FailDutiesHandler();
        }
        
        else if (other.gameObject.CompareTag("diamond"))
        {

            gemCount++;
            other.GetComponent<Diamond>().GemCollect();
        }
        
       
            
    }

    private void FailDutiesHandler()
    {
        uiScript.RetryAnimCaller();
        canMove = false;
        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        
    }

    
    
    public IEnumerator BarPosResetHandler()
    {

        yield return new WaitForSeconds(0.5f);
        bar.transform.localPosition = new Vector3(0f, bar.transform.localPosition.y, properDistanceFromPlayerForBar);


    }
}
