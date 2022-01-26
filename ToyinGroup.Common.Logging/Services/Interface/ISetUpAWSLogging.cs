﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyinGroup.Common.Logging.Services.Interface
{
    public interface ISetUpAWSLogging
    {
        Task SetUpAWSLogging(WebHostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration);
    }
}
