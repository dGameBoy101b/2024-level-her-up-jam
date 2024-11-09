using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CardStackCounter : MonoBehaviour, IUpdateCardCount
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
	}

	public void UpdateCardCount(int count)
	{
		this.Text.text = count.ToString();
	}
}
