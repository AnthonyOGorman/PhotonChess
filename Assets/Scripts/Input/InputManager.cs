using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
	#region Events
	public delegate void PressDownEvent(Vector2 position);
	public event PressDownEvent OnPressDown;
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
		controls.Gameplay.Press.started += ctx => PressDown(ctx);
	}

	private void PressDown(InputAction.CallbackContext context)
	{
		if(OnPressDown != null)
		{
			OnPressDown(controls.Gameplay.Position.ReadValue<Vector2>());	
		}
	}
}
