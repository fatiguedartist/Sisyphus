namespace RunningDimensions.Extensions
{
    public static class Path
    {
        public static string Combine(string root, params string[] paths)
        {
            var path = root;

            for (var i = 0; i < paths.Length; i++)
            {
                path = System.IO.Path.Combine(path, paths[i]);
            }

            return path;
        }
    }
}
