using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class ShaderManager : MonoBehaviour
{
    [SerializeField, Range(1, 1000)] int amountOfBlocks = 1;

    [SerializeField] Shader pixelShader;
    Material pixelMaterial;

    RenderTexture originalSceneTexture;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if (originalSceneTexture == null || originalSceneTexture.width != source.width || originalSceneTexture.height != source.height)
        {
            if (originalSceneTexture != null)
                originalSceneTexture.Release();

            originalSceneTexture = new RenderTexture(source.width, source.height, 0, source.graphicsFormat);
            originalSceneTexture.filterMode = FilterMode.Point;
            originalSceneTexture.Create();
        }

        InitFrame();

        Graphics.Blit(source, originalSceneTexture);

        Graphics.Blit(null, destination, pixelMaterial);
    }

    void InitFrame()
    {
        InitMaterial(pixelShader, ref pixelMaterial);
        pixelMaterial.SetInt("AmountOfBlocks", amountOfBlocks);
        pixelMaterial.SetInt("TextureWidth", originalSceneTexture.width);
        pixelMaterial.SetInt("TextureHeight", originalSceneTexture.height);
        pixelMaterial.SetTexture("_OriginalScene", originalSceneTexture);
    }

    void InitMaterial(Shader shader, ref Material mat)
    {
        if (mat == null || (mat.shader != shader && shader != null))
        {
            if (shader == null)
            {
                shader = Shader.Find("Unlit/Texture");
            }

            mat = new Material(shader);
        }
    }

    void OnDestroy()
    {
        if (originalSceneTexture != null)
        {
            originalSceneTexture.Release();
            Destroy(originalSceneTexture);
        }
    }
}
