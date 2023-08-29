using StimulusFrontEnd.Services.Base;
namespace StimulusFrontEnd.Services.Modified
{
    public class ServiceClientModified : Client
    {
        public ServiceClientModified(HttpClient httpClient) : base(httpClient)
        {
        }


        //public override Task<Page> PageTheorieAsync(int id, CancellationToken cancellationToken)
        //{
        //    return base.PageTheorieAsync(id, cancellationToken);
        //}
    }
}
