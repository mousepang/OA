﻿using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IBLL
{
    public interface IUserInfoService:IBaseServicee<UserInfo>
    {
        bool DeleteEntities(List<int>list);
    }
}
