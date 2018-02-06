using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UiTestApp.Factory
{
    public class Variant
    {
        public Variant(Database1DataSet.VariantsRow variantRow)
        {
            Name = variantRow.Name.Trim();
            Scaler = variantRow.Scaler;
            Rpm = variantRow.RPM;
            Mag = variantRow.Mag;
            Range = variantRow.Range;
            TimeToRelaod = variantRow.TimeToRelaod;
            HeadshotMultiplier = variantRow.HeadshotMultiplier;
            Type = WeaponFactory.GetType(variantRow.Type);
        }

        public Variant()
        {
            Type = new Type();
        }

        public double HeadshotMultiplier { get; set; }

        public int TimeToRelaod { get; set; }

        public int Range { get; set; }

        public int Mag { get; set; }

        public int Rpm { get; set; }

        public double Scaler { get; set; }

        public Type Type { get; set; }

        public string Name { get; set; }

        public Variant Clone()
        {
            return new Variant
            {
                HeadshotMultiplier = HeadshotMultiplier,
                TimeToRelaod = TimeToRelaod,
                Range = Range,
                Mag = Mag,
                Rpm = Rpm,
                Scaler = Scaler,
                Type = Type.Clone(),
                Name = Name
            };
        }

    }
}