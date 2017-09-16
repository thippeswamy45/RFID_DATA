
using System.Globalization;
using System.Resources;

namespace CS_VBusSample1
{
    internal class Resources
    {
        static System.Resources.ResourceManager m_rmNameValues;

        static Resources()
        {
            m_rmNameValues = new System.Resources.ResourceManager(
                "CS_VBusSample1.Resources", typeof(Resources).Assembly);
        }

        public static string GetString(string name)
        {
            return m_rmNameValues.GetString(name);
        }

    }
}