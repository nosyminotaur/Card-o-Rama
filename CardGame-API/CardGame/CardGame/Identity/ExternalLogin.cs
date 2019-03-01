namespace CardGame.Identity
{
    /// <summary>
    /// Holds data regarding external login providers
    /// This class can be extended for specific login providers
    /// </summary>
    public class ExternalLogin
    {
        /// <summary>
        /// Name of the Provider, example, Google, Facebook etc.
        /// </summary>
        public string LoginProvider;
        /// <summary>
        /// Normally, the ID Token or Refresh token of the login provider
        /// </summary>
        public string LoginProderKey;
    }

}
}
