using DogHouseService.Core.DataTransferObjects.DogObjects;

namespace DogHouseService.Core.Helpers
{
    public static class SortOptionsHelper
    {
        private static readonly List<string> _sortAttributes;

        static SortOptionsHelper()
        {
            _sortAttributes = typeof(DogResponse).GetProperties().Select(p => p.Name).ToList();
        }

        public static List<string> GetSortAttributeOptions()
        {
            return _sortAttributes;
        }
    }
}
