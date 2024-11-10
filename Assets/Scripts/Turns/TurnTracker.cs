using System.Collections.Generic;
using UnityEngine;

public class TurnTracker : MonoBehaviour
{
	#region Instance
	public static TurnTracker Instance { get; private set; }

	public void RegisterInstance()
	{
		if (Instance != null)
			Debug.LogWarning($"Overwritting turn tracker instance: {Instance.name}", this);
		Instance = this;
	}

	public void UnregisterInstance()
	{
		if (Instance != this)
			return;
		Instance = null;
	}
	#endregion

	#region Listeners
	private IEnumerable<T> FindComponents<T>()
	{
		foreach (var root in this.gameObject.scene.GetRootGameObjects())
			foreach (var component in root.gameObject.GetComponentsInChildren<T>())
				yield return component;
	}

	private IEnumerable<IStartTurn> start_turn_listeners = null;
	public IEnumerable<IStartTurn> StartTurnListeners
	{
		get
		{
			if (this.start_turn_listeners == null) 
				this.start_turn_listeners = this.FindComponents<IStartTurn>();
			return this.start_turn_listeners;
		}
		set => this.start_turn_listeners = value;
	}

	private IEnumerable<IEndTurn> end_turn_listeners = null;
	public IEnumerable<IEndTurn> EndTurnListeners
	{
		get
		{
			if (this.end_turn_listeners == null)
				this.end_turn_listeners = this.FindComponents<IEndTurn>();
			return this.end_turn_listeners;
		}
		set => this.end_turn_listeners = value;
	}
	#endregion

	#region Count
	private int _count = 0;
	public int Count 
	{
		get => this._count; 
		set
		{
			foreach (var end in this.EndTurnListeners)
				end.TurnEnd(this.Count);
			this._count = value;
			foreach (var start in this.StartTurnListeners)
				start.TurnStart(this.Count);
		}
	}

	public void AdvanceCount()
	{
		this.Count++;
	}

	public void ResetCount()
	{
		this.Count = 0;
	}
	#endregion

	#region Unity Messages
	private void OnEnable()
	{
		this.RegisterInstance();
	}

	private void OnDisable()
	{
		this.UnregisterInstance();
	}
	#endregion
}
