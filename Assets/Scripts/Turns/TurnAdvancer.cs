using UnityEngine;

public class TurnAdvancer : MonoBehaviour
{
	public void AdvanceTurn()
	{
		TurnTracker.Instance.AdvanceCount();
	}
}
