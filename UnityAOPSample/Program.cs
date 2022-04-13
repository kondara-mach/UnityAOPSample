using ClassLibrary1;
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

    //internal class ExceptionLoggerCallHandler : ICallHandler
    //{
    //    public IMethodReturn Invoke(
    //      IMethodInvocation input, GetNextHandlerDelegate getNext)
    //    {
    //        IMethodReturn result = getNext()(input, getNext);
    //        if (result.Exception != null)
    //        {
    //            Console.WriteLine("ExceptionLoggerCallHandler:");
    //            Console.WriteLine("\tParameters:");
    //            for (int i = 0; i < input.Arguments.Count; i++)
    //            {
    //                var parameter = input.Arguments[i];
    //                Console.WriteLine(
    //                  string.Format("\t\tParam{0} -> {1}", i, parameter.ToString()));
    //            }
    //            Console.WriteLine();
    //            Console.WriteLine("Exception occured: ");
    //            Console.WriteLine(
    //              string.Format("\tException -> {0}", result.Exception.Message));

    //            Console.WriteLine();
    //            Console.WriteLine("StackTrace:");
    //            Console.WriteLine(Environment.StackTrace);
    //        }

    //        return result;
    //    }

    //    public int Order { get; set; }
    //}

    //internal class ExceptionLoggerAttribute : HandlerAttribute
    //{
    //    public override ICallHandler CreateHandler(IUnityContainer container)
    //    {
    //        return new ExceptionLoggerCallHandler();
    //    }
    //}

    //internal class LoggerAttribute : HandlerAttribute
    //{
    //    public override ICallHandler CreateHandler(IUnityContainer container)
    //    {
    //        return new ExceptionLoggerCallHandler();
    //    }
    //}

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
            return first + second;
        }

        public int Multiply(int first, int second)
        {
            return second * first;
        }
    }


    //var container = new UnityContainer();
    //// Interceptorを有効化
    //container.AddNewExtension<Interception>();

    //// Personクラスは仮想メソッドをインターセプトして、DataErrorInfoImplementationBehaviorでインターセプトするぜ
    //container.RegisterType<Person>(
    //    new Interceptor<VirtualMethodInterceptor>(),
    //    new InterceptionBehavior<DataErrorInfoImplementationBehavior>());

    //// コンテナからインスタンスを取得して動くか試してみる
    //var p = container.Resolve<Person>();
    //Console.WriteLine("ErrorMessage: {0}", p.Name);
    //p.Name = "aaa";
    //Console.WriteLine("ErrorMessage: {0}", p.Name);

    //Console.WriteLine(p.Greet("Taro"));



    //public class DataErrorInfoImplementationBehavior : IInterceptionBehavior
    //{
    //    public IEnumerable<Type> GetRequiredInterfaces()
    //    {
    //        yield return typeof(IDataErrorInfo);
    //    }

    //    public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
    //    {
    //        Console.WriteLine("開始: " + input.MethodBase.Name);

    //        var result = getNext()(input, getNext);

    //        Console.WriteLine($"終了:{input.MethodBase.Name}");

    //        return result;
    //    }

    //    public bool WillExecute => true;
    //}
}