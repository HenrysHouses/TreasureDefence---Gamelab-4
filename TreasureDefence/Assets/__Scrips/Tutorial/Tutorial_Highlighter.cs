using UnityEngine;
using TMPro;
public class Tutorial_Highlighter : MonoBehaviour
{
    [Header("Activation For Next Tutorial")]
    [SerializeField] float minPosition;
    [SerializeField] bool onDisable, onDestroy;
    [SerializeField] GameObject nextTutorial;

    [SerializeField] Transform heightOffset;

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.up);
        Vector3 dir = Camera.main.transform.position - transform.position;
        float offset = Vector3.Dot(forward, dir);

        Vector3 pos = heightOffset.localPosition;
        if(offset > minPosition)
            pos.y = offset;
        else
            pos.y = minPosition;

        heightOffset.localPosition = pos;
    }

    [SerializeField] TextMeshPro Tutorial;

    public void setTutorialText(string text)
    {
        Tutorial.text = text;
    }

    void OnDisable()
    {
        if(onDisable && nextTutorial)
            nextTutorial.SetActive(true);
    }

    void OnDestroy()
    {
        if(onDestroy && nextTutorial)
            nextTutorial.SetActive(true);
    }
}
