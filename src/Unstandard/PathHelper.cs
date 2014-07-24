namespace Microline {
   public static class PathHelper {
      public static string GetAssemblyDirectory() {
         string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
         System.UriBuilder uri = new System.UriBuilder(codeBase);
         string path = System.Uri.UnescapeDataString(uri.Path);
         return System.IO.Path.GetDirectoryName(path);
      }
      
      public static string GetAbsolutePath(string path) {
         if (!System.IO.Path.IsPathRooted(path)) {
            string assemblyPath = System.IO.Path.Combine(Microline.PathHelper.GetAssemblyDirectory(), path);
            return System.IO.Path.GetFullPath(assemblyPath);
         } else {
            return path;
         }
      }      
   }
}