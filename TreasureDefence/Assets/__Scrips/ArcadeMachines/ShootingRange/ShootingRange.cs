using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : ArcadeMachine
{   // Mikkel tried to make this.
    public GameObject Target;
    Renderer targetRenderer;
    [SerializeField] Rigidbody targetRB;
    public int HitTarget;
    [Tooltip("The Speed of the Target")]
    public float TargetMovementSpeed;
    public float t, StartTimer;
    float timeLeft;
    [SerializeField] Transform leftPos, rightPos;
    bool leftHasChanged, rightHasChanged;
    Vector3 velocity;
  
    void Start()
    {
        targetRenderer = Target.GetComponent<Renderer>();
    }

    // Base Arcade Behaviour

    // Start the Game. What it costs.
    public override void StartSetup()
    {
        base.StartSetup();
        HitTarget = 0;
        timeLeft = StartTimer;
        randomizeLeftPos();
        randomizeRightPos();
        float random = Random.Range(0,1);
        if(random == 0)
            velocity = new Vector3(0, 0, TargetMovementSpeed);
        else
            velocity = new Vector3(0, 0, -TargetMovementSpeed);
    }

    public override void isPlayingUpdate()
    {
        t += TargetMovementSpeed * Time.deltaTime;
        Vector3 pos = Target.transform.localPosition;

        if(pos.z < leftPos.localPosition.z)
            randomizeRightPos();
        if(pos.z > rightPos.localPosition.z)
            randomizeLeftPos();
        targetRB.velocity = velocity;
        timeLeft -= Time.deltaTime;
    }


    public override bool WinCondition()
    {

        if (HitTarget >= 5)
        {
            return true;
        }
        return false;
    }

    public override bool LooseCondition()
    {
        // Debug.Log("Time ran out.");
        if (timeLeft <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void Reward()
    {
        GameManager.instance.SpawnTower(towerRewardPrefab, spawnPoint.position);
    }
    public override void Reset()
    {
        HitTarget = 0;
        targetRB.velocity = new Vector3(0, 0, 0);
    }

    public void Hit()
    {
        HitTarget++;
        targetRenderer.material.color = Color.red;
        Invoke("resetColor", 0.3f);
    }

    void resetColor()
    {
        targetRenderer.material.color = Color.white;
    }
    //While Playing.  Damit Rune.

    void randomizeLeftPos()
    {
        if(!leftHasChanged)
        {
            Vector3 pos = leftPos.localPosition;
            pos.z = Random.Range(-1.0f, 0.0f);
            leftPos.localPosition = pos;
            leftHasChanged = true;
            rightHasChanged = false;
            velocity = new Vector3(0, 0, -TargetMovementSpeed);
        }
    }

    void randomizeRightPos()
    {
        if(!rightHasChanged)
        {
            Vector3 pos = rightPos.localPosition;
            pos.z = Random.Range(0.0f, 1.0f);
            rightPos.localPosition = pos;
            rightHasChanged = true;
            leftHasChanged = false;
            velocity = new Vector3(0, 0, TargetMovementSpeed);
        }
    }
}
