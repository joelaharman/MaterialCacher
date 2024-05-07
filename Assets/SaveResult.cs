using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveResult : MonoBehaviour
{
    private int width = 1024;
    private int height = 1024;
    public RenderTexture renderTexture;
    private Material mainMaterial;
    private Material cachedMaterial;
    private new Renderer renderer;

    private bool useDynamicMaterial = true;


    /// <summary>
    /// 
    /// </summary>
    private static void CacheTextureResult(Material material)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        renderer = GetComponent<Renderer>();
        cachedMaterial = new Material(Shader.Find("Standard"));
        cachedMaterial.mainTexture = renderTexture;
        mainMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (useDynamicMaterial)
                SwapToCachedMaterial();
            else
                SwapToMainMaterial();
        }
    }

    private void SwapToCachedMaterial ()
    {
        Graphics.Blit(renderTexture, mainMaterial);
        renderer.material = cachedMaterial; 
        useDynamicMaterial = false;
    }

    private void SwapToMainMaterial()
    {
        renderer.material = mainMaterial;
        useDynamicMaterial = true;
    }
}
