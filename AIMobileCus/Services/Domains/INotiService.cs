using AIMobileCus.Models.DataModels;

namespace AIMobileCus.Services.Domains
{
    public interface INotiService
    {
        void Entry(NotiEntity notiEntity);
        IList<NotiEntity> RetrieveAll (string customerId);
    }
}
