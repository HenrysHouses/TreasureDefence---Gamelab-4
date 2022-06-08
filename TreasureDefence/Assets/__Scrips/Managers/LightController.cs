/*
 * Written by:
 * Henrik
*/
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject fireObj;
    public Vector3 FireScaleDown, FireScaledPos;
    private Vector3 StartScale, startPos;
    public float fadeSpeed;
    private float _fade = 1;
    float maxIntensity;
    Light _light;

    [SerializeField] bool _state = true;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        maxIntensity = _light.intensity;
        if(fireObj)
        {
            StartScale = fireObj.transform.localScale; 
            startPos = fireObj.transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_state  && _light.intensity < maxIntensity)
            _light.intensity += Time.deltaTime * fadeSpeed; 
        else if (_light.intensity > 0)
            _light.intensity -= Time.deltaTime * fadeSpeed; 

        if(fireObj)
        {
            if(!_state  && fireObj.transform.localScale != FireScaleDown)
            {
                _fade -= fadeSpeed * Time.deltaTime;
                Vector3 scale = Vector3.Lerp(FireScaleDown, StartScale, _fade);
                fireObj.transform.localScale = scale;
                Vector3 pos = Vector3.Lerp(FireScaledPos, startPos, _fade);
                fireObj.transform.localPosition = pos;
            }
            if (_state && fireObj.transform.localScale != StartScale)
            {
                _fade += fadeSpeed * Time.deltaTime;
                Vector3 scale = Vector3.Lerp(FireScaleDown, StartScale, _fade);
                fireObj.transform.localScale = scale;
                Vector3 pos = Vector3.Lerp(FireScaledPos, startPos, _fade);
                fireObj.transform.localPosition = pos;
            }
        }
    }

    public bool fadeLightSate
    {
        set =>  _state = value;
    }
}
