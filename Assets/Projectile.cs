﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    public int damage;
    WaveSpawnner WaveSpawnnerScript;
    public int wavenumber;
    public GameObject prefab;
    public int Devilscripthealth;
    

    private void Start(){
        //Destroy(gameObject,lifeTime);//if the lieftime is passed we destroy the projecticle..
        Invoke("DestroyProjectile",lifeTime);
        
    }

    private void Update(){
        transform.Translate(Vector2.up*speed*Time.deltaTime); 
        //Destroy(this.gameObject,3);
    }  


    public void DestroyProjectile(){
        Destroy(gameObject);
        Instantiate(explosion,transform.position,Quaternion.identity);       
    }

    //to detect collision..with enemy
    private void OnTriggerEnter2D(Collider2D collision){
        
        if (collision.tag =="Enemy" && GameObject.FindGameObjectWithTag("Player") != null){
            
            //Changes being made
            WaveSpawnnerScript=GameObject.FindGameObjectWithTag("wave").GetComponent<WaveSpawnner>();
            wavenumber=WaveSpawnnerScript.currentWaveIndex;
            
            if(wavenumber>=1){
                    if(collision.GetComponent<OurEnemy>().transform.name=="Sphere(Clone)"
                        && GameObject.FindGameObjectWithTag("Weapon").transform.name!="gun"
                        )
                    {        
                        //damage=1;
                        collision.GetComponent<OurEnemy>().YellowBoost(damage);
                        DestroyProjectile();
                    }   
                
                else{
                
                collision.GetComponent<OurEnemy>().TakeDamage(damage); //collision.GetComponent<Enemy> loads the enemy script on
                                                        //collision object and call the TakeDamage function of Enemy.cs
                DestroyProjectile();
                  
                }        
            }
            else{                   
                collision.GetComponent<OurEnemy>().TakeDamage(damage); //collision.GetComponent<Enemy> loads the enemy script on
                                                    //collision object and call the TakeDamage function of Enemy.cs
            DestroyProjectile();
            }
        }
        if(collision.tag=="devil"){
        collision.GetComponent<Devil>().TakeDamage(damage);
        //Devilscripthealth=GameObject.FindGameObjectWithTag("devil").GetComponent<Devil>().health;
        //if (Devilscripthealth>=22){
        //prefab = (GameObject)Resources.Load("ToonExplosion/Prefabs/smallDevil", typeof(GameObject));
        //Instantiate(prefab,transform.position,Quaternion.identity);
        DestroyProjectile();
        }

        if(collision.tag=="bomb"){
            explosion = (GameObject)Resources.Load("ToonExplosion/Prefabs/Explosion", typeof(GameObject));
            Collider2D[] colliders = Physics2D.OverlapCircleAll(collision.gameObject.transform.position,9.0f);
            //Debug.Log(colliders);
            foreach (Collider2D col in colliders)
            {
                if (col.tag == "Enemy")
                {
                Destroy(col.gameObject);
                }
            }
            Destroy(collision.gameObject);
            Instantiate(explosion,transform.position,Quaternion.identity);
            }
            
        }   
}
