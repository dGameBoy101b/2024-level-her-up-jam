using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(TMP_Text))]
public class ResourceCounter : MonoBehaviour
{
	public ResourceType Type;

	public ResourceTracker Tracker => ResourceTracker.Instances[this.Type];

	private TMP_Text _text;
	public TMP_Text Text
	{
		get
		{
			if (this._text == null)
				this._text = this.GetComponent<TMP_Text>();
			return this._text;
		}
		set => this._text = value;
	}

	public string Separator = " / ";

	public void UpdateCounts()
	{
		this.Text.text = $"{this.Tracker.Amount}{this.Separator}{this.Tracker.Threshold}";
		Debug.Log($"Updated counts: {this.Text.text}", this);
	}

	private void OnAmountChanged(int amount)
	{
		this.UpdateCounts();
	}

	private void OnThresholdChanged(int threshold)
	{
		this.UpdateCounts();
	}

	public void AddListeners()
	{
		this.Tracker.OnAmountChanged.AddListener(this.OnAmountChanged);
		this.Tracker.OnThresholdChange.AddListener(this.OnThresholdChanged);
	}

	public void RemoveListeners()
	{
		this.Tracker.OnAmountChanged.RemoveListener(this.OnAmountChanged);
		this.Tracker.OnThresholdChange.RemoveListener(this.OnThresholdChanged);
	}

	private void OnEnable()
	{
		this.AddListeners();
	}

	private void OnDisable()
	{
		this.RemoveListeners();
	}
}
