using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
		var card_stats = this.transform.GetChild(index).GetComponent<Card>().Stats;
		Debug.Log($"Playing card {index}: {card_stats.DisplayName}", this);
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
