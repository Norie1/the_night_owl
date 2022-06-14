using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Player_Info
{
    public int walletInfo;
    public int health;
    public bool canThrow;
    public float[] transform; 
    public string scene;

    public Player_Info(PlayerHealth ph , PlayerFireBall pfb , string nameScene , Transform pTransform)
    {

        health = ph.currentHealth;
        canThrow = pfb.canThrowFireBall;
        scene = nameScene;
        transform = new float[3]; 
        transform[0] = pTransform.position.x;
        transform[1] = pTransform.position.y;
        transform[2] = pTransform.position.z;
    }
}
