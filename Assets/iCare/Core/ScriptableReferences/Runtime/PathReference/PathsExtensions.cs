namespace iCare {
    public static class PathsExtensions {
        public static string Get(this Paths path) {
            return PathReferenceManager.GetByName(path.ToString()).Value + "/";
        }
    }
}