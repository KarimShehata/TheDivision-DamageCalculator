namespace UiTestApp
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

        public double HeadshotMultiplier { get; set; }

        public int TimeToRelaod { get; set; }

        public int Range { get; set; }

        public int Mag { get; set; }

        public int Rpm { get; set; }

        public double Scaler { get; set; }

        public Type Type { get; set; }

        public string Name { get; set; }
    }
}