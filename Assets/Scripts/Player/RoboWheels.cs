using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboWheels : MonoBehaviour
{
    InputController inputs;
    [SerializeField] float incSteps = 1;
    [SerializeField] Transform ParticalEffect;
    PlayerDetectionSensors sensors;

    private void Start()
    {
        var randvalue = Random.Range(0, 180);
        transform.eulerAngles = new Vector3(0, 0, randvalue);

        inputs = new InputController();
        inputs.Player.Enable();

        sensors = transform.root.GetComponent<PlayerDetectionSensors>();
    }

    private void Update()
    {
        var value = inputs.Player.Move.ReadValue<float>();
        if (value > 0)
        {
            if(sensors.IsGrounded())
                ParticalEffect.GetComponent<ParticleSystem>().Emit(1);
            var angel = transform.eulerAngles;
            angel.z -= incSteps;
            transform.eulerAngles = angel;
            ParticalEffect.eulerAngles = new Vector3(0,0,0);
        }
        else if(value < 0)
        {
            if (sensors.IsGrounded())
                ParticalEffect.GetComponent<ParticleSystem>().Emit(1);
            var angel = transform.eulerAngles;
            angel.z += incSteps;
            transform.eulerAngles = angel;
            ParticalEffect.eulerAngles = new Vector3(0, -180, 0);

        }
        else
        {
           // ParticalEffect.GetComponent<ParticleSystem>().Stop();
        }
    }
}
