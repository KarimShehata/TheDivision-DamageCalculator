namespace UiTestApp.Factory
{
    public class Weapon
    {
        public Weapon(Database1DataSet.WeaponsRow weaponsRow)
        {
            Name = weaponsRow.Name;
            Variant = WeaponFactory.GetVariant(weaponsRow.Variant);
        }

        public Weapon(Variant variant)
        {
            Variant = variant;
        }

        public Weapon()
        {
            Variant = new Variant();
        }

        public Variant Variant { get; set; }

        public string Name { get; set; }

        public double Bonus { get; set; }

        public double Damage { get; set; }
    }
}