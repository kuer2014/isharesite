using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BetterSite.Common
{
    class MVC模型绑定
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "角色名称")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "电邮地址")]
       // [EmailAddress] //需更高版本的.net 
        public string Email { get; set; }
        [Required]
        // [Phone]//需更高版本的.net 
        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
      //  [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]  //需更高版本的.net 
        public string ConfirmPassword { get; set; }

        #region 知识点
        //  [Serializable]
        //        序列化是指将对象实例的状态存储到存储媒体的过程。在此过程中，先将对象的公共字段和私有字段以及类的名称（包括类所在的程序集）转换为字节流，然后再把字节流写入数据流。
        //     在随后对对象进行反序列化时，将创建出与原对象完全相同的副本 说的简单点就是  对象是暂时保存在内存中的，不能用U盘考走了，有时为了使用介质转移对象，并且把对象的状态保持下来，就需要把对象保存下来，这个过程就叫做序列化。
        //  [DataContract] 数据契约。服务契约定义了远程访问对象和可供调用的方法，数据契约则是服务端和客户端之间要传送的自定义数据类型。
        //        一旦声明一个类型为DataContract，那么该类型就可以被序列化在服务端和客户端之间传送。
        //      [DataContract] 和[DataMember] 联合使用来标记被序列化的字段
        #endregion 知识点
    }
}
