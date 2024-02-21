
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successAudio = null;
    [SerializeField] AudioClip failAudio = null;

    [SerializeField] ParticleSystem successParticle = null;
    [SerializeField] ParticleSystem failParticle = null;

    AudioSource audioSource;
    CapsuleCollider capsuleCollider;

    bool isTransitioning = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        ActivateCheat();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning)
            return;
        //위 조건문이 맞다면 이 함수를 빠져나간다. 이 축약문장이 훨씬 좋은듯
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                GoNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void ActivateCheat()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            //collisionDisable = !collisionDisable       toggle collsion
            capsuleCollider.enabled = false;
            this.gameObject.transform.GetChild(1).GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Fuel"))
        {
            Destroy(other.gameObject);
        }
    }

    void StartCrashSequence()
    {
        PlayAudio(failAudio);
        failParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        isTransitioning = true;
    }

    void GoNextLevel()
    {
        PlayAudio(successAudio);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay); //delay보다는 levelLoadDelay가 더 어울리는듯.
        isTransitioning = true;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = ++currentSceneIndex;  
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void PlayAudio(AudioClip WhatSequence)
    {
        // if(!audioSource.isPlaying)
        // {
            audioSource.Stop();
            audioSource.PlayOneShot(WhatSequence);
        // }
        // else
        // {
        //     audioSource.Stop();
        // }
        // 다른효과음이 발생하지 않도록 설정하는 것이었지만 movement 효과음에도 작동하지 않음. bool형 변수로 효과음작동방식을 바꿔주는것이 좋은듯
    }

}
