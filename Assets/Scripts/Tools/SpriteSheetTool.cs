using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpriteSheetTool : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    private float _errorRound = 0.05f;
    
    [ContextMenu("RemoveBackground")]
    void RemoveBackground()
    {
        Texture2D copyTexture = new Texture2D(_texture.width, _texture.height, TextureFormat.ARGB32, false);
        copyTexture.SetPixels(_texture.GetPixels());
        copyTexture.Apply();
        var pixelToRemove = copyTexture.GetPixel(0, 0);
        Debug.Log(pixelToRemove.r + " " + pixelToRemove.g + " " + pixelToRemove.b + " " + pixelToRemove.a);
        Debug.Log(copyTexture.width + " " + copyTexture.height);
        for (var x = 0; x < copyTexture.width; x++)
        {
            for (var y = 0; y < copyTexture.height; y++)
            {
                var pixel = copyTexture.GetPixel(x, y);
                if(Math.Abs(pixel.r - pixelToRemove.r) <= _errorRound && Math.Abs(pixel.g - pixelToRemove.g) <= _errorRound)
                {
                    copyTexture.SetPixel(x, y, Color.clear);
                }
            }
        }
        SaveTexture(copyTexture);
      
    }

    [ContextMenu("ResetTool")]
    void ResetTool()
    {
    }
    
    private void SaveTexture(Texture2D texture)
    {
        
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/RenderOutput";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        System.IO.File.WriteAllBytes(dirPath + "/R_" + UnityEngine.Random.Range(0, 100000) + ".png", bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}