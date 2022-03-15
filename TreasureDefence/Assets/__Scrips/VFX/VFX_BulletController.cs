using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VFX_BulletController : MonoBehaviour
{
    [SerializeField] GameObject impact_VFX;
    [SerializeField] float bulletSpeed;
    [SerializeField] float startRotationMaxSpeed = 1;
    Vector3 startRotationSpeed;
    Transform mesh;
    Rigidbody _rigidbody;
    void Start()
    {
        float x = Random.Range(-startRotationMaxSpeed ,startRotationMaxSpeed);
        float y = Random.Range(-startRotationMaxSpeed ,startRotationMaxSpeed);
        float z = Random.Range(-startRotationMaxSpeed ,startRotationMaxSpeed);
        startRotationSpeed = new Vector3(x,y,z);

        mesh = transform.GetChild(0);
        mesh.eulerAngles  = startRotationSpeed; 

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * bulletSpeed;
    }

    void Update()
    {
        float x = startRotationSpeed.x * Time.deltaTime;
        float y = startRotationSpeed.y * Time.deltaTime;
        float z = startRotationSpeed.z * Time.deltaTime;
        
        mesh.Rotate(new Vector3(x, y, z), Space.World);
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        Quaternion normalDirection = Quaternion.LookRotation(other.GetContact(0).normal);
        Instantiate(impact_VFX, transform.position, normalDirection);
        Destroy(gameObject);

        if (other.transform.CompareTag("Target"))
        {

            other.collider.GetComponentInParent<ShootingRangeController_ArcadeMachine>().Hit();
        }
    }


}
