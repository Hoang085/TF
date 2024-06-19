using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Image uiImage; // Reference to the UI Image component
    public string imageName1 = "Image1"; // Name of the first image in Resources
    public string imageName2 = "Image2"; // Name of the second image in Resources

    private bool isFirstImage = true;

    void Start()
    {
        // Load the first image at the start
        Sprite newSprite = Resources.Load<Sprite>(imageName1);
        if (newSprite != null)
        {
            uiImage.sprite = newSprite;
        }
        else
        {
            Debug.LogError("Failed to load image: " + imageName1);
        }
    }

    public void ChangeImageOnClick()
    {
        // Toggle between the two images
        string imageNameToLoad = isFirstImage ? imageName2 : imageName1;
        Sprite newSprite = Resources.Load<Sprite>(imageNameToLoad);

        if (newSprite != null)
        {
            uiImage.sprite = newSprite;
            isFirstImage = !isFirstImage; // Toggle the flag
        }
        else
        {
            Debug.LogError("Failed to load image: " + imageNameToLoad);
        }
    }
}