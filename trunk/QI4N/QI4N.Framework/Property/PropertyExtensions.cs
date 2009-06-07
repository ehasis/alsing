namespace QI4N.Framework.Internal
{
    using System.Reflection;

    public static class PropertyExtensions
    {
        public static object GetValue(this AbstractProperty self)
        {
            MethodInfo getter = self.GetType().GetMethod("Get",BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            object result = getter.Invoke(self, new object[]
                                                    {
                                                    });

            return result;
        }

        public static void SetValue(this AbstractProperty self, object value)
        {
            MethodInfo setter = self.GetType().GetMethod("Set");
            setter.Invoke(self, new[]
                                    {
                                            value
                                    });
        }
    }
}