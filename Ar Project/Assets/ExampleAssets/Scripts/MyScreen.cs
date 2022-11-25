using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class MyScreen : MonoBehaviour
{
    [SerializeField]
    GameObject blink;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject sTOP;
    [SerializeField]
    GameObject Option;
    [SerializeField]
    GameObject Exit;
    private bool expanded;
   
    public void TakePic()
    {
        canvas.SetActive(false);
        StartCoroutine("Capture");
    }
    public void Vid()
    {
        sTOP.GetComponent<CanvasGroup>().alpha = 0; 
        sTOP.SetActive(true);
       
    }
    public void GoGallery()
    {

        //sTOP.SetActive(false);
        PickImage(512);
    }
    public void Options()
    {
      
        expanded = !expanded;
        Option.GetComponent<Animator>().SetBool("expanded", expanded);

    }
    public void ExMenu()
    {
        Exit.SetActive(true);
    }
    public void Ex()
    {
        Application.Quit();
    }
    public void ExMenuOff()
    {
        Exit?.SetActive(false);
    }
    IEnumerator Capture()
    {

        yield return new WaitForEndOfFrame();
        blink.SetActive(true);
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

        Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(ss);

        yield return new WaitForEndOfFrame();
        blink.SetActive(false);
        canvas.SetActive(true); 
    }
   
    private void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                Material material = sTOP.GetComponent<RawImage>().material;
                sTOP.GetComponent<CanvasGroup>().alpha = 1;
                if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                material.mainTexture = texture;
                sTOP.GetComponent<RawImage>().texture = material.mainTexture;
            }
        });

        Debug.Log("Permission result: " + permission);
    }

}
