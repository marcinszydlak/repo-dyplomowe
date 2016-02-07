using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }
    public enum ContentType
    {
        [StringValue("application/pdf")]
        PDF = 1,
        [StringValue("audio/mpeg")]
        MP3 = 2,
        [StringValue("audio/x-ms-wma")]
        WMA = 3,
        [StringValue("audio/x-wav")]
        WAV = 4,
        [StringValue("image/gif")]
        GIF = 5,
        [StringValue("image/png")]
        PNG = 6,
        [StringValue("image/jpeg")]
        JPG = 7,
        [StringValue("application/msword")]
        DOC = 8,
        [StringValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        DOCX = 9,
        [StringValue("application/vnd.ms-excel")]
        XLS = 10,
        [StringValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        XLSX = 11,
        [StringValue("application/vnd.ms-powerpoint")]
        PPT = 12,
        [StringValue("application/vnd.openxmlformats-officedocument.presentationml.presentation")]
        PPTX = 13,
        [StringValue("text/plain")]
        TXT = 14,
        [StringValue("application/x-rar-compressed")]
        RAR = 15,
        [StringValue("application/zip")]
        ZIP = 16,

    }
}
