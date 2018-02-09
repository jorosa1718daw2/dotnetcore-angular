using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UserCreateViewModel
    {
        #region Constructor
        public UserCreateViewModel()
        {
        }
        #endregion

        #region Properties
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        #endregion
    }
}
