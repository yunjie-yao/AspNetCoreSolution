namespace YangXuAPI.DtoParameters
{
    public class TokenDtoParameter
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 签发人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Token过期时间，单位分钟
        /// </summary>
        public int AccessExpiration { get; set; }
        /// <summary>
        /// RefreshToken过期时间，单位分钟
        /// </summary>
        public int RefreshExpiration { get; set; }
    }
}
