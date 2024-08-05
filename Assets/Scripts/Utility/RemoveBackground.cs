using UnityEngine;
using System.IO;

public class RemoveBackground : MonoBehaviour
{
    public Texture2D inputTexture;

    void Start()
    {
        if (inputTexture != null)
        {
            Texture2D outputTexture = RemoveMagenta(inputTexture);
            SaveTextureAsPNG(outputTexture, "Assets/OutputTexture.png");
        }
    }

    Texture2D RemoveMagenta(Texture2D originalTexture)
    {
        Color magenta = new Color(1, 0, 1, 1); // 마젠타 색상
        Texture2D newTexture = new Texture2D(originalTexture.width, originalTexture.height);

        for (int y = 0; y < originalTexture.height; y++)
        {
            for (int x = 0; x < originalTexture.width; x++)
            {
                Color pixelColor = originalTexture.GetPixel(x, y);
                if (pixelColor == magenta)
                {
                    pixelColor.a = 0; // 투명하게 설정
                }
                newTexture.SetPixel(x, y, pixelColor);
            }
        }

        newTexture.Apply();
        return newTexture;
    }

    void SaveTextureAsPNG(Texture2D texture, string filePath)
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("Texture saved as PNG at: " + filePath);
    }
}