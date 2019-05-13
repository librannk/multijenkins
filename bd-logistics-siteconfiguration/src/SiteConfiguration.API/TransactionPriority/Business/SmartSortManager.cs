using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.Core.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.Models;

namespace SiteConfiguration.API.TransactionPriority.Business
{
    public class SmartSortManager: ISmartSortManager
    {
        private readonly ISmartSortRepository _smartSortRepository;
        private readonly ILogger<SmartSortManager> _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public SmartSortManager(ISmartSortRepository smartSortRepository, IExecutionContextAccessor executionContextAccessor
            , IUnitOfWork unitOfWork, ILogger<TransactionPriorityManager> logger)
        {
            _smartSortRepository = smartSortRepository;
            _executionContextAccessor = executionContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RequestResponseModel.SmartSortResponse>> GetAllSmartSort(bool isActive=true)
        {
            List<RequestResponseModel.SmartSortResponse> listSmartSort = new List<RequestResponseModel.SmartSortResponse>();

            var listSmartSortAll = await _smartSortRepository.GetAllSmartSortAsync(isActive);

            if(listSmartSortAll == null)
            {
                return null;

            }

            foreach (var smartSort in listSmartSortAll)
            {
                listSmartSort.Add(new RequestResponseModel.SmartSortResponse() { Id = smartSort.SmartSortColumnKey.ToString(), Name = smartSort.ColumnNameText, IsActive = smartSort.ActiveFlag });

            }
            return listSmartSort;
        }
    }
}
