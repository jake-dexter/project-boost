using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] ParticleSystem left_thruster;
    [SerializeField] ParticleSystem right_thruster;
    [SerializeField] ParticleSystem main_booster;

    [SerializeField] AudioClip thrust_sfx;
    [SerializeField] float rocket_thrust = 1000f;
    [SerializeField] float angular_velocity = 90f;
    
    Rigidbody rb;
    AudioSource audio_source;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio_source = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopSound(audio_source);
            StopParticle(main_booster);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            PlayParticle(right_thruster);
            Rotate(angular_velocity);
        }
        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            PlayParticle(left_thruster);
            Rotate(-angular_velocity);
        }
        else
        {
            StopParticle(right_thruster);
            StopParticle(left_thruster);
        }
    }

    void StartThrust()
    {
        PlayParticle(main_booster);
        rb.AddRelativeForce(Vector3.up * rocket_thrust * Time.deltaTime);
        PlaySound(audio_source, thrust_sfx);
    }

    void Rotate(float angle)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * angle * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void PlaySound(AudioSource source, AudioClip sound)
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(sound);
        }
    }

    void StopSound(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }

    void PlayParticle(ParticleSystem particle)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }

    void StopParticle(ParticleSystem particle)
    {
        if (particle.isPlaying)
        {
            particle.Stop();
        }
    }
}
