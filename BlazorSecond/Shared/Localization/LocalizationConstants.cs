namespace BlazorSecond.Shared.Localization
{
    /// <summary>
    /// 対応言語の列挙
    /// </summary>
    public static class LocalizationConstants
    {
        public static readonly LanguageCode[] SupportedLanguages = {
            new LanguageCode
            {
                Code = "ja-JP",
                DisplayName= "Japanese"
            },
            new LanguageCode
            {
                Code = "en-US",
                DisplayName= "English"
            }
        };
    }
}
