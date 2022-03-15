using System.Collections;
using UnityEngine;
using FMODUnity;

public enum moleState
{
    MovingUp,
    MovingDown,
    Waiting,
    Exposed
}

public class MoleController : MonoBehaviour
{
    [SerializeField] GameObject OnHitParticle;
    public bool isHit;
    Rigidbody _rigidbody;
    [SerializeField] moleState state = moleState.Waiting;
    [SerializeField] bool isMovingDown, isMovingUp, isWaiting;
    public bool isMoving => isMovingDown || isMovingUp || isWaiting;
    
    [SerializeField] float heightOffset;
    [SerializeField] float speedMin, speedMax, currentSpeed;
    [SerializeField] float exposedRangeMin, exposedRangeMax;
	StudioEventEmitter MoleHitSFX;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        MoleHitSFX = GetComponent<StudioEventEmitter>();
    }

    void Update()
    {
        

        if(Input.GetKey(KeyCode.Space))
            showMole();
        Vector3 pos = transform.localPosition; 

        switch(state)
        {
            case moleState.MovingUp:
                isMovingUp = true;
                if(pos.y <= heightOffset)
                {
                    _rigidbody.velocity = Vector3.up * currentSpeed;
                    // transform.localPosition = new Vector3(pos.x, pos.y + currentSpeed * Time.deltaTime, pos.z);
                }
                if(transform.localPosition.y >= heightOffset)
                {
                    isMovingUp = false;
                    state = moleState.Exposed;

                }
                break;
            
            case moleState.Exposed:
                _rigidbody.velocity = Vector3.zero;
                if(!isWaiting)
                    StartCoroutine(RandomWaitTime(moleState.MovingDown, exposedRangeMin, exposedRangeMax));
                break;
            
            case moleState.MovingDown:
                isMovingDown = true;
                if(pos.y >= 0)
                {
                    _rigidbody.velocity = Vector3.down * currentSpeed;
                    // transform.localPosition = new Vector3(pos.x, pos.y - currentSpeed * Time.deltaTime, pos.z);
                }
                if(transform.localPosition.y <= 0)
                {
                    transform.localPosition = new Vector3(pos.x, 0, pos.z);
                    isMovingDown = false;
                    state = moleState.Waiting;
                }
                break;

            case moleState.Waiting:
                isHit = false;
                _rigidbody.velocity = Vector3.zero;
                break;
        }
    }

    IEnumerator RandomWaitTime(moleState exitState, float randomMin, float randomMax )
    {
        isWaiting = true;
        float random = Random.Range(randomMin, randomMax);
        yield return new WaitForSeconds(random);
        state = exitState;
        isWaiting = false;
    }



    void OnCollisionEnter(Collision collision)
    {
        if(!isHit && collision.collider.CompareTag("Mallet") && collision.relativeVelocity.magnitude > 0)
            OnHit(collision);
    }

    void OnHit(Collision collision)
    {
        GetComponentInParent<WackAMoleController_ArcadeMachine>().hitCount++;
        isHit = true;
        isMovingUp = false;
        isMovingDown = true;
        state = moleState.MovingDown;
        GameObject spawn = Instantiate(OnHitParticle, collision.GetContact(0).point, Quaternion.identity);
        MoleHitSFX.Play();
    }

    public void showMole()
    {
        state = moleState.MovingUp;      
        currentSpeed = Random.Range(speedMin, speedMax);
    }
}
