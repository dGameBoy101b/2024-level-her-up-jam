using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CardStats : ScriptableObject
{
	public string DisplayName;

	public Sprite Image;

	[Serializable]
	public struct ResourceChange
	{
		public ResourceType Type;

		public int Change;
	}
	public List<ResourceChange> ChangeList = new();

	#region Costs
	private Dictionary<ResourceType, int> _costs = null;
	public IReadOnlyDictionary<ResourceType, int> Costs
	{
		get
		{
			if (this._costs == null)
				this._costs = this.CalculateCosts();
			return this._costs;
		}
	}

	private Dictionary<ResourceType, int> CalculateCosts()
	{
		Dictionary<ResourceType, int> costs = new();
		foreach (ResourceChange change in this.ChangeList)
		{
			if (change.Change >= 0)
				continue;
			if (costs.ContainsKey(change.Type))
				costs[change.Type] -= change.Change;
			else
				costs[change.Type] = -change.Change;
		}
		return costs;
	}

	public void InvalidateCosts()
	{
		this._costs = null;
	}

	public bool CanAfford()
	{
		return ResourceTracker.CanAfford(this.Costs);
	}
	#endregion

	#region Changes
	private Dictionary<ResourceType, int> _changes = null;
	public IReadOnlyDictionary<ResourceType, int> Changes
	{
		get
		{
			if (this._changes == null)
				this._changes = this.CalculateChanges();
			return this._changes;
		}
	}

	private Dictionary<ResourceType, int> CalculateChanges()
	{
		Dictionary<ResourceType, int> changes = new();
		foreach (var item in this.ChangeList)
		{
			if (changes.ContainsKey(item.Type))
				changes[item.Type] += item.Change;
			else
				changes.Add(item.Type, item.Change);
		}
		return changes;
	}

	public void InvalidateChanges()
	{
		this._changes = null;
	}

	public void ApplyChanges()
	{
		ResourceTracker.Change(this.Changes);
	}
	#endregion
}
