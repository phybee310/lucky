
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject howtoPlay;

     public void Tutorial()
    {
        howtoPlay.SetActive(true);
        Time.timeScale = 0;
   
        
     }

    public void Back()
{
        howtoPlay.SetActive(false);
        Time.timeScale = 1;

}


}
