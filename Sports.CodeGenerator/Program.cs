using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sports.CodeGenerator
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var property = args[0];
            var privateProperty = $"_{property.Substring(0, 1).ToLower()}{property.Substring(1, property.Length - 1)}";
            var str = string.Format(" private bool {0};" +
            "public bool {1}" +
            "[" +
                "get => {0};" +
                "set => SetProperty(ref {0}, value, \"{1}\");" +
            "]", privateProperty, property).Replace("[","{").Replace("]","}");
            Console.WriteLine(str);
            Clipboard.SetText(str);
        }
    }
}
