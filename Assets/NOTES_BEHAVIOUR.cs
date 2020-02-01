using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTES_BEHAVIOUR : MonoBehaviour
{
    //This document is made for taking some notes about the game's behabviour, also info about it.
    /*Game's Autor: kantosCoder
    
    
    Notes:     
         About the UI layer:  for working is good to have it embeeded with the main camera (to keep it tight),
         but it can do weird things with the layers, so in order to have it working properly is better to have
         it on "overlay" mode.
    
        example of gameobject
        //load main_projectile_behaviour
        private GameObject projectile;
        //create main_projectile_behaviour mother object
        projectile = Resources.Load("Projectile_main") as GameObject;
         

        example of timer:
        using UnityEngine;
         using System.Collections;
 
         public class SimpleTimer: MonoBehaviour {
 
         public float targetTime = 60.0f;
 
         Update(){
 
         targetTime -= Time.deltaTime;
 
         if (targetTime <= 0.0f)
         {
            timerEnded();
         }
 
         }
 
         void timerEnded()
         {
            //do your stuff here.
         }
        }


        //example of remaining time:
         public class Countdown: MonoBehaviour {
        public int duration = 60;
        public int timeRemaining;
        public bool isCountingDown = false;
        public void Begin()
        {
            if (!isCountingDown) {
                isCountingDown = true;
                timeRemaining = duration;
                Invoke ( "_tick", 1f );
            }
        }
 
        private _tick() {
            timeRemaining--;
            if(timeRemaining > 0) {
                Invoke ( "_tick", 1f );
            } else {
                isCountingDown = false;
            }
        }
    }
         
    */




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
