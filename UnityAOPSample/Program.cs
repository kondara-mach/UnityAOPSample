using ClassLibrary1;
using System;
using Unity;
using Unity.Interception;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;

namespace UnityAOPSample
{
    /*
     * .NET6 では動かない
     * ICallHandlerの実装部分を別Assemblyで.Net Standard 2.1 2.0にしても
     * 結果は同じ
     * container.Resolveを呼び出すメインAssemblyのプラットフォームが.NET5以下
     * の必要あり
     */
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container
              .RegisterType<ICalculator, Calculator>()
              .Configure<Interception>()
              .SetInterceptorFor<ICalculator>(new InterfaceInterceptor());

            // Resolve
            ICalculator calc = container.Resolve<ICalculator>();

            // Call method
            calc.Add(1, 2);
        }
    }

    public interface ICalculator
    {
        [Logger]
        int Add(int first, int second);

        [ExceptionLogger]
        int Multiply(int first, int second);
    }

    public class Calculator : ICalculator
    {

        public int Add(int first, int second)
        {
            Console.WriteLine("Add メソッド");
            return first + second;
        }

        public int Multiply(int first, int second)
        {
            return second * first;
        }
    }
}