using UnityEngine;

[ExecuteAlways]
public class StickToTransform : MonoBehaviour
{
	public Transform StickTo;
	public bool Rotation;
	public bool Late;
	
	private void LateUpdate()
	{
		if (!Late) return;
		
		if (StickTo)
		{
			transform.position = StickTo.position;
			if (Rotation) transform.rotation = StickTo.rotation;
		}
	}

	void Update () 
	{
		if (Late) return;
		if (StickTo)
		{
			transform.position = StickTo.position;
			if (Rotation) transform.rotation = StickTo.rotation;
		}
	}
}
