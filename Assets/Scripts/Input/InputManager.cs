using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
	#region Events
	public delegate void MouseClickEvent(Vector2 position);
	public event MouseClickEvent OnMouseClickEvent;
	#endregion

	private Controls controls;

	// Quick singleton
	private static InputManager instance;
	public static InputManager Instance
	{
		get
		{
			if( instance == null )
			{
				instance = FindObjectOfType<InputManager>();
				
				if( instance == null )
				{
					GameObject obj = new GameObject();
					instance = obj.AddComponent<InputManager>();
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		controls = new Controls();
	}

	private void OnEnable()
	{
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}

	private void Start()
	{
		controls.Mouse.Click.started += ctx => MouseClick(ctx);
	}

	private void MouseClick(InputAction.CallbackContext context)
	{
		if(OnMouseClickEvent != null)
		{
			OnMouseClickEvent(controls.Mouse.Point.ReadValue<Vector2>());	
		}
	}
}
