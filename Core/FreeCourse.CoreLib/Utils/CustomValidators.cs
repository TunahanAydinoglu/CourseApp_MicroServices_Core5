using MongoDB.Bson;

namespace FreeCourse.CoreLib.Utils
{
    public static class CustomValidators
    {
        public static bool IsObjectId(string id)
        {
            return ObjectId.TryParse(id, out _);
        }
    }
}