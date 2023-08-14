﻿using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Service.Base
{
    public abstract class BaseManager<TEntity> : IBaseManager<TEntity>
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region Properties
        /// <summary>
        /// Message lỗi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public abstract string MessageError { get; set; } 
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseRepository"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        protected BaseManager(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra trùng mã 
        /// </summary>
        /// <param name="entityCode">entityCode</param>
        /// <returns></returns>
        /// <exception cref="ConfilctException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckExistByCode(string entityCode)
        {
            var entity = await _baseRepository.FindByCodeAsync(entityCode);

            if (entity != null)
            {
                throw new ConflictException(MessageError);
            }
            return;
        }
        #endregion
    }
}