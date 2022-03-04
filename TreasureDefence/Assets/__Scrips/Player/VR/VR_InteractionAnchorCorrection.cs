using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VR_InteractionAnchorCorrection : MonoBehaviour
{
    XRRayInteractor grabInteractable;
    [SerializeField] Transform _attachTransform;
    Quaternion startRotation = new Quaternion(0.415974647f,0,0,0.909376204f), activeRotation = new Quaternion(0,0,0,1);

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRRayInteractor>();
        _attachTransform = findRayOrigin();
    }

    public void toggleTransformAnchor()
    {
        if(_attachTransform)
        {
            if(grabInteractable.attachTransform.rotation == startRotation)
            {
                grabInteractable.attachTransform.rotation = Quaternion.identity;
            }
            else
            {
                grabInteractable.attachTransform.rotation = startRotation;
            }
        }
    }

    Transform findRayOrigin()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name.Contains("Ray Origin"))
            {
                return transform.GetChild(i);
            }
        }
        return null;
    }
}
