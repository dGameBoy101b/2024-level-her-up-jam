using UnityEngine;

public class Card : MonoBehaviour, IUpdateCardStats
{
	public CardStats Stats;

	public void UpdateCardStats(CardStats stats)
	{
		this.Stats = stats;
		foreach (var component in this.GetComponentsInChildren<IUpdateCardStats>())
			component.UpdateCardStats(stats);
	}

	private void Start()
	{
		this.UpdateCardStats(this.Stats);
	}
}
