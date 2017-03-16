using System;
using System.Activities;
using System.Collections.Generic;
using ClassLibrary1;
using System.Threading;

namespace ConsoleApplication51
{
    class Program
    {
        static void Main(string[] args)
        {
            Field f = new Field();
            IDictionary<string, object> input = new Dictionary<string, object>();
            input.Add("field", f);

            Activity logic = new Logic();
            //IDictionary<string, object> result = WorkflowInvoker.Invoke(logic, input);

            AutoResetEvent syncEvent = new AutoResetEvent(false);

            WorkflowApplication w = new WorkflowApplication(logic, input);
            w.Completed = (e) =>
            {
                syncEvent.Set();
            };

            w.Run();
            syncEvent.WaitOne();

            Console.ReadLine();
        }

    }
}
