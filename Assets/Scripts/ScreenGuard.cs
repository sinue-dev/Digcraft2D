using UnityEngine;

/// <summary>
/// Should use temporarily until Unity3d-team fix the bug
/// http://issuetracker.unity3d.com/issues/input-when-resizing-window-input-dot-mouseposition-will-be-clamped-by-the-original-window-size
/// </summary>
class ScreenGuard : MonoBehaviour
{
	private int prevWidth;
	private int prevHeight;

	private bool isInited = false;


	private void Init()
	{
		prevWidth = Screen.width;
		prevHeight = Screen.height;

		isInited = true;
	}

	private void SetResolution()
	{
		prevWidth = Screen.width;
		prevHeight = Screen.height;

		Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);
	}

	public void Update()
	{
		if (!isInited)
		{
			Init();
		}

		if ((Screen.width != prevWidth) || (Screen.height != prevHeight))
		{
			SetResolution();
		}
	}
}