//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JCJ.OA.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class PhotoInfo
    {
        public int ID { get; set; }
        public string KeyWords { get; set; }
        public string Title { get; set; }
        public string PicUrls { get; set; }
        public string PictureContent { get; set; }
        public string Author { get; set; }
        public string Orgin { get; set; }
        public System.DateTime AddDate { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public short Recommend { get; set; }
        public short Rolls { get; set; }
        public short IsTop { get; set; }
        public int ReadPoint { get; set; }
        public short DelFlag { get; set; }
        public int PhotoClassID { get; set; }
        public int ShowStyle { get; set; }
    
        [JsonIgnore]
        public virtual PhotoClass PhotoClass { get; set; }
    }
}