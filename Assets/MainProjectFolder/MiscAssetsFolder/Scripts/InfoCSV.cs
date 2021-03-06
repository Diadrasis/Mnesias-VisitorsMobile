// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class InfoCSV : MonoBehaviour
{
	List<Row> rowList = new List<Row>();
	public bool isLoaded = false;
	public bool IsLoaded()
	{
		return isLoaded;
	}

	public List<Row> GetRowList()
	{
		return rowList;
	}

	/*public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);//all line elements
		//TextAsset child = TextAsset.Instantiate(Resources.Load(Application.persistentDataPath+"/"+csv.name) as TextAsset);
		for (int i = 1; i < grid.Length; i++)
		{
			Row row = new Row();
			row.mouthpiece = grid[i][0];
			row.boreLength = grid[i][1];
			row.boreDiameter = grid[i][2];
			row.boreWeight = grid[i][3];
			row.numberOfHoles = grid[i][4];
			row.distanceHole = grid[i][5];
			row.diameterHole = grid[i][6];
			row.hole = grid[i][7];
			row.folder = grid[i][8];
			row.soundPiece = grid[i][9];

			rowList.Add(row);
			
		isLoaded = true;
	}*/

	public int NumRows()
	{
		return rowList.Count;
	}

	public Row GetAt(int i)
	{
		if (rowList.Count <= i)
			return null;
		return rowList[i];
	}

	public Row Find_mouthpiece(string find)
	{
		return rowList.Find(x => x.mouthpiece == find);
	}
	public List<Row> FindAll_mouthpiece(string find)
	{
		return rowList.FindAll(x => x.mouthpiece == find);
	}
	public Row Find_boreLength(string find)
	{
		return rowList.Find(x => x.boreLength == find);
	}
	public List<Row> FindAll_boreLength(string find)
	{
		return rowList.FindAll(x => x.boreLength == find);
	}
	public Row Find_boreDiameter(string find)
	{
		return rowList.Find(x => x.boreDiameter == find);
	}
	public List<Row> FindAll_boreDiameter(string find)
	{
		return rowList.FindAll(x => x.boreDiameter == find);
	}
	public Row Find_boreWeight(string find)
	{
		return rowList.Find(x => x.boreWeight == find);
	}
	public List<Row> FindAll_boreWeight(string find)
	{
		return rowList.FindAll(x => x.boreWeight == find);
	}
	public Row Find_numberOfHoles(string find)
	{
		return rowList.Find(x => x.numberOfHoles == find);
	}
	public List<Row> FindAll_numberOfHoles(string find)
	{
		return rowList.FindAll(x => x.numberOfHoles == find);
	}
	public Row Find_distanceHole(string find)
	{
		return rowList.Find(x => x.distanceHole == find);
	}
	public List<Row> FindAll_distanceHole(string find)
	{
		return rowList.FindAll(x => x.distanceHole == find);
	}
	public Row Find_diameterHole(string find)
	{
		return rowList.Find(x => x.diameterHole == find);
	}
	public List<Row> FindAll_diameterHole(string find)
	{
		return rowList.FindAll(x => x.diameterHole == find);
	}
	public Row Find_hole(string find)
	{
		return rowList.Find(x => x.hole == find);
	}
	public List<Row> FindAll_hole(string find)
	{
		return rowList.FindAll(x => x.hole == find);
	}
	public Row Find_folder(string find)
	{
		return rowList.Find(x => x.folder == find);
	}
	public List<Row> FindAll_folder(string find)
	{
		return rowList.FindAll(x => x.folder == find);
	}
	public Row Find_soundPiece(string find)
	{
		return rowList.Find(x => x.soundPiece == find);
	}
	public List<Row> FindAll_soundPiece(string find)
	{
		return rowList.FindAll(x => x.soundPiece == find);
	}

}
[SerializeField]
public class Row
{
	public string mouthpiece;
	public string boreLength;
	public string boreDiameter;
	public string boreWeight;
	public string numberOfHoles;
	public string distanceHole;
	public string diameterHole;
	public string hole;
	public string folder;
	public string soundPiece;

}