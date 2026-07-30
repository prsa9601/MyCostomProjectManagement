using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace MyCostomProjectManagement.Shared.Extensions
{
    public static class GetRoutesExtentions
    {
        public static List<string> GetAllRoutes()
        {
            var list = new List<string>();
            var assembly = typeof(Program).Assembly; // اسمبلی اصلی

            foreach (var type in assembly.GetTypes())
            {
                var attr = type.GetCustomAttribute<RouteAttribute>();
                if (attr != null)
                {
                    list.Add(attr.Template);
                }
            }
            return list.Distinct().ToList();
        }
    }
}
