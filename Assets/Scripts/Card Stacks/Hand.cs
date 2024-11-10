using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardStack))]
public class Hand : MonoBehaviour, IStartTurn, IEndTurn
{
	private CardStack _stack;
	public CardStack Stack
	{
		get
		{
			if (this._stack == null)
				this._stack = this.GetComponent<CardStack>();
			return this._stack;
		}
	}

	#region Sort Order
	private int GetIndex(Transform child)
	{
		for (int index = 0; index < this.transform.childCount; index++)
			if (child.IsChildOf(this.transform.GetChild(index)))
				return index;
		return -1;
	}

	public void SetSortOrders()
	{
		var selected = EventSystem.current.currentSelectedGameObject;
		if (!selected.transform.IsChildOf(this.transform))
			return;
		int selected_index = this.GetIndex(selected.transform);
		int max_sort = Math.Max(selected_index, this.transform.childCount - selected_index);
		for (int index = 0; index < this.transform.childCount; index++)
		{
			var child = this.transform.GetChild(index);
			int sort_order = max_sort - Math.Abs(selected_index - index);
			foreach (var component in child.GetComponentsInChildren<IUpdateSortOrder>())
				component.UpdateSortOrder(sort_order);
		}
		Debug.Log("set sort orders",this);
	}
	#endregion

	#region Drawing
	public Card CardPrefab;

	public UnityEvent OnRequestDraw = new();

	public void Draw(int count)
	{
		Debug.Log($"Drawing {count} cards",this);
		for (; count > 0; count--)
			this.OnRequestDraw.Invoke();
	}

	public void OnDraw(CardStats stats)
	{
		this.Stack.Add(stats);
		var card_object = GameObject.Instantiate(this.CardPrefab.gameObject, this.transform);
		var card = card_object.GetComponent<Card>();
		card.UpdateCardStats(stats);
	}
	#endregion

	#region Discarding
	public UnityEvent<IEnumerable<CardStats>> OnDiscard = new();

	public void DiscardAll()
	{
		List<CardStats> cards = new(this.Stack);
		this.Stack.Clear();
		for (int index = 0; index < this.transform.childCount; index++)
			Destroy(this.transform.GetChild(index).gameObject);
		this.OnDiscard.Invoke(cards);
	}

	public void Discard(int index)
	{
		var card = this.Stack[index];
		this.Stack.RemoveAt(index);
		Destroy(this.transform.GetChild(index).gameObject);
		this.OnDiscard.Invoke(new CardStats[] { card });
	}
	#endregion

	#region Playing
	public void Play(int index)
	{
		var card_stats = this.transform.GetChild(index).GetComponentInChildren<Card>().Stats;
		Debug.Log($"Playing card {index}: {card_stats.DisplayName}", this);
		if (!card_stats.CanAfford())
		{
			Debug.LogWarning("Cannot afford");
			return;
		}
		card_stats.ApplyChanges();
		this.Discard(index);
	}
	#endregion

	#region Start Draw
	[Min(0)]
	public int StartDraw = 5;

	public void TurnStart(int count)
	{
		this.Draw(this.StartDraw);
	}
	#endregion

	#region End Discard
	public void TurnEnd(int count)
	{
		this.DiscardAll();
	}
	#endregion
}
