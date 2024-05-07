using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachableMaterialController : MonoBehaviour
{
    private int width = 1024;
    private int height = 1024;
    public RenderTexture renderTexture;
    [HideInInspector] public Material mainMaterial;
    [HideInInspector] public Material cachedMaterial;
    [HideInInspector] public new Renderer renderer;

    void Start()
    {
        renderTexture = new RenderTexture(width, height, 32);
        renderer = GetComponent<Renderer>();
        cachedMaterial = new Material(Shader.Find("Unlit/Texture"));
        cachedMaterial.mainTexture = renderTexture;
        mainMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapToCachedMaterial();
        }
    }

    private void SwapToCachedMaterial ()
    {
        // Update the saved texture.
        MaterialCacher.Cache(this);

        // Swap to the material with the "saved" texture.
        renderer.material = cachedMaterial;
    }

    private void SwapToMainMaterial()
    {
        renderer.material = mainMaterial;
    }

    private void OnMouseDown()
    {
        SwapToMainMaterial();
    }

    private void OnMouseUp()
    {
        SwapToCachedMaterial();
    }
}
