using UnityEngine;
using FMODUnity;

public class OneShotController : MonoBehaviour
{
    StudioEventEmitter _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!FmodExtensions.IsPlaying(_AudioSource.EventInstance))
            Destroy(gameObject);
    }
}
