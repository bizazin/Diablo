using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TargetPointer : MonoBehaviour
{

	#region Singleton

	public static TargetPointer Instance;

	private void Awake()
	{
		if (Instance != null)
			return;
		Instance = this;
	}
	#endregion

	[SerializeField] private RectTransform pointerUI;
	[SerializeField] private Sprite outOfScreenIcon; 	
	[SerializeField] private Sprite pointerIcon;
	[SerializeField] private float interfaceScale;

	private Vector3 startPointerSize;
	private Camera mainCamera;
	
	public Transform Target;

	private void Start()
	{
		interfaceScale = 100;
		startPointerSize = pointerUI.sizeDelta;
		mainCamera = Camera.main;		
	}

	private void LateUpdate()
	{
		if (Target == null)
			return;
		
		Vector3 realPos = mainCamera.WorldToScreenPoint(Target.position); 
		Rect rect = new Rect(0, 0, Screen.width, Screen.height);

		Vector3 outPos = realPos;
		float direction = 1;

		pointerUI.GetComponent<Image>().sprite = outOfScreenIcon;

		if (!IsBehind(Target.position)) 
			if (rect.Contains(realPos)) 
			 	pointerUI.GetComponent<Image>().sprite = pointerIcon;
		else 
		{
			realPos = -realPos;
			outPos = new Vector3(realPos.x, 0, 0); 
			if (mainCamera.transform.position.y < Target.position.y)
			{
				direction = -1;
				outPos.y = Screen.height; 				
			}
		}
		
		float offset = pointerUI.sizeDelta.x / 2;
		outPos.x = Mathf.Clamp(outPos.x, offset, Screen.width - offset);
		outPos.y = Mathf.Clamp(outPos.y, offset, Screen.height - offset);

		Vector3 pos = realPos - outPos;  

		RotatePointer(direction * pos);

		pointerUI.sizeDelta = new Vector2(startPointerSize.x / 100 * interfaceScale, startPointerSize.y / 100 * interfaceScale);
		pointerUI.anchoredPosition = outPos;
	}

	private bool IsBehind(Vector3 point) 
	{		
		Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);
		Vector3 toOther = point - mainCamera.transform.position;
		return Vector3.Dot(forward, toOther) < 0;
	}

	private void RotatePointer(Vector2 direction) 
	{		
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		pointerUI.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void ToggleTarget(bool state)
	{
		var image = GetComponent<Image>();
		var tempColor = image.color;
		tempColor.a = Convert.ToInt32(state);
		image.color = tempColor;
		pointerUI.GetComponent<Image>().color = tempColor;
	}

}