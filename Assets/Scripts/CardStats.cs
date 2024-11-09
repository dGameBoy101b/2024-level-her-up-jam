using UnityEngine;

[CreateAssetMenu()]
public class CardStats : ScriptableObject
{
	public string DisplayName;

	[Min(0)]
	public int TimeCost;

	public Sprite Image;

	public int FoodChange = 0;

	public int MoneyChange = 0;

	public int PassionChnage = 0;
}
