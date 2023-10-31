using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImg : MonoBehaviour
{
    public float _timer;

    public ARTrackedImageManager trackedImageManager;

    public List<GameObject> _objectList = new List<GameObject>();

    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();

    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();

    private List<float> _trackedTimer = new List<float>();

    public List<AudioClip> list2 = new List<AudioClip>();

    private Dictionary<string, AudioClip> _prefabDic2 = new Dictionary<string, AudioClip>();    
    void Awake()
    {
        foreach (GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj);
        }

        foreach (AudioClip obj in list2)
        {
            _prefabDic2.Add(obj.name, obj);
        }
    }

    void Update()
    {
        if(_trackedImg.Count > 0)
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();
            for(var i = 0; i <  _trackedImg.Count; i++)
            {
                if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {
                    if (_trackedTimer[i] > _timer)
                    {
                        string name = _trackedImg[i].referenceImage.name;
                        GameObject tObj = _prefabDic[name];
                        tObj.SetActive(false);
                        tNumList.Add(_trackedImg[i]);
                    }
                    else
                    {
                        _trackedTimer[i] += Time.deltaTime;
                    }
                }
            }

            if (tNumList.Count > 0)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    int num = _trackedImg.IndexOf(tNumList[i]);
                    _trackedImg.Remove(_trackedImg[num]);
                    _trackedTimer.Remove(_trackedTimer[num]);
                }
            }
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;   
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if(!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
                UpdateSound(trackedImage);
            }
            //UpdateSound(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if(!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
                _trackedTimer.Add(0);
            }
            else
            {
                int num = _trackedImg.IndexOf(trackedImage);
                //추가한 부분 Limited상태가 아닐때 타이머를 0으로 만듬
                if (_trackedImg[num].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
                {

                }
                else
                {
                    _trackedTimer[num] = 0;
                }
            }
            UpdateImage(trackedImage);
            
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        int num = _trackedImg.IndexOf(trackedImage);
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDic[name];
        //추가한 부분 Limited상태가 아닐때(트랙킹중이 아닐때),
        //셋액티브true와 위치 동기화를 하지 않음
        if (_trackedImg[num].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
        {

        }
        else
        {
            tObj.transform.position = trackedImage.transform.position;
            tObj.transform.rotation = Quaternion.Euler(0f, 2f, 0f);
            tObj.SetActive(true);
        }
    }

    /*private void UpdateSound(ARTrackedImg trackedImage)
    {
        string name = trackedImage.name;

        AudioClip sound = _prefabDic2[name];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }*/

    private void UpdateSound(ARTrackedImage trackedImage)
    {
        int num = _trackedImg.IndexOf(trackedImage);
        string name = trackedImage.referenceImage.name;
        
        AudioClip sound = _prefabDic2[name];
        if (_trackedImg[num].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
        {

        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
        
    }
}
