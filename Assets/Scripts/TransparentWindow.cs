using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{

        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }
        
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        
        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        
        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);
        
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern int SetWindowPos(IntPtr hwnd, int hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);
        
        [DllImport("Dwmapi.dll")]
        private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
        
        const int GWL_STYLE = -16;
        const int GWL_EXSTYLE = -20;
        const uint WS_POPUP = 0x80000000;
        const uint WS_VISIBLE = 0x10000000;
        const uint WS_EX_LAYERED = 0x00080000;
        const uint WS_EX_TRANSPARENT = 0x00000020;
        const int HWND_TOPMOST = -1;
        const int LWA_ALPHA = 2;
        
        
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_EDITOR
        /*int fWidth = Screen.width;
        int fHeight = Screen.height;*/
        var margins = new MARGINS() { cxLeftWidth = -1 };
        var hwnd = GetActiveWindow();
        
        SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        SetLayeredWindowAttributes(hwnd, 0, 255, 2);
        //SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, fWidth, fHeight, 32 | 64); 
        SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0, 0); 
        DwmExtendFrameIntoClientArea(hwnd, ref margins);
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
