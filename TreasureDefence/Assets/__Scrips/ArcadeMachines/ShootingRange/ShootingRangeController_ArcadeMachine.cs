using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using TMPro;

public class ShootingRangeController_ArcadeMachine : ArcadeMachine
{   // Mikkel tried to make this.
    // Henrik Revamped this
    public GameObject Target;
    [SerializeField] TextMeshPro GameInfo;
    string startText;
    [SerializeField] Rigidbody targetRB;
    public int HitTarget;
    [Tooltip("The Speed of the Target")]
    public float TargetMovementSpeed;
    public float t, StartTimer;
    float timeLeft;
    [SerializeField] Transform leftPos, rightPos;
    bool leftHasChanged, rightHasChanged;
    Vector3 velocity;
    [SerializeField] GameObject startGameBottle;
    [SerializeField] Animator animator;
    [SerializeField] Transform rugTransform;
    [SerializeField] VR_PlayerController VRPlayer;
    bool holdingGun, shouldTeleport;
    [SerializeField] StudioEventEmitter BottleHit;
    [SerializeField] GameObject BottleShardsParticle, BottleHalfPrefab;
    [SerializeField] Vector3 brokenBottleVelocity;
    [SerializeField] bool IsOpen_animator;
    [SerializeField] ParticleSystem[] winConfetti;

    new void Start()
    {
        base.Start();
        startText = "Shoot the bottle to start";
        GameInfo.text = startText;
    }

    // Base Arcade Behaviour
    // Start the Game. What it costs.
    public override void StartSetup()
    {
        base.StartSetup();
        // Teleport To rug, disable movement
        BottleHit.Play();
        shouldTeleport = true;
        // arcade setup
        HitTarget = 0;
        timeLeft = StartTimer;
        randomizeLeftPos();
        randomizeRightPos();
        float random = Random.Range(0,1);
        if(random == 0)
            velocity = new Vector3(0, 0, TargetMovementSpeed);
        else
            velocity = new Vector3(0, 0, -TargetMovementSpeed);
        startGameBottle.SetActive(false);
        Target.SetActive(true);

    }

    new void Update()
    {
        base.Update();

        if(!holdingGun)
        {
            if(animator.GetBool("IsOpen") != IsOpen_animator)
                animator.SetBool("IsOpen", IsOpen_animator);
        }
        else if (!animator.GetBool("IsOpen"))
            animator.SetBool("IsOpen", true);
    }

    public override void isPlayingUpdate()
    {
        t += TargetMovementSpeed * Time.deltaTime;
        Vector3 pos = Target.transform.localPosition;


        // Debug.Log(pos.z + ": " + rightPos.localPosition.z + ", " + leftPos.localPosition.z);
        if(pos.z <= leftPos.localPosition.z)
            randomizeRightPos();
        if(pos.z >= rightPos.localPosition.z)
            randomizeLeftPos();
        targetRB.velocity = velocity;
        timeLeft -= Time.deltaTime;

        GameInfo.text = timeLeft + "\nHits: " + HitTarget + " / 5";
    }

    void LateUpdate()
    {
        if(shouldTeleport)
        {
            VRPlayer.transform.position = rugTransform.position;
            // VRPlayer.transform.rotation = rugTransform.rotation;
            shouldTeleport = false;
            VRPlayer.canMove = false;
            VRPlayer.canTeleport = false;
        }
    }


    public override bool WinCondition()
    {

        if (HitTarget >= 5)
        {
            return true;
        }
        return false;
    }

    public override bool LoseCondition()
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

    public override void LoseTrigger()
	{
		GameInfo.text = "You lose \n"+ HitTarget + " / 5 Hits";
	}

    public override void Reward()
    {
        foreach (var particle in winConfetti)
		{
			particle.Play();
		}

		GameInfo.text = "You Won! Take your reward!";
        GameManager.instance.SpawnTower(towerRewardPrefab, spawnPoint.position);
        Instantiate(BottleShardsParticle, Target.transform.position, Quaternion.identity);
        GameObject bottle = Instantiate(BottleHalfPrefab, Target.transform.position, Quaternion.identity);
        Rigidbody rb = bottle.GetComponent<Rigidbody>();
        rb.velocity = brokenBottleVelocity;
        // Debug.Break();
    }

    public override void Reset()
    {
        HitTarget = 0;
        targetRB.velocity = new Vector3(0, 0, 0);
        startGameBottle.SetActive(true);
        Target.SetActive(false);
        if(animator.GetBool("IsOpen") && !holdingGun)
        {
            animator.SetBool("IsOpen", false);
        }
        VRPlayer.canMove = true;
        VRPlayer.canTeleport = true;
        StartCoroutine(resetInfo());
    }

    public void Hit()
    {
        HitTarget++;
        BottleHit.Play();
        Instantiate(BottleShardsParticle, Target.transform.position, Quaternion.identity);
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
            velocity = new Vector3(0, 0, -1*velocity.z);
        }
        Debug.Log("left");
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
            velocity = new Vector3(0, 0, -1*velocity.z);
        }
        Debug.Log("right");

    }
    
    public void setIsOpen(bool state)
    {
        if(!isPlaying)
            IsOpen_animator = state;
            // animator.SetBool("IsOpen", state);
    }

    public void isHoldingGun(bool state)
    {
        holdingGun = state;
    }

    IEnumerator resetInfo()
    {
        yield return new WaitForSeconds(15);
        GameInfo.text = startText;
    }
}
