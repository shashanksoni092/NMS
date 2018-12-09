using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMS.Models
{
    public class JoinGroup
    {
        NMSEntities db = new NMSEntities();

        public List<NGroup> GetGroups()
        {
            
            return db.NGetAllGroups().ToList();
        }
    }
}