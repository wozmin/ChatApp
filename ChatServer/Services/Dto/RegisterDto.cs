namespace Services.Dto
{
    /// <summary>
    /// Register Dto
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// User email 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Address { get; set; }
    }
}
