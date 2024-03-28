using AIMobileCus.Models.DataModels;

namespace AIMobileCus.Services.Domains
{
    public interface INotiService
    {
        public NotiEntity GetById(string id);
        void Entry(NotiEntity notiEntity);
        IList<NotiEntity> RetrieveAll ();
        void Update(NotiEntity Noti);
        IList<NotiEntity> RetrieveByTransactionId(string TransactionId);
    }
}
