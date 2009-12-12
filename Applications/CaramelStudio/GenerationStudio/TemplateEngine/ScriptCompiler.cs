using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using GenerationStudio.TemplateEngine;
using Microsoft.CSharp;

namespace My.Scripting
{
    public class ScriptCompiler
    {
        public static string ScriptLanguageName_CSharp = "CSharp";
        public static string ScriptLanguageName_VisualBasic = "VisualBasic";

        #region Properties

        public static object ThreadRoot
        {
            get { return threadRoot; }
        }

        public Exception Error
        {
            get { return error; }
        }

        #endregion

        #region Errors and error handling

        private static void Error_ExpectedScriptLanguageStatement()
        {
            throw new UnknownScriptLanguageException("Expected language definition at beginning of" +
                                                     " script (syntax: " + getLanguageStatementSyntax() + ")");
        }

        private static void Error_LanguageStatementSyntaxError()
        {
            throw new UnknownScriptLanguageException("Cannot interpret the language statement " +
                                                     getLanguageStatementSyntax() + ")");
        }

        private static void Error_CompilationFailed(string message)
        {
            throw new ScriptCompilerException("Compilation failed:\n" + message);
        }

        private static void Error_IScriptNotImplemented()
        {
            throw new ScriptNotImplementedException("Interface " + typeof (ITemplate).FullName + " must be implemented!");
        }

        private static string getLanguageStatementSyntax()
        {
            return "#language = {" + ScriptLanguageName_CSharp + " | " + ScriptLanguageName_VisualBasic + "}";
        }

        private void addError(Exception value)
        {
            error = value;
        }

        #endregion

        public ITemplate Compile(string script)
        {
            try
            {
                var codeDomProvider =
                    new CSharpCodeProvider(new Dictionary<string, string> {{"CompilerVersion", "v3.5"}});

                var cParams = new CompilerParameters
                              {
                                  GenerateExecutable = false,
                                  GenerateInMemory = true,
                                  OutputAssembly = getTemporaryOutputAssemblyName(),
                                  MainClass = "**not used**",
                                  IncludeDebugInformation = false
                              };


                // allow all referenced assemblies to be used by the script...
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                    cParams.ReferencedAssemblies.Add(asm.Location);


                CompilerResults results = codeDomProvider.CompileAssemblyFromSource(cParams, script);

                if (results.Errors.Count > 0)
                {
                    var errors = new StringBuilder();
                    foreach (CompilerError err in results.Errors)
                    {
                        errors.Append(err + "\n");
                    }
                    Error_CompilationFailed(errors.ToString());
                    return null; // keeps compiler happy :o|
                }

                // remove temporary files...
                if (File.Exists(cParams.OutputAssembly))
                    File.Delete(cParams.OutputAssembly);
                // get the first class that implements the IScript interface...
                ITemplate scriptObject = getScriptObject(results.CompiledAssembly);
                if (scriptObject == null)
                    Error_IScriptNotImplemented();
                return scriptObject;
            }
            catch (ScriptCompilerException e)
            {
                addError(e);
                return null;
            }
        }

        #region Internal stuff

        private static readonly object threadRoot = new object();
        private static long tmpOutputAssemblyID;
        private Exception error;

        /// <summary>
        /// Creates a thread safe temporary assembly name. 
        /// The method is primarily intended for on-the-run, in-memory, compilation.
        /// </summary>
        /// <param name="language">The script language</param>
        /// <returns>A temporary assembly name.</returns>
        private static string getTemporaryOutputAssemblyName()
        {
            long result;
            lock (ThreadRoot)
            {
                result = ++tmpOutputAssemblyID;
            }
            return "temp_asm" + result;
        }

        /// <summary>
        /// Returns the first class in the assembly that implements the IScript interface. 
        /// </summary>
        /// <param name="asm">
        /// The assembly that's expected to contain the IScript implementor.
        /// </param>
        /// <returns>An instance of the first class found to implement the IScript interface. 
        /// If no such class exists a null value is returned instead</returns>
        private ITemplate getScriptObject(Assembly asm)
        {
            Type[] types = asm.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsClass && type.GetInterface("ITemplate") != null)
                    return (ITemplate) Activator.CreateInstance(type);
            }
            return null;
        }

        #endregion
    }

    public class ScriptCompilerException : Exception
    {
        public ScriptCompilerException(string message) : base(message) {}
    }

    public class UnknownScriptLanguageException : ScriptCompilerException
    {
        public UnknownScriptLanguageException(string message) : base(message) {}
    }

    public class ScriptNotImplementedException : ScriptCompilerException
    {
        public ScriptNotImplementedException(string message) : base(message) {}
    }
}