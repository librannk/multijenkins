using System;
using System.Linq;
using AutoMapper;
using Formulary.API.Model;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Formulary.API.Model.InternalModel;
using Formulary.API.Model.ViewModel;

namespace Formulary.API.BusinessLayer.Concrete
{
    /// <summary>
    /// Business logic of Formulary Service
    /// </summary>
    public class FormularyManager : IFormularyManager
    {
        #region Private Fields
        private readonly IItemRepository _itemReository;
        private readonly IMedicationItemRepository _medicationItemRepository;
        private readonly IFormularyRepository _formularyRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly INDCRepository _nDCRepository;
        private readonly IFacilityNDCAssocRepository _facilityNDCAssocRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="formularyRepository"></param>
        /// <param name="facilityRepository"></param>
        /// <param name="nDCRepository"></param>
        /// <param name="facilityNDCAssocRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="itemReository"></param>
        /// <param name="medicationItemRepository"></param>
        public FormularyManager(
            IFormularyRepository formularyRepository,
            IFacilityRepository facilityRepository,
            INDCRepository nDCRepository,
            IFacilityNDCAssocRepository facilityNDCAssocRepository, IItemRepository itemReository, IMedicationItemRepository medicationItemRepository,
          IUnitOfWork unitOfWork, IMapper mapper
            )
        {
            _itemReository = itemReository;
            _medicationItemRepository = medicationItemRepository;
            _formularyRepository = formularyRepository;
            _facilityRepository = facilityRepository;
            _nDCRepository = nDCRepository;
            _facilityNDCAssocRepository = facilityNDCAssocRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        #endregion

        #region Public Method
        /// <summary>
        /// Save formulary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FormularyEntity> SaveFormulary(FormularyRequest request)
        {
            var itemDuplicateCheck = _formularyRepository.GetAll();

            if (itemDuplicateCheck != null && itemDuplicateCheck.Any(x => x.ItemId == request.ItemId))
            {
                return null;
            }

            var formualryEntity = _mapper.Map<FormularyEntity>(request);
            await _formularyRepository.Add(formualryEntity);
            await _unitOfWork.CommitChanges();
            return formualryEntity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public async Task<FacilityEntity> SaveFacility(FacilityRequest request)
        {
            var facilityEntity = new FacilityEntity()
            {
                CreatedDate = DateTime.Now,
                CreatedBy = Admin.CreatedBy,
                Active = request.Active,
                LastModifiedBy = Admin.LastUpdatedBy,
                LastModifiedDate = DateTime.Now,
                ADUIgnoreCritLow = request.ADUIgnoreCritLow,
                ADUIgnoreStockOut = request.ADUIgnoreStockOut,
                ADUQtyRounding = request.ADUQtyRounding,
                FormularyId = request.FormularyId,
                Approved = request.Approved,
                FacilityId = request.FacilityId
            };

            await _facilityRepository.Add(facilityEntity);
            await _unitOfWork.CommitChanges();
            return facilityEntity;
        }
        /// <summary>
        /// Save NDC
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<NDCEntity> SaveNDC(NDCRequest request)
        {
            var nDCEntity = new NDCEntity()
            {
                FormularyId = request.FormularyId,
                GenericName = request.GenericName,
                NDC = request.NDC,
                TradeName = request.TradeName,
                CreatedDate = DateTime.Now,
                CreatedBy = Admin.CreatedBy,
                Active = request.Active,
                LastModifiedBy = Admin.LastUpdatedBy,
                LastModifiedDate = DateTime.Now,
            };
            await _nDCRepository.Add(nDCEntity);
            await _unitOfWork.CommitChanges();
            return nDCEntity;
        }
        /// <summary>
        /// facilityNDCDetails
        /// </summary>
        /// <param name="facilityNDCDetails"></param>
        public async Task<FacilityNDCAssocEntity> SaveFacilityNDCAssoc(FacilityNDCAssociationRequest facilityNDCDetails)
        {
            var facilityNDCAssocEntity = new FacilityNDCAssocEntity()
            {
                FacilityId = facilityNDCDetails.FacilityId,
                NDCId = facilityNDCDetails.NDCId,
                IsPreferred = facilityNDCDetails.IsPreferred,
                Cost = facilityNDCDetails.Cost,
                CreatedBy = Admin.CreatedBy,
                CreatedDate = DateTime.Now,
                LastModifiedBy = Admin.LastUpdatedBy,
                LastModifiedDate = DateTime.Now
            };
            await _facilityNDCAssocRepository.Add(facilityNDCAssocEntity);
            await _unitOfWork.CommitChanges();
            return facilityNDCAssocEntity;
        }

        /// <summary>
        /// Update Formulary in SQL
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FormularyEntity> UpdateFormulary(int Id, FormularyRequest request)
        {
            var formualryEntity = await _formularyRepository.Get(Id);
            var itemDuplicateCheck = _formularyRepository.GetAll();
            if ((formualryEntity == null || itemDuplicateCheck != null && itemDuplicateCheck.Any(x => x.ItemId == request.ItemId)))
            {
                return null;
            }
            formualryEntity.ItemId = request.ItemId;
            formualryEntity.ItemName = request.ItemName;
            formualryEntity.Active = request.Active;
            formualryEntity.Description = request.Description;
            formualryEntity.LastModifiedDate = DateTime.Now;
            formualryEntity.LastModifiedBy = Admin.LastUpdatedBy;
            _formularyRepository.Update(formualryEntity);
            await _unitOfWork.CommitChanges();
            return formualryEntity;

        }
        /// <summary>
        /// To add system Formulary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BusinessResult<SystemItemSetupRequest>> AddSystemItem(SystemItemSetupRequest request)
        {
           var existingItem = await _itemReository.GetItemByCode(request.ItemId);
            if (existingItem != null)
            {
                return new BusinessResult<SystemItemSetupRequest>(null, CreateUpdateResultEnum.ErrorAlreadyExists);
            }

            var key = Guid.NewGuid();

            var itemEntity = new ItemEntity
            {
                ItemKey = key,
                TenantKey = Guid.NewGuid(),
                CreatedByActorKey = Guid.NewGuid(),
                CreatedDateTime = DateTimeOffset.Now,
                LastModifiedDateTime = DateTime.UtcNow,
                LastModifiedByActorKey = Guid.NewGuid(),
                ItemID = request.ItemId,
                AlternateItemID = request.AlternateItemID,
                MedicationItemFlag = true
        };
            itemEntity.CreatedDateTime = DateTimeOffset.Now;
            
            var medEntity = new MedicationItemEntity
            {
                MedicationItemKey = key,
                TenantKey = Guid.NewGuid(),
                CreatedByActorKey = Guid.NewGuid(),
                CreatedDateTime = DateTimeOffset.Now,
                LastModifiedDateTime = DateTime.UtcNow,
                LastModifiedByActorKey = Guid.NewGuid(),
                MedicationClassKey = request.MedicationClassKey,
                Description = request.Description,
                GLAccountKey = request.GenralLedgerAccountKey,
                DispenseFormLookupKey = request.DispensingFormKey,
                DispenseUnitLookupKey = request.DispensingUnitKey,
                StrengthAmount = request.StrengthAmount,
                ConcentrationVolumeAmount = request.ConcentrationVolumeAmount,
                TotalVolumeAmount = request.TotalVolumeAmount,
                TotalVolumeUnitOfMeasureKey = request.TotalVolumeUnitOfMessureKey,
                StrengthUnitOfMeasureKey = request.StrengthUnitOfMessureKey,
                ConcentrationVolumeUnitOfMeasureKey = request.ConcentrationVolumeUnitOfMessureKey,
                ChargeCode = request.ChargeCode,
                HighRiskFlag = request.HighRiskFlag,
                ChemotherapyFlag = request.ChemotherapyFlag,
                NonFormularyFlag = request.NonFormularyFlag,
                LASAFlag = request.LASAFlag,
                RefrigeratedFlag = request.RefrigeratedFlag,
                DrugFlag = request.DrugFlag,
                ChemoAgentFlag = request.ChemoAgentFlag,
                BioHazFlag = request.BioHazFlag,
                HazAerosolFlag = request.HazAerosolFlag,
                HazBaseFlag = request.HazBaseFlag,
                HazAcidFlag = request.HazAcidFlag,
                HazChemicalFlag = request.HazChemicalFlag,
                HazOxidizerFlag = request.HazOxidizerFlag,
                HazToxicFlag = request.HazToxicFlag,
                FreezerFlag = request.FreezerFlag,
                ActiveFlag = request.ActiveFlag
            };
            itemEntity.MedicationItem = medEntity;

            var systemFormulary =_mapper.Map<SystemFormulary>(request);
            systemFormulary.ItemKey = itemEntity.ItemKey;
            systemFormulary.MedicationItemKey = itemEntity.MedicationItem.MedicationItemKey;


            if (await _itemReository.AddSystemItem(itemEntity) /*&& await _medicationItemRepository.AddMedicationItem(medEntity)*/)
            {
                return new BusinessResult<SystemItemSetupRequest>(systemFormulary,
                    CreateUpdateResultEnum.Success);
            }
            return new BusinessResult<SystemItemSetupRequest>(null, CreateUpdateResultEnum.ValidationFailed);
            
        }

        #endregion
    }
}