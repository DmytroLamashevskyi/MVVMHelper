using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CustomProject.Utils
{
    public static class SimpleCompiler
    {
        private static List<Process> processes = new List<Process>();


        public static void CompileCode(string source)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider(); 
            string Output = "Out.exe"; 
            

            CompilerParameters parameters = new CompilerParameters();

            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, source);

            if (results.Errors.Count > 0)
            {

                foreach (CompilerError CompErr in results.Errors)
                {
                    LogProvider.Instance().AddError(
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" );
                }
            }
            else
            {
                //Successful Compile

                LogProvider.Instance().AddLog("Success!");
                //If we clicked run then launch our EXE
                processes.Add(Process.Start(Output)); 
            }
        }

        public static void CloseAllProcesses()
        {
            foreach(Process process in processes)
            {
                process.Kill();
            }
        }

    }
}
