using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerBall")
        {
            int currentSceneIndex = LevelManager.instance.GetActiveSceneIndex();
            MyEventSystem.I.CompleteLevel(currentSceneIndex);
            LevelManager.instance.LoadNextLevel();
        }
    }
}
