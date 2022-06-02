using ClassLibrary1;
using System;
using Unity;
using Unity.Interception;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;

namespace UnityAOPSample
{
    /*
     * .NET6 では動かない
     * .Net Standard 2.1 2.0にしても結果は同じ
     * container.Resolveを呼び出すメインAssemblyのプラットフォームが.NET5以下
     * の必要あり
     */
    class Program
    {
        static void Main(string[] args)
        {
            // コンテナを生成
            IUnityContainer container = new UnityContainer();

            // Interceptorを有効化
            container.AddNewExtension<Interception>();

            // コンテナに登録する際に、オプションを指定
            container
              .RegisterType<ICalculator, Calculator>()
              .Configure<Interception>()
              .SetInterceptorFor<ICalculator>(new InterfaceInterceptor());

            // ここからテスト
            // コンテナから取り出す
            ICalculator calc = container.Resolve<ICalculator>();
            
            // メソッドの呼び出し
            calc.Add(1, 2);
        }
    }

    public interface ICalculator
    {
        [Logger]
        int Add(int first, int second);
    }

    public class Calculator : ICalculator
    {

        public int Add(int first, int second)
        {
            Console.WriteLine("Add メソッド");
            return first + second;
        }
    }
}