using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardStack : MonoBehaviour, ICollection<CardStats>
{
	[SerializeField]
	protected List<CardStats> _cards = new();

	public UnityEvent<int> OnCountChange = new();

	#region Collection
	public void Add(CardStats item)
	{
		this._cards.Add(item);
		this.OnCountChange.Invoke(this.Count);
	}

	public void Clear()
	{
		this._cards.Clear();
		this.OnCountChange.Invoke(0);
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
		this.OnCountChange.Invoke(this.Count);
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
	#endregion
}
