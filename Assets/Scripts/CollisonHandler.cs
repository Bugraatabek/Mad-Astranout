using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisonHandler : MonoBehaviour
{  
    [SerializeField] float levelLoadDelay = 3f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
   
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
   void Update() 
   {
    RespondToDebugKeys();
    
   } 
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }
         
     switch (other.gameObject.tag)
     {
        case "Friendly":
            Debug.Log("Hi");
            break;
        case "Finish":
            StartSuccessSequence();
            break;
        default:
            StartCrashSequence();    
            break;
        

    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        audioSource.PlayOneShot(crashSound); 
        crashParticles.Play(); 
    }
    }
    void ReloadLevel() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Level 1");
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
        GetComponent<Movement>().enabled = false;
    }
    void RespondToDebugKeys()
    {   
        if (Input.GetKeyDown(KeyCode.L)) 
        {
        LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }   
    } 
   
}
    

