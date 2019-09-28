using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class data_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/data.xls";
	private static readonly string exportPath = "Assets/Resources/data.asset";
	private static readonly string[] sheetNames = { "Sheet1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_Sheet1 data = (Entity_Sheet1)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_Sheet1));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_Sheet1> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_Sheet1.Sheet s = new Entity_Sheet1.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_Sheet1.Param p = new Entity_Sheet1.Param ();
						
					cell = row.GetCell(0); p.time = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.type = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.x = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.y = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.vx = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.vy = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.note = (int)(cell == null ? 0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
