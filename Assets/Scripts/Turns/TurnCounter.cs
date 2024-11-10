using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TurnCounter : MonoBehaviour, IStartTurn
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

	public void TurnStart(int count)
	{
		this.Text.text = count.ToString();
	}
}
