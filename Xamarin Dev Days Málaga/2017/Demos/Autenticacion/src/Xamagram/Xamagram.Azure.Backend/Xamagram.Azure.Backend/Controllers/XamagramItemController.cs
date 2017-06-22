using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Xamagram.DataObjects;
using Xamagram.Azure.Backend;

namespace Xamagram.Controllers
{
    [Authorize]
    public class XamagramItemController : TableController<XamagramItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<XamagramItem>(context, Request);
        }

        // GET tables/XamagramItem
        public IQueryable<XamagramItem> GetAllXamagramItem()
        {
            return Query(); 
        }

        // GET tables/XamagramItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<XamagramItem> GetXamagramItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/XamagramItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<XamagramItem> PatchXamagramItem(string id, Delta<XamagramItem> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/XamagramItem
        public async Task<IHttpActionResult> PostXamagramItem(XamagramItem item)
        {
            XamagramItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/XamagramItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteXamagramItem(string id)
        {
             return DeleteAsync(id);
        }
    }
}