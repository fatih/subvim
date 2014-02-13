using Nancy;

namespace OmniSharp.LookupAllTypes
{
    public class LookupAllTypesModule : NancyModule
    {
        public LookupAllTypesModule(LookupAllTypesHandler handler)
        {
            Post["/lookupalltypes"] = x =>
            {
                var res = handler.GetLookupAllTypesResponse();
                return Response.AsJson(res);
            };
        }
    }
}