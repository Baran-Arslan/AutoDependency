namespace iCare {
    public static class StringsExtensions {
        public static string Get(this Strings strings) {
            return StringReferenceManager.GetByName(strings.ToString()).Value;
        }
    }
}