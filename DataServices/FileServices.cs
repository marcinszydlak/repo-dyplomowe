using Database;
using DataServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Reflection;

namespace DataServices
{
    public class FileServices
    {
        public string GetContentType(string Extension)
        {
            switch(Extension.ToUpper())
            {
                case "PDF": return GetStringValue(ContentType.PDF);
                case "MP3": return GetStringValue(ContentType.MP3);
                case "WMA": return GetStringValue(ContentType.WMA);
                case "WAV": return GetStringValue(ContentType.WAV);
                case "GIF": return GetStringValue(ContentType.GIF);
                case "PNG": return GetStringValue(ContentType.PNG);
                case "JPG": return GetStringValue(ContentType.JPG);
                case "DOC": return GetStringValue(ContentType.DOC);
                case "DOCX": return GetStringValue(ContentType.DOCX);
                case "XLS": return GetStringValue(ContentType.XLS);
                case "XLSX": return GetStringValue(ContentType.XLSX);
                case "PPT": return GetStringValue(ContentType.PPT);
                case "PPTX": return GetStringValue(ContentType.PPTX);
                case "TXT": return GetStringValue(ContentType.TXT); 
                case "C": return GetStringValue(ContentType.TXT);
                case "CPP": return GetStringValue(ContentType.TXT);
                case "CS": return GetStringValue(ContentType.TXT);
                case "JAVA": return GetStringValue(ContentType.TXT);
                case "RAR": return GetStringValue(ContentType.RAR);
                case "ZIP" :return GetStringValue(ContentType.ZIP);
                default : throw new Exception("Aplikacja nie obsługuje rozszerzenia : "+ Extension);
            }
        }
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}
