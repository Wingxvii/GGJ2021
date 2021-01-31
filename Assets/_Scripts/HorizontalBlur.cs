using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// taken from https://www.ronja-tutorials.com/post/016-postprocessing-basics/
public class HorizontalBlur : MonoBehaviour
{
    //material that's applied when doing postprocessing
    [SerializeField]
    public Material horizontalBlurMaterial;
    float blurStrength = 0.1F;

    //method which is automatically called by unity after the camera is done rendering
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //draws the pixels from the source texture to the destination texture
        Graphics.Blit(source, destination, horizontalBlurMaterial);
    }

    public void ChangeBlurAmount(float percentage) {
        float blur_size_value = percentage * blurStrength; // the blurring is too stronk :p
        blur_size_value = (float)(blur_size_value * 0.5); // _BlurSize is a range from 0-0.5
        horizontalBlurMaterial.SetFloat("_BlurSize", blur_size_value);
    }
}
