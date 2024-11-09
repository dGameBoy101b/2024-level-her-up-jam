using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CardStack))]
public class Discard : MonoBehaviour
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

	public UnityEvent<IEnumerable<CardStats>> OnReshuffle = new();

	public void Reshuffle()
	{
		List<CardStats> cards = new(this.Stack);
		this.Stack.Clear();
		this.OnReshuffle.Invoke(cards);
	}
}
