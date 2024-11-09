using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CardName : MonoBehaviour, IUpdateCardStats
{
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

	public void UpdateCardStats(CardStats stats)
	{
		this.Text.text = stats.DisplayName;
	}
}
