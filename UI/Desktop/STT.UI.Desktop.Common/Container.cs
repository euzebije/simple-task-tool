using Microsoft.Practices.Unity;

namespace STT.UI.Desktop.Common
{
    public static class Container
    {
        private static readonly IUnityContainer UnityContainer = new UnityContainer();

        public static void RegisterType<T>()
        {
            UnityContainer.RegisterType<T>();
        }

        public static void RegisterType<TFrom, TTo>() where TTo: TFrom
        {
            UnityContainer.RegisterType<TFrom, TTo>();
        }

        public static T Resolve<T>()
        {
            return UnityContainer.Resolve<T>();
        }
    }
}