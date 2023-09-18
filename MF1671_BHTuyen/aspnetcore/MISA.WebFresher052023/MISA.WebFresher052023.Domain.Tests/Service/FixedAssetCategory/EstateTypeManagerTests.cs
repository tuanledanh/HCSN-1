using MISA.WebFresher052023.Domain.Entity.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Service;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MISA.WebFresher052023.Domain.Tests.Service.FixedAssetCategory
{
    public class EstateTypeManagerTests
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IFixedAssetCategoryRepository fixedAssetCategoryRepository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public FixedAssetCategoryManager fixedAssetCategoryManager { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [SetUp]
        public void Setup()
        {
            fixedAssetCategoryRepository = Substitute.For<IFixedAssetCategoryRepository>();
            fixedAssetCategoryManager = new FixedAssetCategoryManager(fixedAssetCategoryRepository);
        }
        #endregion


        #region Methods Tests 
        /// <summary>
        /// Test method CheckCodeConflictAsync khi có loại tài sản với mã code truyền vào
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        [Test]
        public async Task CheckFixedAssetCategoryExistByCode_ExistFixedAssetCategory_ConflictFixedAssetCategory()
        {

            // Arrange
            var code = "sdfsdf";
            fixedAssetCategoryRepository.FindByCodeAsync(code).Returns(new FixedAssetCategoryEntity());

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await fixedAssetCategoryManager.CheckCodeConflictAsync(code));
            await fixedAssetCategoryRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test method CheckCodeConflictAsync khi không loại tài sản với mã code truyền vào (Success)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        [Test]
        public async Task CheckFixedAssetCategoryExistByCode_NoExistFixedAssetCategory_Success()
        {
            // Arrange
            var code = "HelloWorld";
            fixedAssetCategoryRepository.FindByCodeAsync(code).ReturnsNull();

            // Act 
            await fixedAssetCategoryManager.CheckCodeConflictAsync(code);

            // Assert
            await fixedAssetCategoryRepository.Received(1).FindByCodeAsync(code);
        }
        #endregion
    }
}
