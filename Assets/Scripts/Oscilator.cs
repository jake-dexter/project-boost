using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    public float period = 1f;
    Vector3 starting_position;
    float movement_factor;
    [SerializeField] Vector3 relative_position;

    const float tau = Mathf.PI * 2f;

    void Start()
    {
        starting_position = gameObject.transform.position;
    }

    void Update()
    {
        if (period >= Mathf.Epsilon)
        {
            float cycles = Time.time / period;
            movement_factor = Mathf.Abs(Mathf.Sin(tau * cycles));
        }

        Vector3 offset = movement_factor * relative_position;
        gameObject.transform.position = starting_position + offset;
    }
}
