using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Card : MonoBehaviour, IUpdateCardStats, IUpdateSortOrder
{
	#region Stats
	public CardStats Stats;

	public void UpdateCardStats(CardStats stats)
	{
		this.Stats = stats;
		foreach (var component in this.GetComponentsInChildren<IUpdateCardStats>())
		{
			if (component is Card)
				continue;
			component.UpdateCardStats(stats);
		}
	}
	#endregion

	#region Playing
	public int HandIndex
	{
		get
		{
			for (int index = 0; index < this.transform.parent.childCount; index++)
				if (this.transform.IsChildOf(this.transform.parent.GetChild(index)))
					return index;
			return -1;
		}
	}

	public void Play()
	{
		var hand = this.GetComponentInParent<Hand>();
		hand.Play(this.HandIndex);
	}
	#endregion

	#region Sort Order
	private Canvas _canvas;
	public Canvas Canvas
	{
		get
		{
			if (this._canvas == null)
				this._canvas = this.GetComponent<Canvas>();
			return this._canvas;
		}
	}

	public void UpdateSortOrder(int order)
	{
		this.Canvas.sortingOrder = order;
	}

	public void RecalculateSortOrders()
	{
		this.GetComponentInParent<Hand>().SetSortOrders();
	}
	#endregion

	private void Start()
	{
		this.UpdateCardStats(this.Stats);
	}
}
