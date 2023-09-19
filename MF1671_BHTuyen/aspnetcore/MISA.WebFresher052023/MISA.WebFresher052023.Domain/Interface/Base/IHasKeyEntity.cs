namespace MISA.WebFresher052023.Domain.Interface.Base
{
    public interface IHasKeyEntity
    {
        #region Methods
        /// <summary>
        /// Hàm lấy KeyId
        /// </summary>
        /// <returns>Id của bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid GetKey();

        /// <summary>
        /// Set KeyId
        /// </summary>
        /// <param name="id">Mã Id của bản ghi</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKey(Guid id);
        #endregion

    }
}

