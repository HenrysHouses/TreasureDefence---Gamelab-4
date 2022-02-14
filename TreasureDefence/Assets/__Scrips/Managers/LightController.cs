/*
 * Written by:
 * Henrik
*/
using UnityEngine;

public class LightController : MonoBehaviour
{
    public float fadeSpeed;
    float maxIntensity;
    Light _light;
    bool _state = true;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        maxIntensity = _light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state  && _light.intensity < maxIntensity)
            _light.intensity += Time.deltaTime * fadeSpeed; 
        else if (_light.intensity > 0)
            _light.intensity -= Time.deltaTime * fadeSpeed; 
    }

    public bool fadeLightSate
    {
        set =>  _state = value;
    }
}
