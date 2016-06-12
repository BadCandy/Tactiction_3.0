using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;

    void Start()
    {
        Shaking = false;
    }

    void Update()
    {
        if (ShakeIntensity > 0)
        {
            
            transform.position = OriginalPosition + Random.insideUnitSphere * ShakeIntensity;
            transform.rotation = new Quaternion(
                 OriginalRotation.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f,
                 OriginalRotation.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f,
                 OriginalRotation.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f,
                 OriginalRotation.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .1f);
           
            ShakeIntensity -= ShakeDecay;
        }

        else if (Shaking)
        {
            Shaking = false;
        }
    }

    public void DoShake()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;

        ShakeIntensity = 0.1f;
        ShakeDecay = 0.01f;
        Shaking = true;
    }
}
