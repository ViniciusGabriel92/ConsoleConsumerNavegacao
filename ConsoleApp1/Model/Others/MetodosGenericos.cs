using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class MetodosGenericos
    {
        public static void InsertInformationNavigation(InformationNavigation objInformationNavigation)
        {
            try
            {
                new DAONavigationInformation().Inserir(objInformationNavigation);
            }
            catch(Exception ex)
            {
                string exc = ex.Message;
                throw;
            }
        }

        public static string BuscarParametros(object obj)
        {
            string result = "";

            string[] propertyNames = obj.GetType().GetProperties().Select(p => p.Name).ToArray();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                //Recupera valor do atributo
                if (!obj.GetType().GetProperty(propertyNames[i]).PropertyType.Namespace.ToString().Equals("System.Collections.Generic"))
                {
                    object propValue = obj.GetType().GetProperty(propertyNames[i]).GetValue(obj, null);

                    //concatena o atributo com o seu valor
                    result += propertyNames[i] + ": " + propValue + ", ";
                }
            }
            return result.Substring(0, result.Length - 2);
        }
    }
}
