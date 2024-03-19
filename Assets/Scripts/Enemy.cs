using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerBall")
        {
            int currentSceneIndex = LevelManager.instance.GetActiveSceneIndex();
            MyEventSystem.I.FailLevel(currentSceneIndex);
            LevelManager.instance.ReloadCurrentLevel();
        }
    }
}
