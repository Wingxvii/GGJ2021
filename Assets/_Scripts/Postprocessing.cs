﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// taken from https://www.ronja-tutorials.com/post/016-postprocessing-basics/
public class Postprocessing : MonoBehaviour
{
    //material that's applied when doing postprocessing
    [SerializeField]
    public Material postprocessMaterial;
    float blurStrength = 0.5F;

    //method which is automatically called by unity after the camera is done rendering
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        //draws the pixels from the source texture to the destination texture
        Graphics.Blit(source, destination, postprocessMaterial);
    }

    public void ChangeBlurAmount(float percentage) {
        float blur_size_value = percentage * blurStrength; // the blurring is too stronk :p
        blur_size_value = (float)(blur_size_value * 0.5); // _BlurSize is a range from 0-0.5
        postprocessMaterial.SetFloat("_BlurSize", blur_size_value);
    }
}
