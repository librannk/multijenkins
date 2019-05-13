using System.Threading.Tasks;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.UnitOfWork;
using Formulary.API.Infrastructure.DataAccess.SQL.EFRepository;
using Formulary.API.Model.ViewModel;
using Microsoft.EntityFrameworkCore;


namespace Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryImplementation
{
    /// <summary>
    /// Class MedicationItemRepository.
    /// Implements the <see cref="MedicationItemEntity" />
    /// Implements the <see cref="Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.EntityRepositoryContracts.IMedicationItemRepository" />
    /// </summary>
    /// <seealso cref="MedicationItemEntity" />
    /// <seealso cref="IMedicationItemRepository" />
    public class MedicationItemRepository : BaseRepository<MedicationItemEntity>, IMedicationItemRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationItemRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MedicationItemRepository(ApplicationDBContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// add medicationItemEntity to database
        /// </summary>
        /// <param name="medicationItemEntity"></param>
        /// <returns></returns>
        public async Task<bool> AddMedicationItem(MedicationItemEntity medicationItemEntity)
        {
            await Add(medicationItemEntity);
            //TODO: Actor
            return await _unitOfWork.CommitChanges() > 0;
        }

    }
}
