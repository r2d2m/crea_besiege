using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleIO
{
	public const string VehicleDirectory = "UserVehicles";

	public const string FileExtension = ".save";

	private static void EnsureVehicleDirectoryExistence()
	{
		if (!Directory.Exists(FullVehiclePath))
		{
			Directory.CreateDirectory(FullVehiclePath);
		}
	}

	public static string GetPath(string name)
	{
		return Path.ChangeExtension(Path.Combine(FullVehiclePath, name), FileExtension);
	}

	public static bool Exists(string name)
	{
		return File.Exists(GetPath(name));
	}

	public static string[] GetUserVehicleNames()
	{
		if (Directory.Exists(FullVehiclePath))
		{
			string[] fullNames = Directory.GetFiles(FullVehiclePath);
			List<string> names = new List<string>();

			for (int i = 0; i < fullNames.Length; ++i)
			{
				if (Path.GetExtension(fullNames[i]) == FileExtension)
				{
					names.Add(Path.GetFileNameWithoutExtension(fullNames[i]));
				}
			}

			return names.ToArray();
		}

		return new string[0];
	}

	public static string LoadJson(string name)
	{
		var reader = new StreamReader(GetPath(name));

		return reader.ReadToEnd();
	}

	public static Vehicle Load(string name)
	{
		string json = LoadJson(name);

		return Vehicle.CreateFromJson(json);
	}

	public static void Save(string vehicleJson, string name)
	{
		EnsureVehicleDirectoryExistence();

		var writer = new StreamWriter(GetPath(name));
		writer.Write(vehicleJson);
		writer.Close();
	}

	public static void Save(Vehicle vehicle, string name)
	{
		Save(vehicle.ToJson(), name);
	}

	public static void Remove(string name)
	{
		string path = GetPath(name);

		if (File.Exists(path))
		{
			File.Delete(path);
		}
	}

	public static string FullVehiclePath
	{
		get => Path.Combine(Application.streamingAssetsPath, VehicleDirectory);
	}
}
