namespace UiTestApp.Factory
{
    public class Type
    {
        public Type(Database1DataSet.TypesRow typeRow)
        {
            Name = typeRow.Name;
            BonusType = typeRow.BonusType;
        }

        public Type()
        {
        }

        public string Name { get; set; }
        public string BonusType { get; set; }

        public Type Clone()
        {
            return new Type
            {
                Name = Name,
                BonusType = BonusType
            };
        }
    }
}