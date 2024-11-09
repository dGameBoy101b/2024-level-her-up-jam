using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardImage : MonoBehaviour, IUpdateCardStats
{
	private Image _image;
	public Image Image
	{
		get
		{
			if (this._image == null)
				this._image = this.GetComponent<Image>();
			return this._image;
		}
		set => this._image = value;
	}

	public void UpdateCardStats(CardStats stats)
	{
		this.Image.sprite = stats.Image;
	}
}
