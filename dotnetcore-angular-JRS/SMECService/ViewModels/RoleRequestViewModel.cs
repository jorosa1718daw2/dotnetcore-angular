using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMECService.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class RoleRequestViewModel
    {

        #region Constructor
        public RoleRequestViewModel()
        {

        }
        #endregion

        #region Properties
        public string name { get; set; }
        #endregion

    }
}
