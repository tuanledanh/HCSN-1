using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Entity.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Service;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace MISA.WebFresher052023.Domain.Tests.Service.FixedAsset
{
    public class FixedAssetManagerTests
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
        public IFixedAssetCategoryRepository fixedAssetCategoryRepository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IDepartmentRepository departmentRespostory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public FixedAssetManager fixedAssetManager { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [SetUp]
        public void Setup()
        {
            this.fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
            this.fixedAssetCategoryRepository = Substitute.For<IFixedAssetCategoryRepository>();
            this.departmentRespostory = Substitute.For<IDepartmentRepository>();
            this.fixedAssetManager = new FixedAssetManager(fixedAssetRepository, fixedAssetCategoryRepository, departmentRespostory);
        }
        #endregion

        #region Methods Tests
        /// <summary>
        /// Test method CheckEstateExistByCode khi tồn tại FixedAsset với mã code truyền vào
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckEstateExistByCode_ExistEstate_ConflictEstate()
        {
            // Arrange
            var code = "sdfsdf";
            fixedAssetRepository.FindByCodeAsync(code).Returns(new FixedAssetEntity());

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await fixedAssetManager.CheckExistByCode(code));
            await fixedAssetRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test method CheckEstateExistByCode khi không tồn tại FixedAsset với mã code truyền vào (success)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckEstateExistByCode_NoExistEstate_Success()
        {
            // Arrange
            var code = "HelloWorld";
            fixedAssetRepository.FindByCodeAsync(code).ReturnsNull();

            // Act
            await fixedAssetManager.CheckExistByCode(code);

            // Assert
            await fixedAssetRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test method CheckExistByDepartmentId khi không tồn tại Department với Id truyền vào
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckExistByDepartmentId_NoExistDepartment_ThrowNotFoundException()
        {
            // Arrange
            var departmentId = Guid.NewGuid().ToString();
            departmentRespostory.FindAsync(departmentId).ReturnsNull();

            // Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await fixedAssetManager.CheckExistByDepartmentId(departmentId));
            await departmentRespostory.Received(1).FindAsync(departmentId);
        }


        /// <summary>
        /// Test method CheckExistByDepartmentId khi tồn tại Department với Id truyền vào (Success)
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckExistByDepartmentId_ExistDepartment_Success()
        {
            // Arrange
            var departmentId = Guid.NewGuid().ToString();
            departmentRespostory.FindAsync(departmentId).Returns(new DepartmentEntity());

            // Act 
            await fixedAssetManager.CheckExistByDepartmentId(departmentId);

            // Assert
            await departmentRespostory.Received(1).FindAsync(departmentId);
        }


        /// <summary>
        /// Test method CheckExistByFixedAssetCategoryId khi không tồn tại FixedAssetCategory với Id truyền vào
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckExistByFixedAssetCategoryId_NoExistFixedAssetCategory_ThrowNotFoundException()
        {
            // Arrange
            var fixedAssetCategoryId = Guid.NewGuid().ToString();
            departmentRespostory.FindAsync(fixedAssetCategoryId).ReturnsNull();

            // Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await fixedAssetManager.CheckExistByFixedAssetCategoryId(fixedAssetCategoryId));
            await fixedAssetCategoryRepository.Received(1).FindAsync(fixedAssetCategoryId);
        }

        /// <summary>
        /// Test method CheckExistByFixedAssetCategoryId khi tồn tại FixedAssetCategory với Id truyền vào (Success)
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckExistByFixedAssetCategoryId_ExistFixedAssetCategory_Success()
        {
            // Arrange
            var fixedAssetCategoryId = Guid.NewGuid().ToString();
            fixedAssetCategoryRepository.FindAsync(fixedAssetCategoryId).Returns(new FixedAssetCategoryEntity());

            // Act 
            await fixedAssetManager.CheckExistByFixedAssetCategoryId(fixedAssetCategoryId);

            // Assert
            await fixedAssetCategoryRepository.Received(1).FindAsync(fixedAssetCategoryId);
        }
        #endregion
    }
}
