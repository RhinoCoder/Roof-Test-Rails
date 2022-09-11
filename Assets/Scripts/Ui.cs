using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ui : MonoBehaviour
{
    [SerializeField] private Animator uiAnim;
    [SerializeField] private PlayerMovement playerMovement;



    public void PlayGame()
    {
        uiAnim.SetTrigger("play");
        playerMovement.canMove = true;
 

    }

    public void RetryAnimCaller()
    {
        uiAnim.SetTrigger("fail");
        
    }

    public void FailGame()
    {
        SceneManager.LoadScene(0);
    }
}
