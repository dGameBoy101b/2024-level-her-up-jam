using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CardStack))]
public class Deck : MonoBehaviour
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

	public UnityEvent OnRequestReshuffle = new();

	public void Reshuffle(IEnumerable<CardStats> cards)
	{
		foreach (var card in cards)
			this.Stack.Add(card);
		Debug.Log("Reshuffled", this);
	}

	public CardStats DrawCard()
	{
		if (this.Stack.Count < 1)
			this.OnRequestReshuffle.Invoke();
		int index = Random.Range(0, this.Stack.Count - 1);
		var card = this.Stack[index];
		this.Stack.RemoveAt(index);
		Debug.Log($"Drew card {index}: {card.DisplayName}", this);
		return card;
	}
}
