using System;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace ClassLibrary1
{
    internal class ExceptionLoggerCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(
          IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // メソッド呼び出し前のログを出力
            Console.WriteLine("Before {0}", input.MethodBase.Name);

            // インターセプトしたメソッドを呼び出す
            IMethodReturn result = getNext()(input, getNext);

            // メソッド呼び出し後のログを出力
            Console.WriteLine("After {0}", input.MethodBase.Name);

            return result;
        }

        public int Order { get; set; }
    }

    public class ExceptionLoggerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ExceptionLoggerCallHandler();
        }
    }

    public class LoggerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ExceptionLoggerCallHandler();
        }
    }
}
