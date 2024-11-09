using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour, IList<CardStats>
{
	[SerializeField]
	private List<CardStats> _cards = new();

	private void UpdateCardCount()
	{
		foreach (var component in this.GetComponentsInChildren<IUpdateCardCount>())
			component.UpdateCardCount(this.Count);
	}

	#region List
	public void Add(CardStats item)
	{
		this._cards.Add(item);
		this.UpdateCardCount();
	}

	public void Clear()
	{
		this._cards.Clear();
		this.UpdateCardCount();
	}

	public bool Contains(CardStats item)
	{
		return this._cards.Contains(item);
	}

	public void CopyTo(CardStats[] array, int arrayIndex)
	{
		this._cards.CopyTo(array, arrayIndex);
	}

	public bool Remove(CardStats item)
	{
		bool removed = this._cards.Remove(item);
		this.UpdateCardCount();
		return removed;
	}

	public int Count => this._cards.Count;

	public bool IsReadOnly => false;

	public IEnumerator<CardStats> GetEnumerator()
	{
		return this._cards.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	public int IndexOf(CardStats item)
	{
		return this._cards.IndexOf(item);
	}

	public void Insert(int index, CardStats item)
	{
		this._cards.Insert(index, item);
		this.UpdateCardCount();
	}

	public void RemoveAt(int index)
	{
		this._cards.RemoveAt(index);
		this.UpdateCardCount();
	}

	public CardStats this[int index] 
	{
		get => this._cards[index];
		set => this._cards[index] = value;
	}
	#endregion
}
