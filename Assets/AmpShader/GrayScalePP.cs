using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GrayScalePP : MonoBehaviour
{
    public Shader shader;

    public float intensity;
    public Color color;
    private Material material;

    private void Start()
    {
        material = new Material(shader);
    }
    private void Update()
    {
        material.SetFloat("_intensity",intensity);
        material.SetColor("_color",color);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination,material);
    }
}
