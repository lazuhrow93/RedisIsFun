﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Helpers
{
    public interface IDictionaryable
    {
        Dictionary<string, object> ToDictionary();
    }
}
