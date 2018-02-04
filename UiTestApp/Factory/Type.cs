namespace UiTestApp
{
    public class Type
    {
        public Type(Database1DataSet.TypesRow typeRow)
        {
            Name = typeRow.Name;
            BonusType = typeRow.BonusType;
        }

        public string Name { get; set; }
        public string BonusType { get; set; }
    }
}