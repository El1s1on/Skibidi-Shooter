using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AutoBlur : MonoBehaviour
{
    private DepthOfField depthOfField;
    private float blurValue;

    private void Awake()
    {
        PostProcessVolume postProcessing = FindObjectOfType<PostProcessVolume>();

        if (postProcessing == null) return;
        postProcessing.profile.TryGetSettings(out depthOfField);
    }

    private void Update()
    {
        if (depthOfField == null) return;

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            blurValue = 0f;
        }
        else
        {
            blurValue = 20f;
        }

        depthOfField.focalLength.value = blurValue;
    }
}
