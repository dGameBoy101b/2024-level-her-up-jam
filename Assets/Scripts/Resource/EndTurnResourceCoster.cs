using UnityEngine;

[RequireComponent(typeof(ResourceTracker))]
public class EndTurnResourceCoster : MonoBehaviour, IEndTurn
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

	public void TurnEnd(int count)
	{
		this.Tracker.Amount -= this.Tracker.Threshold;
	}
}
