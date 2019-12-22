using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VehicleLoader
{
	public const string VehicleDirectory = "UserVehicles";

	public const string FileExtension = ".json";

	private static void EnsureVehicleDirectoryExistence()
	{
		if (!Directory.Exists(VehicleDirectory))
		{
			Directory.CreateDirectory(VehicleDirectory);
		}
	}

	private static string GetPath(string name)
	{
		return Path.ChangeExtension(Path.Combine(VehicleDirectory, name), FileExtension);
	}

	public static string[] GetUserVehicleNames()
	{
		if (Directory.Exists(VehicleDirectory))
		{
			string[] fullNames = Directory.GetFiles(VehicleDirectory);
			var names = new string[fullNames.Length];

			for (int i = 0; i < fullNames.Length; ++i)
			{
				names[i] = Path.GetFileNameWithoutExtension(fullNames[i]);
			}

			return names;
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
}
