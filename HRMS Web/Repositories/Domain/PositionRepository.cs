﻿using HRMS_Web.DAO;
using HRMS_Web.Models.DataModels;
using HRMS_Web.Repositories.Common;

namespace HRMS_Web.Repositories.Domain
{
    public class PositionRepository : BaseRepository<PositionEntity>, IPositionRepository
    {
        private readonly HRMSWebDBContext _dBContext;

        public PositionRepository(HRMSWebDBContext dBContext) : base(dBContext)
        {
            this._dBContext = dBContext;
        }
    }
}
