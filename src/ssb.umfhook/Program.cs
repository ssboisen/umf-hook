using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using EnvDTE;
using EnvDTE80;

namespace ssb.umfhook
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

                var projects = solution.Projects.OfType<Project>().ToList();

                projects.Where(p => !p.Saved).ToList().ForEach(p => stringBuilder.AppendLine(p.Name));

                var unsavedModified = projects.SelectMany(p => p.ProjectItems.OfType<ProjectItem>().SelectMany(GetUnsavedModifiedProjectItems)).Select(Format).ToList();

                stringBuilder.AppendLine(string.Join(Environment.NewLine, unsavedModified));

                var content = stringBuilder.ToString();
                
                if (!string.IsNullOrWhiteSpace(content))
                    Console.WriteLine(stringBuilder);
            }

            MessageFilter.Revoke();
        }

        private static string Format(string filename)
        {
            return string.Format("'{0}' has unsaved changes", filename);
        }

        private static IEnumerable<string> GetUnsavedModifiedProjectItems(ProjectItem projectItem)
        {
            if ((projectItem.ProjectItems != null && projectItem.ProjectItems.Count == 0) && projectItem.SubProject == null)
            {
                if (!projectItem.Saved)
                    yield return projectItem.Name;
            }

            var projectItems = projectItem.ProjectItems == null ? new List<ProjectItem>() : projectItem.ProjectItems.OfType<ProjectItem>().ToList();

            if (projectItem.SubProject != null)
            {
                if (!projectItem.SubProject.Saved)
                    yield return projectItem.SubProject.Name;

                foreach (var subProjectProjectItem in projectItem.SubProject.ProjectItems.OfType<ProjectItem>().SelectMany(GetUnsavedModifiedProjectItems))
                {
                    yield return subProjectProjectItem;
                }
            }

            foreach (var pi in projectItems.Where(pi => !pi.Saved))
            {
                yield return pi.Name;
            }
        }
    }
}
