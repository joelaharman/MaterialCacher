using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialCacher : MonoBehaviour
{
    private static MaterialCacher instance;
    private Camera cacheCamera;

    /// <summary>
    /// Cache the material on the specified object.
    /// </summary>
    public static void Cache (CachableMaterialController textureCacher)
    {
        if (instance == null) Setup();
        instance.cacheCamera.transform.position = Camera.main.transform.position;

        // Change the layer of the specific object to the layer.
        int layer = textureCacher.gameObject.layer;
        textureCacher.gameObject.layer = LayerMask.NameToLayer("Cache");
        

        // Render the texture.
        instance.cacheCamera.enabled = true;
        instance.cacheCamera.targetTexture = textureCacher.renderTexture;
        instance.cacheCamera.Render();
        instance.cacheCamera.enabled = false;

        // Reset the layer.
        textureCacher.gameObject.layer = layer;
    }

    public static void Uncache(CachableMaterialController textureCacher)
    {
        if (instance == null) Setup();
        textureCacher.renderer.material = textureCacher.mainMaterial;
    }
    
    public static void Setup ()
    {
        GameObject obj = new GameObject("CacheCamera");
        
        // Set up the camera.
        Camera cam = obj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.clear;
        int layerIndex = LayerMask.NameToLayer("Cache");
        cam.cullingMask = 1 << layerIndex;
        cam.enabled = false;

        // Material caching properties for future use.
        instance = obj.AddComponent<MaterialCacher>();
        instance.cacheCamera = cam;
    }
}
