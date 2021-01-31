using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// taken from https://www.ronja-tutorials.com/post/016-postprocessing-basics/
public class Postprocessing : MonoBehaviour
{
    //material that's applied when doing postprocessing
    [SerializeField]
    private Material postprocessMaterial;

    //method which is automatically called by unity after the camera is done rendering
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //draws the pixels from the source texture to the destination texture
        Graphics.Blit(source, destination, postprocessMaterial);
    }
}
