using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    bool isAlive = true;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] ParticleSystem crash;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} **Triggered by** {other.gameObject.name}");   
        if ( isAlive == true)
        {
            StartCrash();
        }
    }

    void StartCrash()
    {
        isAlive = false;
        GetComponent<PlayerControls>().enabled = false;
        crash.Play();
        GetComponentInChildren<MeshRenderer>().enabled = false;
        MeshRenderer[] childMeshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in childMeshRenderers)
        {
            renderer.enabled = false;
        }
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
