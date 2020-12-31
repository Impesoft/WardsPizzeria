using System;

namespace WardsPizzeria
{
    public static class GlobalVariables
    {
        private static string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Set required doc paths
        public static string Path = $"{documents}\\Pizzalijst.xml";

        public static string LogPath = $"{documents}\\Saleslog.xml";
    }
}