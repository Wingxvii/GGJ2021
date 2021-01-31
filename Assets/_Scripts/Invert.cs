using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// taken from https://www.ronja-tutorials.com/post/016-postprocessing-basics/
public class Invert : MonoBehaviour
{
    //material that's applied when doing postprocessing
    [SerializeField]
    public Material invertMaterial;
    bool invertToggle = false;

    //method which is automatically called by unity after the camera is done rendering
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //draws the pixels from the source texture to the destination texture
        if (invertToggle) {
            Graphics.Blit(source, destination, invertMaterial);
        } else {
            Graphics.Blit(source, destination);
        }
    }

    public void SetInvert() {
        invertToggle = true;
    }
    public void UnsetInvert() {
        invertToggle = false;
    }
}
