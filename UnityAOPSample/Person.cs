using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UnityAOPSample
{
    internal class Person : IDataErrorInfo
    {
        public string? Error { get; private set; }

        // Interceptorで実装してもらう
        public virtual string? this[string? columnName]
        {
            get { throw new NotImplementedException(); }
        }

        [Required(ErrorMessage = "名前を入力してください")]
        public string? Name { get; set; }

        public string Greet(string name)
        {
            return string.Format("Hello, {0}!", name);
        }
    }
}
