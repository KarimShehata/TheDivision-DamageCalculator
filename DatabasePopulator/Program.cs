using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DatabasePopulator
{
    class Program
    {
        public static void Main(string[] args)
        {
            var csvData = new List<string[]>();

            using (var reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Base Damage Calculator - Weapon Damage Ranges.csv")))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    csvData.Add(values);
                }
            }

            var filteredvariants = new List<string[]>();

            foreach (var item in csvData)
            {
                //FillWeaponsTable(item);

                var found = false;

                foreach (var variant in filteredvariants)
                {
                    if (variant[1] != item[1]) continue;

                    found = true;
                    break;
                }

                if (found) continue;

                filteredvariants.Add(item);
            }

            //FillVariantsTable(filteredvariants);
        }

        private static void FillWeaponsTable(string[] item)
        {
            var dataSet = new Database1DataSet();

            var weaponRow = dataSet.Weapons.NewWeaponsRow();

            weaponRow.Name = item[2];
            weaponRow.Variant = item[1];

            dataSet.Weapons.Rows.Add(weaponRow);

            var WeaponsTableAdapter = new Database1DataSetTableAdapters.WeaponsTableAdapter();
            WeaponsTableAdapter.Update(dataSet.Weapons);
        }

        private static void FillVariantsTable(List<string[]> filteredvariants)
        {
            var dataSet = new Database1DataSet();
            foreach (var variant in filteredvariants)
            {
                var variantRow = dataSet.Variants.NewVariantsRow();

                variantRow.Type = variant[0];
                variantRow.Name = variant[1];
                variantRow.Scaler = double.Parse(variant[4].Replace(".", ","));
                variantRow.RPM = int.Parse(variant[5]);
                variantRow.Mag = int.Parse(variant[6]);
                variantRow.Range = int.Parse(variant[7]);
                variantRow.TimeToRelaod = int.Parse(variant[8]);
                variantRow.HeadshotMultiplier = double.Parse(variant[9].Replace(".",","));
                variantRow.BonusType = variant[10];

                dataSet.Variants.Rows.Add(variantRow);
            }

            var variantsTableAdapter = new Database1DataSetTableAdapters.VariantsTableAdapter();
            variantsTableAdapter.Update(dataSet.Variants);
        }
    }
}