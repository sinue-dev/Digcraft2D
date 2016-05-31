using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block {

	public string DisplayName;
	public byte id;
	public Sprite sprite;
	public bool isSolid = true;
    public bool isDestroyable = true;
	public Drop[] drops;

	public Block() { }

	public Block(Block block)
	{
		this.DisplayName = block.DisplayName;
		this.id = block.id;
		this.sprite = block.sprite;
		this.isSolid = block.isSolid;
        this.isDestroyable = block.isDestroyable;
		this.drops = block.drops;
	}

	public cBlock Serializable()
	{
		return new cBlock(DisplayName, id, isSolid, drops);
	}
}

[System.Serializable]
public class cBlock
{
	public string DisplayName;
	public byte id;
	public bool isSolid = true;
	public Drop[] drops;

	public cBlock(string DisplayName, byte id, bool isSolid, Drop[] drops)
	{
		this.DisplayName = DisplayName;
		this.id = id;
		this.isSolid = isSolid;
		this.drops = drops;
	}
}