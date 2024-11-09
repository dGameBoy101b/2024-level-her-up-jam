using UnityEngine;
using TMPro;

public class CardResourceChange : MonoBehaviour, IUpdateCardStats
{
	public ResourceType Type;

	[SerializeField]
	private TMP_Text _text;
	public TMP_Text Text
	{
		get
		{
			if (this._text == null)
				this._text = this.GetComponentInChildren<TMP_Text>();
			return this._text;
		}
		set => this._text = value;
	}

	public Color PositiveColor = Color.green;

	public Color NegativeColor = Color.red;

	public void UpdateCardStats(CardStats stats)
	{
		int value;
		bool has_value = stats.Changes.TryGetValue(this.Type, out value);
		bool is_active = has_value && value != 0;
		this.gameObject.SetActive(is_active);
		if (!is_active)
			return;
		this.Text.text = value < 0 ? value.ToString() : $"+{value}";
		this.Text.color = value < 0 ? this.NegativeColor : this.PositiveColor;
	}
}
