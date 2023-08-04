namespace MISA.WebFresher052023.Domain.Entity
{
    public interface IHasKeyEntity
    {
        #region Methods
        /// <summary>
        /// Hàm lấy KeyId
        /// </summary>
        /// <returns>Id bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyId();

        /// <summary>
        /// Hàm lấy KeyCode
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyCode();

        /// <summary>
        /// Set KeyId
        /// </summary>
        /// <param name="id">StringId</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKeyId(string id);
        #endregion

    }
}

