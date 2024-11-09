using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceTracker : MonoBehaviour
{
	#region Registration
	private static Dictionary<ResourceType, ResourceTracker> _instances = new();
	public static IReadOnlyDictionary<ResourceType, ResourceTracker> Instances => _instances;

	public void Register()
	{
		_instances[this.Type] = this;
	}

	public void Unregister()
	{
		_instances.Remove(this.Type);
	}

	public ResourceType Type;
	#endregion

	#region Amount
	[Min(0)]
	public int StartingAmount;

	public UnityEvent<int> OnAmountChanged = new();

	private int _amount;
	public int Amount 
	{
		get => this._amount;
		set
		{
			var old = this._amount;
			if (old == value)
				return;
			this._amount = value;
			this.OnAmountChanged.Invoke(value);
		}
	}
	#endregion

	#region Threshold
	[Min(0)]
	public int StartingThreshold;

	public UnityEvent<int> OnThresholdChange = new();

	private int _threshold;
	public int Threshold
	{
		get => this._threshold;
		set
		{
			var old = this._threshold;
			if (old == value)
				return;
			this._threshold = value;
			this.OnThresholdChange.Invoke(value);
		}
	}
	#endregion

	public void SetToStartValues()
	{
		this.Amount = this.StartingAmount;
		this.Threshold = this.StartingThreshold;
	}

	#region Spending
	public static bool CanAfford(IReadOnlyDictionary<ResourceType, int> costs)
	{
		foreach (var item in costs)
		{
			if (Instances[item.Key].Amount < item.Value)
				return false;
		}
		return true;
	}

	public static void Change(IReadOnlyDictionary<ResourceType, int> changes)
	{
		foreach (var item in changes)
		{
			Instances[item.Key].Amount += item.Value;
		}
	}
	#endregion

	#region Unity Messages
	private void Start()
	{
		this.SetToStartValues();
	}

	private void OnEnable()
	{
		this.Register();
	}

	private void OnDisable()
	{
		this.Unregister();
	}
	#endregion
}
