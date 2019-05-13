using AutoMapper;
using Formulary.API.BusinessLayer.Contract;
using Formulary.API.Common;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Formulary.API.Model;
using Formulary.API.Model.InternalModel;
using System;
using System.Threading.Tasks;

namespace Formulary.API.BusinessLayer.Concrete
{
    /// <summary>
    /// ItemSetUpManager.
    /// </summary>
    public class SystemItemSetUpManager : ISystemItemSetUpManager
    {
        #region Private Fields
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemItemSetUpManager"/> class.
        /// </summary>
        /// <param name="itemRepository">The item repository.</param>
        /// <param name="mapper">The mapper.</param>
        public SystemItemSetUpManager(
            IItemRepository itemRepository,
            IMapper mapper
        )
        {
            _itemRepository = itemRepository;
            _mapper = mapper;

        }

        #endregion

        /// <summary>
        /// Updates the system item set up.
        /// </summary>
        /// <param name="itemkey">The itemkey.</param>
        /// <param name="medicationItem">The medication item.</param>
        /// <returns>Task&lt;BusinessResult&lt;Model.MedicationItem&gt;&gt;.</returns>
        public async Task<BusinessResult<SystemItemSetupRequest>> UpdateSystemItemSetUp(Guid itemkey, SystemItemSetupRequest medicationItem)
        {
            var itemEntity = await _itemRepository.GetItemById(itemkey);
            if (itemEntity == null)
            {
                return new BusinessResult<SystemItemSetupRequest>(null, CreateUpdateResultEnum.NotFound);
            }

            //set value for item 
            itemEntity.ItemID = medicationItem.ItemId;
            itemEntity.AlternateItemID = medicationItem.AlternateItemID;
            itemEntity.LastModifiedByActorKey = Guid.NewGuid();
            itemEntity.LastModifiedDateTime = DateTime.UtcNow;

            //set value for medication item
            itemEntity.MedicationItem.MedicationItemKey = itemkey;
            itemEntity.MedicationItem.ActiveFlag = medicationItem.ActiveFlag;
            itemEntity.MedicationItem.MedicationClassKey = medicationItem.MedicationClassKey;
            itemEntity.MedicationItem.Description = medicationItem.Description;
            itemEntity.MedicationItem.GLAccountKey = medicationItem.GenralLedgerAccountKey;
            itemEntity.MedicationItem.DispenseFormLookupKey = medicationItem.DispensingFormKey;
            itemEntity.MedicationItem.DispenseUnitLookupKey = medicationItem.DispensingUnitKey;
            itemEntity.MedicationItem.StrengthAmount = medicationItem.StrengthAmount;
            itemEntity.MedicationItem.ConcentrationVolumeAmount = medicationItem.ConcentrationVolumeAmount;
            itemEntity.MedicationItem.TotalVolumeAmount = medicationItem.TotalVolumeAmount;
            itemEntity.MedicationItem.StrengthUnitOfMeasureKey = medicationItem.StrengthUnitOfMessureKey;
            itemEntity.MedicationItem.TotalVolumeUnitOfMeasureKey = medicationItem.TotalVolumeUnitOfMessureKey;
            itemEntity.MedicationItem.ChargeCode = medicationItem.ChargeCode;
            itemEntity.MedicationItem.HighRiskFlag = medicationItem.HighRiskFlag;
            itemEntity.MedicationItem.LASAFlag = medicationItem.LASAFlag;
            itemEntity.MedicationItem.DrugFlag = medicationItem.DrugFlag;
            itemEntity.MedicationItem.FreezerFlag = medicationItem.FreezerFlag;
            itemEntity.MedicationItem.ChemotherapyFlag = medicationItem.ChemotherapyFlag;
            itemEntity.MedicationItem.RefrigeratedFlag = medicationItem.RefrigeratedFlag;
            itemEntity.MedicationItem.NonFormularyFlag = medicationItem.NonFormularyFlag;
            itemEntity.MedicationItem.HazToxicFlag = medicationItem.HazToxicFlag;
            itemEntity.MedicationItem.HazAerosolFlag = medicationItem.HazAerosolFlag;
            itemEntity.MedicationItem.HazOxidizerFlag = medicationItem.HazOxidizerFlag;
            itemEntity.MedicationItem.HazChemicalFlag = medicationItem.HazChemicalFlag;
            itemEntity.MedicationItem.HazAcidFlag = medicationItem.HazAcidFlag;
            itemEntity.MedicationItem.HazBaseFlag = medicationItem.HazBaseFlag;
            itemEntity.MedicationItem.ChemoAgentFlag = medicationItem.ChemoAgentFlag;
            itemEntity.MedicationItem.BioHazFlag = medicationItem.BioHazFlag;
            itemEntity.MedicationItem.LastModifiedByActorKey = Guid.NewGuid();
            itemEntity.MedicationItem.LastModifiedDateTime = DateTime.UtcNow;

            //TODO: for ndc
            //var productIdentificationEntity = _mapper.Map<ICollection<ProductIdentification>,ICollection<ProductIdentificationEntity>>(medicationItem.ProductIdentifications);
            //itemEntity.MedicationItem.ProductIdentifications = productIdentificationEntity;

            //List<PreferredOrdering> preferredOrderingList = new List<PreferredOrdering>();

            //foreach (var item in medicationItem.ProductIdentifications)
            //{
            //    PreferredOrdering preferredOrdering = new PreferredOrdering();
            //    preferredOrdering.PreferredOrderingKey = item.preferredOrderingKey;
            //    preferredOrdering.IsPreferred = item.PreferredNDC;
            //    preferredOrdering.ItemKey = medicationItem.ItemKey;
            //    preferredOrdering.ProductIDKey = item.ProductIdentificationKey;
            //    preferredOrderingList.Add(preferredOrdering);
            //}

            //List<PreferredOrderingEntity> dhjhf = new List<PreferredOrderingEntity>();
            //var preferredOrderingEntity = _mapper.Map(preferredOrderingList,dhjhf);
            //itemEntity.PreferredOrderings = preferredOrderingEntity;

            var updateResult = await _itemRepository.UpdateSystemItemSetUp(itemEntity);

            if (updateResult)
            {
                return new BusinessResult<SystemItemSetupRequest>(
                    _mapper.Map<ItemEntity, SystemItemSetupRequest>(itemEntity), CreateUpdateResultEnum.Success);
            }

            return new BusinessResult<SystemItemSetupRequest>(null, CreateUpdateResultEnum.ValidationFailed);
        }
    }
}
