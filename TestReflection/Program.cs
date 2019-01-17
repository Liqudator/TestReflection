using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======= Late Binding ========");
            // попробовать загрузить локальную копию ListOfNumber
            Assembly asm = null;
            try
            {
                asm = Assembly.Load("ListOfNumber");
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            if (asm != null)
                CreateUsingLateBinding(asm);
            Console.ReadLine();
        }

        static void CreateUsingLateBinding(Assembly asm)
        {
            try
            {
                // получить метаданные для типа Methods
                Type methods = asm.GetType("ListOfNumber.Methods");
                // создать объект methods на лету
                var obj = Activator.CreateInstance(methods);
                Console.WriteLine($"Created object {obj}");

                MethodInfo[] mii = methods.GetMethods();
                foreach(var i in mii)
                {
                    Console.WriteLine($"->{i.Name}");
                }

                MethodInfo mi = methods.GetMethod("Add");

                mi.Invoke(obj, null);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
