using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using EnvDTE;
using EnvDTE80;

namespace FindUnsavedModifiedFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                return;

            MessageFilter.Register();
            
            var solutionName = args[0];

            List<DTE2> dte2s = DTEUtil.GetDTEs();
            
            foreach (var dte2 in dte2s.Where(dte => dte.Solution.FullName.Contains(solutionName)))
            {
                var solution = dte2.Solution;
                var stringBuilder = new StringBuilder();

                if (!solution.Saved)
                    stringBuilder.AppendLine(Format(solution.FullName));

                solution.Projects.OfType<Project>()
                    .Where(p => !p.Saved)
                    .ToList().ForEach(p => stringBuilder.AppendLine(Format(p.FullName)));

                solution.Projects.OfType<Project>()
                    .SelectMany(p => p.ProjectItems.OfType<ProjectItem>())
                    .Where(pi => !pi.Saved)
                    .ToList().ForEach(pi => stringBuilder.AppendLine(Format(pi.Name)));

                var contents = stringBuilder.ToString();

                Console.WriteLine(contents);

            }

            MessageFilter.Revoke();
        }

        private static string Format(string filename)
        {
            return string.Format("'{0}' has unsaved changes", filename);
        }
    }
}
