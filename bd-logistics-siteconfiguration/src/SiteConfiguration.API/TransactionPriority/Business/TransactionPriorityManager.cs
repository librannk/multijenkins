using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BD.Core.Context;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using SiteConfiguration.API.TransactionPriority.Abstractions;
using SiteConfiguration.API.TransactionPriority.RequestResponseModel;
using SiteConfiguration.API.Common;
using SiteConfiguration.API.TransactionPriority.Models;
using BD.Core.EventBus.Abstractions;
using Microsoft.Extensions.Options;
using SiteConfiguration.API.IntegrationEvents.Events;
using AutoMapper;

namespace SiteConfiguration.API.TransactionPriority.Business
{
    public class TransactionPriorityManager : ITransactionPriorityManager
    {
        #region PrivateFields
        private readonly ITransactionPriorityRepository _transactionPriorityRepository;
        private readonly ILogger<TransactionPriorityManager> _logger;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionPrioritySmartSortRepository _transactionPrioritySmartSortRepository;
        private readonly ISmartSortRepository _smartSortRepository;
        private readonly IEventBus _eventBus;
        private Configuration.Configuration _configuration;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TransactionPriorityManager(ITransactionPriorityRepository tranPriorityRepository, IExecutionContextAccessor executionContextAccessor
            , IUnitOfWork unitOfWork, ILogger<TransactionPriorityManager> logger, ITransactionPrioritySmartSortRepository transactionPrioritySmartSortRepository, ISmartSortRepository smartSortRepository, IEventBus eventBus,
             IOptions<Configuration.Configuration> options, IMapper mapper)
        {
            _transactionPriorityRepository = tranPriorityRepository;
            _executionContextAccessor = executionContextAccessor;
            _unitOfWork = unitOfWork;
            _transactionPrioritySmartSortRepository = transactionPrioritySmartSortRepository;
            _smartSortRepository = smartSortRepository;
            _eventBus = eventBus;
            _configuration = options.Value;
            _mapper = mapper;
        }
        #endregion

        #region Business Methods
        public async Task<BusinessResponse> AddTransactionPriority(TransactionPriorityPost transactionPriorityPost, Guid facilityID, Dictionary<string, string> headers)
        {
            Models.TransactionPriority objTransactionPriority = new Models.TransactionPriority();

            bool? codeExist = _transactionPriorityRepository.GetAll().ToList()?.Any(x => x.PriorityCode.Trim().ToLower() == transactionPriorityPost.PriorityCode.Trim().ToLower());
            if (codeExist != null && codeExist == true)
            {
                return new BusinessResponse() { IsSuccesss = false, Message = transactionPriorityPost.PriorityCode + " code already exist." };

            }
            
            var newID = Utility.GetNewGuid();
            objTransactionPriority.Id = newID;
            objTransactionPriority.TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey);
            objTransactionPriority.FacilityKey = facilityID;
            objTransactionPriority.PrintLabelKey = Guid.Parse("5c43149d-7c13-49f5-a19d-c0c4f4e58431");
            objTransactionPriority.PriorityCode = transactionPriorityPost.PriorityCode;
            objTransactionPriority.PriorityName = transactionPriorityPost.PriorityName;
            objTransactionPriority.PriorityOrder = 1;
            objTransactionPriority.LegendBackColor = transactionPriorityPost.LegendForeColor;
            objTransactionPriority.LegendForeColor = transactionPriorityPost.LegendBackColor;
            objTransactionPriority.MaxOnHoldLength = 0;
            objTransactionPriority.ForManualPickFlag = transactionPriorityPost.ForManualPickFlag;
            objTransactionPriority.ForManualRestockFlag = transactionPriorityPost.ForManualRestockFlag;
            objTransactionPriority.ADUFlag = transactionPriorityPost.ADUFlag;
            objTransactionPriority.AutoReceiveFlag = false;
            objTransactionPriority.ActiveFlag = transactionPriorityPost.ActiveFlag;
            objTransactionPriority.SystemFlag = true;
            objTransactionPriority.UseInterfaceMedNameFlag = transactionPriorityPost.UseInterfaceMedNameFlag;
            objTransactionPriority.CreatedByActorKey = Utility.GetNewGuid();
            objTransactionPriority.LastModifiedByActorKey = Utility.GetNewGuid();
            objTransactionPriority.CreatedDateTime = DateTimeOffset.Now;
            objTransactionPriority.LastModifiedUTCDateTime = DateTime.UtcNow;

            var maxOrder = 1;
            if (_transactionPriorityRepository.GetAll()?.ToList()?.Count > 0)
            {
                maxOrder = _transactionPriorityRepository.GetAll().Select(i => i.PriorityOrder).Max() + 1;
            }
            objTransactionPriority.PriorityOrder = maxOrder;

            await _transactionPriorityRepository.AddAsync(objTransactionPriority);
            _unitOfWork.CommitChanges();
            SendEvent(GetTransactionPriorityById(newID.ToString()), "Add", headers);
            return new BusinessResponse() { IsSuccesss = true, Message = objTransactionPriority.Id.ToString() };

        }
        private void SendEvent(Models.TransactionPriority transactionPriorityById, string type, Dictionary<string, string> headers)
        {
            var eventMessage = new TransactionPriorityEvent
            {
                Message = transactionPriorityById,
                EventType = type
            };
            try{
                _eventBus.Publish(_configuration.TransactionPriority, eventMessage, headers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public Models.TransactionPriority GetTransactionPriorityById(string id)
        {

            return _transactionPriorityRepository.Get(Guid.Parse(id));

        }

        public async Task<BusinessResponse> UpdateTransactionPriorityAsync(string tranPriorityKey, TransactionPriorityPut transactionPriorityPut, Guid facilityKey, Dictionary<string, string> headers)
        {
            Models.TransactionPriority objTransactionPriority = new Models.TransactionPriority();
            objTransactionPriority = _transactionPriorityRepository.GetByTransactionPriorityAndFacilityKey(Guid.Parse(tranPriorityKey), facilityKey);

            if (objTransactionPriority != null)
            {

                objTransactionPriority.PriorityName = transactionPriorityPut.PriorityName;
                objTransactionPriority.LegendBackColor = transactionPriorityPut.LegendBackColor;
                objTransactionPriority.LegendForeColor = transactionPriorityPut.LegendForeColor;
                objTransactionPriority.MaxOnHoldLength = transactionPriorityPut.MaxOnHoldLength;
                objTransactionPriority.ForManualPickFlag = transactionPriorityPut.ForManualPickFlag;
                objTransactionPriority.ForManualRestockFlag = transactionPriorityPut.ForManualRestockFlag;
                objTransactionPriority.ADUFlag = transactionPriorityPut.ADUFlag;
                objTransactionPriority.ActiveFlag = transactionPriorityPut.ActiveFlag;
                objTransactionPriority.SystemFlag = transactionPriorityPut.SystemFlag;
                objTransactionPriority.UseInterfaceMedNameFlag = transactionPriorityPut.UseInterfaceMedNameFlag;
                objTransactionPriority.LastModifiedByActorKey = Guid.NewGuid();
                objTransactionPriority.LastModifiedUTCDateTime = DateTime.UtcNow;
                if (transactionPriorityPut.PriorityOrder != objTransactionPriority.PriorityOrder)
                {
                    if (objTransactionPriority.PriorityOrder > transactionPriorityPut.PriorityOrder)
                    {
                        await _transactionPriorityRepository.UpdatePriorityOrderUpAsync(transactionPriorityPut.PriorityOrder);
                    }
                    else
                    {
                        await _transactionPriorityRepository.UpdatePriorityOrderDownAsync(transactionPriorityPut.PriorityOrder, objTransactionPriority.PriorityOrder);
                    }

                }
                objTransactionPriority.PriorityOrder = transactionPriorityPut.PriorityOrder;
                _transactionPriorityRepository.Update(objTransactionPriority);
                _unitOfWork.CommitChanges();
                SendEvent(GetTransactionPriorityById(tranPriorityKey), "Update", headers);
                return new BusinessResponse() { IsSuccesss = true, Message = "TransactionPriority Update." };
            }
            else
            {
                return new BusinessResponse() { IsSuccesss = false, Message = "No TransactionPriority Exist to update." };
            }

        }


        public async Task<IEnumerable<TransactionPriorityGet>> GetAllTransactionPriorityASync(int offset, int limit, bool isActive, string facilityKey)
        {

            List<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>();

            var listTransactionPriority = await _transactionPriorityRepository.GetAllTransactionPriorityAsync(isActive, offset, limit, Guid.Parse(facilityKey));

            if (listTransactionPriority == null)
            {
                return null;

            }

            foreach (var transactionPriority in listTransactionPriority)
            {
                TransactionPriorityGet objTransactionPriorityGet = new TransactionPriorityGet();
                objTransactionPriorityGet.TranPriorityKey = transactionPriority.Id.ToString();
                objTransactionPriorityGet.PriorityCode = transactionPriority.PriorityCode;
                objTransactionPriorityGet.PriorityName = transactionPriority.PriorityName;
                objTransactionPriorityGet.ForManualPickFlag = transactionPriority.ForManualPickFlag;
                objTransactionPriorityGet.ForManualRestockFlag = transactionPriority.ForManualRestockFlag;
                objTransactionPriorityGet.UseInterfaceMedNameFlag = transactionPriority.UseInterfaceMedNameFlag;
                objTransactionPriorityGet.ADUFlag = transactionPriority.ADUFlag;
                objTransactionPriorityGet.LegendForeColor = transactionPriority.LegendForeColor;
                objTransactionPriorityGet.LegendBackColor = transactionPriority.LegendBackColor;
                objTransactionPriorityGet.ActiveFlag = transactionPriority.ActiveFlag;
                objTransactionPriorityGet.MaxOnHoldLength = transactionPriority.MaxOnHoldLength;
                objTransactionPriorityGet.SystemFlag = transactionPriority.SystemFlag;
                objTransactionPriorityGet.PriorityOrder = transactionPriority.PriorityOrder;
                objTransactionPriorityGet.SmartSorts = new List<TransactionPrioritySmartSort>();
                var result = transactionPriority.SmartSort.Select(e => new TransactionPrioritySmartSort()
                {
                    SmartSortColumnKey = e.SmartSortColumnKey.ToString(),
                    SmartSortName = e.SmartSortColumnKeyNavigation.ColumnNameText,
                    SmartSortOrder = e.SmartSortOrder,


                }).OrderBy(x => x.SmartSortName);
                objTransactionPriorityGet.SmartSorts.AddRange(result);
                listTransactionPriorityGet.Add(objTransactionPriorityGet);
            }
            return listTransactionPriorityGet.OrderBy(x => x.PriorityName);

        }

        public async Task<IEnumerable<TransactionPriorityGet>> GetAllSerachedTransactionPriorityAsync(string transactionPriorityName, int offset, int limit, string facilityKey)
        {

            List<TransactionPriorityGet> listTransactionPriorityGet = new List<TransactionPriorityGet>();

            var listTransactionPriority = await _transactionPriorityRepository.GetAllSerachedTransactionPriorityAsync(transactionPriorityName, offset, limit, Guid.Parse(facilityKey));

            if (listTransactionPriority == null)
            {
                return null;

            }

            foreach (var transactionPriority in listTransactionPriority)
            {
                TransactionPriorityGet objTransactionPriorityGet = new TransactionPriorityGet();
                objTransactionPriorityGet.TranPriorityKey = transactionPriority.Id.ToString();
                objTransactionPriorityGet.PriorityCode = transactionPriority.PriorityCode;
                objTransactionPriorityGet.PriorityName = transactionPriority.PriorityName;
                objTransactionPriorityGet.ForManualPickFlag = transactionPriority.ForManualPickFlag;
                objTransactionPriorityGet.ForManualRestockFlag = transactionPriority.ForManualRestockFlag;
                objTransactionPriorityGet.UseInterfaceMedNameFlag = transactionPriority.UseInterfaceMedNameFlag;
                objTransactionPriorityGet.ADUFlag = transactionPriority.ADUFlag;
                objTransactionPriorityGet.LegendForeColor = transactionPriority.LegendForeColor;
                objTransactionPriorityGet.LegendBackColor = transactionPriority.LegendBackColor;
                objTransactionPriorityGet.ActiveFlag = transactionPriority.ActiveFlag;
                objTransactionPriorityGet.MaxOnHoldLength = transactionPriority.MaxOnHoldLength;
                objTransactionPriorityGet.SystemFlag = transactionPriority.SystemFlag;
                objTransactionPriorityGet.PriorityOrder = transactionPriority.PriorityOrder;
                objTransactionPriorityGet.SmartSorts = new List<TransactionPrioritySmartSort>();
                var result = transactionPriority.SmartSort.Select(e => new TransactionPrioritySmartSort()
                {
                    SmartSortColumnKey = e.SmartSortColumnKey.ToString(),
                    SmartSortName = e.SmartSortColumnKeyNavigation.ColumnNameText,
                    SmartSortOrder = e.SmartSortOrder,


                }).OrderBy(x => x.SmartSortName);
                objTransactionPriorityGet.SmartSorts.AddRange(result);
                listTransactionPriorityGet.Add(objTransactionPriorityGet);
            }
            return listTransactionPriorityGet.OrderBy(x => x.PriorityName);

        }

        public async Task<IEnumerable<TransactionPrioritySmartSort>> GetSmartSortForTransactionPriority(string transactionPriorityKey, string facilityKey)
        {

            var isTransactionPriorityExist = _transactionPriorityRepository.GetByTransactionPriorityAndFacilityKey(Guid.Parse(transactionPriorityKey), Guid.Parse(facilityKey));
            if (isTransactionPriorityExist == null)
            {
                return null;
            }

            var listTransactionPrioritySmartSort = await _transactionPrioritySmartSortRepository.GetSmartSortForTransactionPriorityAsync(Guid.Parse(transactionPriorityKey));
            if (listTransactionPrioritySmartSort == null)
            {
                return null;

            }
            return listTransactionPrioritySmartSort;
        }


        public async Task<BusinessResponse> PutSmartSortForTransactionPriorityAsync(string transactionPriorityKey, string facilityKey, List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut, Dictionary<string, string> headers)
        {
            var isTransactionPriorityExist = _transactionPriorityRepository.GetByTransactionPriorityAndFacilityKey(Guid.Parse(transactionPriorityKey), Guid.Parse(facilityKey));
            if (isTransactionPriorityExist == null )
            {
                return new BusinessResponse() { IsSuccesss = false, Message = "No transaction priority exist." };
            }

            var smartSorts = await _smartSortRepository.GetAllSmartSortAsync(true);
            if (smartSorts == null || smartSorts.ToList().Count == 0 || listTransactionPrioritySmartSortPut.Count > smartSorts.ToList().Count)
            {
                return new BusinessResponse() { IsSuccesss = false, Message = "No Such SmartSort exist." };
            }

            foreach (var transactionPrioritySmartSortPut in listTransactionPrioritySmartSortPut)
            {
                bool smartSortExist = true;
                foreach (var smartSort in smartSorts)
                {
                    if (smartSort.SmartSortColumnKey.ToString().ToLower() == transactionPrioritySmartSortPut.SmartSortColumnKey.ToLower())
                    {
                        smartSortExist = true;
                        break;
                    }
                    else
                    {
                        smartSortExist = false;
                    }

                }
                if (!smartSortExist)
                {
                    return new BusinessResponse() { IsSuccesss = false, Message = "No SmartSort is present " };

                }
               
            }

            List<SmartSort> listSmartSort = new List<SmartSort>();
            
                foreach (var transactionPrioritySmartSort in listTransactionPrioritySmartSortPut)
                {
                    if (transactionPrioritySmartSort.SmartSortOrder> smartSorts.ToList().Count || transactionPrioritySmartSort.SmartSortOrder <1)
                    {
                        return new BusinessResponse() { IsSuccesss = false, Message = "Smart Sort order is not correct." };
                    }
                listSmartSort.Add(new SmartSort() { SmartSortColumnKey=new Guid(transactionPrioritySmartSort.SmartSortColumnKey), TransPriorityKey=new Guid(transactionPriorityKey), SmartSortOrder = transactionPrioritySmartSort.SmartSortOrder,TenantKey= Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey), LastModifiedUTCDateTime = DateTime.UtcNow, LastModifiedByActorKey = Utility.GetNewGuid(), CreatedByActorKey = Utility.GetNewGuid(),CreatedDateTime= DateTimeOffset.Now });
                 
                }

            await _transactionPrioritySmartSortRepository.PutSmartSortForTransactionPriorityAsync(new Guid(transactionPriorityKey), listSmartSort);

           _unitOfWork.CommitChanges();
            SendEventSmartSort(listSmartSort, "Update", headers);
            return new BusinessResponse() { IsSuccesss = true, Message = "Smart Sorts Updated." };

        }
        private void SendEventSmartSort(List<SmartSort> listSmartSort, string type, Dictionary<string, string> headers)
        {
            var eventMessage = new TransactionPrioritySmartSortEvent
            {
                Message = listSmartSort,
                EventType = type
            };
            try
            {
               // _eventBus.Publish(_configuration.TransactionPrioritySmartSort, eventMessage, headers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public async Task<BusinessResponse> PostSmartSortForTransactionPriorityAsync(string transactionPriorityKey, string facilityKey, List<TransactionPrioritySmartSortPut> listTransactionPrioritySmartSortPut, Dictionary<string, string> headers)
        {
            var isTransactionPriorityExist = _transactionPriorityRepository.GetByTransactionPriorityAndFacilityKey(Guid.Parse(transactionPriorityKey), Guid.Parse(facilityKey));
            if (isTransactionPriorityExist == null)
            {
                return new BusinessResponse() { IsSuccesss = false, Message = "No transaction priority exist." };
            }

            var smartSorts = await _smartSortRepository.GetAllSmartSortAsync(true);
            if (smartSorts == null || smartSorts.ToList().Count == 0 || listTransactionPrioritySmartSortPut.Count > smartSorts.ToList().Count)
            {
                return new BusinessResponse() { IsSuccesss = false, Message = "No SmartSort exist." };
            }

            foreach (var transactionPrioritySmartSort in listTransactionPrioritySmartSortPut)
            {
                if (transactionPrioritySmartSort.SmartSortOrder > smartSorts.ToList().Count || transactionPrioritySmartSort.SmartSortOrder < 1)
                {
                    return new BusinessResponse() { IsSuccesss = false, Message = "Smart Sort order is not correct." };
                }
            }
            List<SmartSort> listSmartSort = new List<SmartSort>();
            foreach (var transactionPrioritySmartSortPut in listTransactionPrioritySmartSortPut)
            {
                bool smartSortExist = true;
                foreach (var smartSort in smartSorts)
                {
                    if (smartSort.SmartSortColumnKey.ToString().ToLower() == transactionPrioritySmartSortPut.SmartSortColumnKey.ToLower())
                    {
                        smartSortExist = true;
                        break;
                    }
                    else
                    {
                        smartSortExist = false;
                    }

                }
                if (!smartSortExist)
                {
                    return new BusinessResponse() { IsSuccesss = false, Message = "No SmartSort is present " };

                }
                listSmartSort.Add(new SmartSort() { TransPriorityKey = new Guid(transactionPriorityKey), SmartSortColumnKey = new Guid(transactionPrioritySmartSortPut.SmartSortColumnKey), TenantKey = Guid.Parse(_executionContextAccessor.Current.Tenant.TenantKey), SmartSortOrder = transactionPrioritySmartSortPut.SmartSortOrder, CreatedByActorKey = Utility.GetNewGuid(), LastModifiedByActorKey = Utility.GetNewGuid(), CreatedDateTime = DateTimeOffset.Now, LastModifiedUTCDateTime = DateTime.UtcNow });
            }

            var listTransactionPrioritySmartSort = await _transactionPrioritySmartSortRepository.GetSmartSortForTransactionPriorityAsync(Guid.Parse(transactionPriorityKey));
            if (listTransactionPrioritySmartSort == null || listTransactionPrioritySmartSort.ToList().Count == 0)
            {
                await _transactionPrioritySmartSortRepository.PostSmartSortForTransactionPriorityAsync(new Guid(transactionPriorityKey), listSmartSort);
            }
            else
            {
                foreach (var transactionPrioritySmartSort in listTransactionPrioritySmartSort)
                {
                    bool smartSortExist = false;
                    foreach (var transactionPrioritySmartSortPut in listTransactionPrioritySmartSortPut)
                    {
                        if (transactionPrioritySmartSortPut.SmartSortColumnKey.ToLower() == transactionPrioritySmartSort.SmartSortColumnKey.ToLower() || transactionPrioritySmartSortPut.SmartSortOrder == transactionPrioritySmartSort.SmartSortOrder)
                        {
                            smartSortExist = true;
                            break;
                        }


                    }
                    if (smartSortExist)
                    {
                        return new BusinessResponse() { IsSuccesss = false, Message = "Smart Sort already exist." };
                    }
                }
                await _transactionPrioritySmartSortRepository.PostSmartSortForTransactionPriorityAsync(new Guid(transactionPriorityKey), listSmartSort);
            }
            _unitOfWork.CommitChanges();
           // SendEventSmartSort(listSmartSort, "Add", headers);
            return new BusinessResponse() { IsSuccesss = true, Message = "Smart Sort Created" };

        }
        #endregion
    }
}
