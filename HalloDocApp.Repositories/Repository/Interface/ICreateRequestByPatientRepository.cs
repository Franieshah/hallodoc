﻿using HalloDocApp.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocApp.Repositories.Repository.Interface
{
    public interface ICreateRequestByPatientRepository
    {
        Task PatientCreateRequest(Aspnetuser aspnetuser);
    }
}
