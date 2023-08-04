using AutoMapper;
using MISA.WebFresher052023.Application.Service;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using NSubstitute;

namespace MISA.WebFresher052023.Application.Tests.Service
{
    [TestFixture]
    public class FixedAssetServiceTests
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IFixedAssetRepository fixedAssetRepository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IMapper mapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IFixedAssetManager fixedAssetManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IUnitOfWork unitOfWork { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public FixedAssetService fixedAssetService { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [SetUp]
        public void SetUp()
        {
            fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
            mapper = Substitute.For<IMapper>();
            fixedAssetManager = Substitute.For<IFixedAssetManager>();
            unitOfWork = Substitute.For<IUnitOfWork>();
            fixedAssetService = new FixedAssetService(fixedAssetRepository, fixedAssetManager, unitOfWork, mapper);
        }
        #endregion

        #region Methods Testset
        /// <summary>
        /// Test hàm DeleteManyEstateAsync khi truyền danh sách rỗng
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task DeleteManyFixedAssetAsync_EmptyList_ThrowException()
        {
            // Arrange
            var fixedAssetIds = new List<string>();

            var expectedExceptionMessage = "Không được truyền danh sách rỗng";

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await fixedAssetService.DeleteManyAsync(fixedAssetIds));

            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));

            await unitOfWork.Received(1).BeginTransactionAsync();

            await unitOfWork.Received(1).RollbackAsync();
        }

        /// <summary>
        /// Test hàm DeleteManyEstateAsync khi truyền danh sách có Id không tồn tại
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task DeleteManyFixedAssetAsync_List10Item_ThrowException()
        {
            // Arrange
            var fixedAssetIds = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                fixedAssetIds.Add(Guid.NewGuid().ToString());
            }

            var fixedAssetEntities = new List<FixedAssetEntity>();
            for (int i = 0; i < 9; i++)
            {
                fixedAssetEntities.Add(new FixedAssetEntity());
            }

            fixedAssetRepository.FindManyByIdAsync(fixedAssetIds).Returns(fixedAssetEntities);

            var expectedExceptionMessage = "Không thể xóa";

            // Act && Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await fixedAssetService.DeleteManyAsync(fixedAssetIds));

            Assert.That(exception.Message, Is.EqualTo(expectedExceptionMessage));

            await fixedAssetRepository.Received(1).FindManyByIdAsync(fixedAssetIds);

            await unitOfWork.Received(1).BeginTransactionAsync();

            await unitOfWork.Received(1).RollbackAsync();

        }

        /// <summary>
        /// Test hàm DeleteManyEstateAsync khi xóa danh sách thành công
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task DeleteManyFixedAssetAsync_List10Item_SuccessAsync()
        {
            // Arrange
            var fixedAssetIds = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                fixedAssetIds.Add(Guid.NewGuid().ToString());
            }

            var fixedAssetEntities = new List<FixedAssetEntity>();
            for (int i = 0; i < 10; i++)
            {
                fixedAssetEntities.Add(new FixedAssetEntity());
            }

            fixedAssetRepository.FindManyByIdAsync(fixedAssetIds).Returns(fixedAssetEntities);

            // Act
            await fixedAssetService.DeleteManyAsync(fixedAssetIds);

            //Assert
            await fixedAssetRepository.Received(1).FindManyByIdAsync(fixedAssetIds);

            await fixedAssetRepository.Received(1).DeleteManyAsync(fixedAssetEntities);

            await unitOfWork.Received(1).BeginTransactionAsync();

            //await unitOfWork.Received(1).RollbackAsync();
        } 
        #endregion
    }
}
