using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// taken from https://www.ronja-tutorials.com/post/016-postprocessing-basics/
public class GhostMode : MonoBehaviour
{
    //material that's applied when doing postprocessing
    [SerializeField]
    public Material ghostModeMaterial;
    bool ghostToggle = true;

    //method which is automatically called by unity after the camera is done rendering
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //draws the pixels from the source texture to the destination texture
        if (ghostToggle) {
            var temporaryTexture = RenderTexture.GetTemporary(source.width, source.height);
            Graphics.Blit(source, temporaryTexture, ghostModeMaterial, 0); //horizontal blur 1st pass
            Graphics.Blit(temporaryTexture, destination, ghostModeMaterial, 1); //vertical blur 2nd pass
            RenderTexture.ReleaseTemporary(temporaryTexture);
        }
        else {
            Graphics.Blit(source, destination);
        }
    }

    public void SetGhostMode() {
        ghostToggle = true;
    }
    public void UnsetGhostMode() {
        ghostToggle = false;
    }
}
