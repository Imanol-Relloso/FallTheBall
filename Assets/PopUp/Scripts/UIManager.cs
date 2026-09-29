using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<UIWindow> _uiWindows;

    public List<UIWindow> UIWindows => _uiWindows;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ShowWindow(string windowName)
    {
        UIWindow window = GetWindow(windowName);

        if (window != null)
        {
            Debug.Log($"Showing window: {windowName}");
            window.Show();
        }
        else
            Debug.LogError("Window not found: " + windowName);
    }

    public void HideWindow(string windowName)
    {
        UIWindow window = GetWindow(windowName);
        
        if (window.Id == windowName)
        {
            Debug.Log($"Hiding window: {windowName}");
            window.Hide();
        }
        else
            Debug.LogError("Window not found: " + windowName);
    }
    

    public string GetActiveWindowID()
    {
        foreach (var window in _uiWindows)
        {
            if (window.IsShowing)
            {
                return window.Id;
            }
        }
        return null;
    }
    public UIWindow GetWindow(string id)
    {
        foreach (var window in _uiWindows)
        {
            if (window.Id == id)
            {
                return window;
            }
        }
        return null;
    }
}