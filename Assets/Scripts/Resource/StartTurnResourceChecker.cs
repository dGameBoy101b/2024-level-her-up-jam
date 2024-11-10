using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ResourceTracker))]
public class StartTurnResourceChecker : MonoBehaviour, IStartTurn
{
	private ResourceTracker _tracker;
	public ResourceTracker Tracker
	{
		get
		{
			if (this._tracker == null)
				this._tracker = this.GetComponent<ResourceTracker>();
			return this._tracker;
		}
	}

	public UnityEvent<ResourceType> OnFail = new();

	public void TurnStart(int count)
	{
		if (this.Tracker.Amount >= 0)
			return;
		this.OnFail.Invoke(this.Tracker.Type);
	}
}
