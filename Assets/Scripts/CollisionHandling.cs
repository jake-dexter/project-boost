using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandling : MonoBehaviour
{
    [SerializeField] AudioClip crash_sfx;
    [SerializeField] AudioClip finish_sfx;

    [SerializeField] ParticleSystem crash_particles;
    [SerializeField] ParticleSystem finish_particles;

    [SerializeField] float crash_wait_time = 1f;
    [SerializeField] float finish_wait_time = 1f;

    AudioSource audio_source;

    bool transitioning;
    bool can_collide = true;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            can_collide = !can_collide;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (transitioning || !can_collide) {return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                FinishSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    void CrashSequence()
    {
        transitioning = true;
        audio_source.Stop();
        PlaySound(audio_source,crash_sfx);
        crash_particles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",crash_wait_time);
    }

    void FinishSequence()
    {
        transitioning = true;
        audio_source.Stop();
        PlaySound(audio_source,finish_sfx);
        finish_particles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",finish_wait_time);
    }

    void ReloadLevel()
    {
        int current_level_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_level_index);
    }

    void LoadNextLevel()
    {
        int current_level_index = SceneManager.GetActiveScene().buildIndex;
        int next_level_index = current_level_index + 1;

        if (next_level_index == SceneManager.sceneCountInBuildSettings)
        {
            next_level_index = 0;
        }
        SceneManager.LoadScene(next_level_index);
    }

    void PlaySound(AudioSource source, AudioClip sound)
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(sound);
        }
    }
}
