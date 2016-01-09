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
                instance_ = FindObjectOfType<CameraManager>();
                Assert.IsTrue(instance_ != null, "You must place the CameraManager Prefab into the scene!");
            }

            return instance_;
        }
    }

    public GameObject blankCamera;
    private List<Camera> cameras;
    private Rect[] viewports;
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    private bool viewportsSetUp = false;
#endif

    private void Awake()
    {
    }

    public int RegisterCamera(Camera camera)
    {
        if(cameras == null)
          cameras = new List<Camera>();

        cameras.Add(camera);
        return cameras.Count-1;
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

        viewports = rects;

        int i;
        for(i = 0; i < camerasSize; i++)
        {
            cameras[i].rect = rects[i];
        }
        for(; i < rects.Length; i++)
        {
            Camera extra = CreateBlack();
            extra.rect = rects[i];
        }
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        viewportsSetUp = true;
#endif
    }

    public Camera CreateBlack()
    {
        Camera extra = (Instantiate(blankCamera, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<Camera>();
        extra.transform.SetParent(transform);
        return extra;
    }

    public void GoBlack(int cameraIndex, Camera camera)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Assert.IsTrue( viewportsSetUp, "A instantiated camera has gone black before viewports were set up!" );
#endif
        Assert.IsTrue( 0 <= cameraIndex && cameraIndex < viewports.Length );

        CreateBlack().rect = viewports[cameraIndex];
        camera.enabled = false;
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
