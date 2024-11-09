using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CardName : MonoBehaviour
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

	public CardStats Stats;

	public void UpdateText()
	{
		this.Text.text = this.Stats.DisplayName;
	}

	private void Start()
	{
		this.UpdateText();
	}
}
