using UnityEngine;
using UnityEngine.Assertions;

using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance_;
    public static CameraManager Instance
    {
        get
        {
            if(instance_ == null)
            {
                var go = new GameObject("CameraManager (Instantiated)");
                instance_ = go.AddComponent<CameraManager>();
            }

            return instance_;
        }
    }

    private List<Camera> cameras;

    private void Awake()
    {
        cameras = new List<Camera>();
    }

    public void RegisterCamera(Camera camera)
    {
        cameras.Add(camera);
    }

    public void SetUpViewports()
    {
        int camerasSize = cameras.Count;
        Rect[] rects = GetViewports(camerasSize);

        //Shuffle rectangles
        int n = rects.Length;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n+1);
            Rect v  = rects[k];  
            rects[k] = rects[n];  
            rects[n] = v;  
        }  

        for(int i = 0; i < camerasSize; i++)
        {
            cameras[i].rect = rects[i];
        }
    }

    private Rect[] GetViewports(int size)
    {
        switch(size)
        {
            case 1:
                return new Rect[]
                {
                    new Rect(0, 0, 1, 1),
                };
            case 2:
                return new Rect[]
                {
                    new Rect(0  , 0, .5f, 1),
                    new Rect(.5f, 0, .5f, 1),
                };
            case 3:
                return new Rect[]
                {
                    new Rect(0  , 0  , .5f, .5f),
                    new Rect(0  , .5f, .5f, .5f),
                    new Rect(.5f, 0  , .5f, 1  ),
                };
            case 4:
                return new Rect[]
                {
                    new Rect(0    , 0  , .5f, .5f),
                    new Rect(0    , .5f, .5f, .5f),
                    new Rect(.5f  , 0  , .5f, .5f),
                    new Rect(.5f  , .5f, .5f, .5f),
                };
            case 5:
            case 6:
                return new Rect[]
                {
                    new Rect(0    , 0  , .33f, .5f),
                    new Rect(.33f , 0  , .33f, .5f),
                    new Rect(.66f , 0  , .33f, .5f),
                    new Rect(0    , .5f, .33f, .5f),
                    new Rect(.33f , .5f, .33f, .5f),
                    new Rect(.66f , .5f, .33f, .5f),
                };
            case 7:
            case 8:
            case 9:
                return new Rect[]
                {
                    new Rect(0    , 0  , .33f,.33f),
                    new Rect(.33f , 0  , .33f,.33f),
                    new Rect(.66f , 0  , .33f,.33f),
                    new Rect(0    ,.33f, .33f,.33f),
                    new Rect(.33f ,.33f, .33f,.33f),
                    new Rect(.66f ,.33f, .33f,.33f),
                    new Rect(0    ,.66f, .33f,.33f),
                    new Rect(.33f ,.66f, .33f,.33f),
                    new Rect(.66f ,.66f, .33f,.33f),
                };
            default:
                Assert.IsTrue(false, "Encountered invalid number of cameras: " + size);
                return new Rect[]{};
        }
    }
}
