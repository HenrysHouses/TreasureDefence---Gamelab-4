using System.Collections;
using UnityEngine;

public enum moleState
{
    MovingUp,
    MovingDown,
    Waiting,
    Exposed
}

public class MoleController : MonoBehaviour
{
    public bool isHit;
    [SerializeField] moleState state = moleState.Waiting;
    [SerializeField] bool isMovingDown, isMovingUp, isWaiting;
    public bool isMoving => isMovingDown || isMovingUp || isWaiting;
    
    [SerializeField] float heightOffset;
    [SerializeField] float speedMin, speedMax, currentSpeed;
    [SerializeField] float exposedRangeMin, exposedRangeMax;

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
                    transform.localPosition = new Vector3(pos.x, pos.y + currentSpeed * Time.deltaTime, pos.z);
                }
                if(transform.localPosition.y >= heightOffset)
                {
                    isMovingUp = false;
                    state = moleState.Exposed;
                }
                break;
            
            case moleState.Exposed:
                if(!isWaiting)
                    StartCoroutine(RandomWaitTime(moleState.MovingDown, exposedRangeMin, exposedRangeMax));
                break;
            
            case moleState.MovingDown:
                isMovingDown = true;
                if(pos.y > 0)
                {
                    transform.localPosition = new Vector3(pos.x, pos.y - currentSpeed * Time.deltaTime, pos.z);
                    if(transform.localPosition.y <= 0)
                    {
                        transform.localPosition = new Vector3(pos.x, 0, pos.z);
                        isMovingDown = false;
                        state = moleState.Waiting;
                    }
                }
                break;

            case moleState.Waiting:
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

    

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(!isHit && other.CompareTag("Mallet"))
            OnHit();    
    }

    void OnHit()
    {
        GetComponentInParent<WackAMoleController>().hitCount++;
        isHit = true;
    }

    public void showMole()
    {
        state = moleState.MovingUp;      
        currentSpeed = Random.Range(speedMin, speedMax);
    }
}
