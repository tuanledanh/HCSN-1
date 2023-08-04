using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.department;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Tests.Service.Department
{
    [TestFixture]
    public class DepartmentManagerTests
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public IDepartmentRepository departmentRepository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        public DepartmentManager departmentManager { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [SetUp]
        public void Setup()
        {
            departmentRepository = Substitute.For<IDepartmentRepository>();
            departmentManager = new DepartmentManager(departmentRepository);
        }
        #endregion

        #region Methods Test
        /// <summary>
        /// Test method CheckDepartmentExistByCode khi có phòng ban tồn tại với mã code truyền vào
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckDepartmentExistByCode_ExistDepartment_ConflictDepartment()
        {
            // Arrange
            var code = "abc";
            departmentRepository.FindByCodeAsync(code).Returns(new DepartmentEntity());

            // Act && Assert
            Assert.ThrowsAsync<ConflictException>(async () => await departmentManager.CheckExistByCode(code));
            await departmentRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Test method CheckDepartmentExistByCode khi không có phòng ban tồn tại với mã code truyền vào (Success)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (20/07/2023)
        [Test]
        public async Task CheckDepartmentExistByCode_NoExistDepartment_Success()
        {
            // Arrange
            var code = "HelloWorld";
            departmentRepository.FindByCodeAsync(code).ReturnsNull();

            // Act 
            await departmentManager.CheckExistByCode(code);

            // Assert
            await departmentRepository.Received(1).FindByCodeAsync(code);
        }
        #endregion
    }
}
