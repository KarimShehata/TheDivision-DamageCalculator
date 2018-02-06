using System.Linq;

namespace UiTestApp.Factory
{
    public class WeaponFactory
    {
        internal static Variant GetVariant(string variantName)
        {
            var variantTableAdapter = new Database1DataSetTableAdapters.VariantsTableAdapter();
            var variants = variantTableAdapter.GetData();

            var variantRow = variants.FirstOrDefault(row => row.Name == variantName);

            return new Variant(variantRow);
        }

        internal static Type GetType(string typeName)
        {
            var typeTableAdapter = new Database1DataSetTableAdapters.TypesTableAdapter();
            var types = typeTableAdapter.GetData();

            var typeRow = types.FirstOrDefault(row => row.Name == typeName);

            return new Type(typeRow);
        }
    }
}